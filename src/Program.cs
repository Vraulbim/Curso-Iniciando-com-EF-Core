using System.Collections.Generic;
using System;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Toda vez que subirmos a aplicação, o banco realiza a migração dos dados
            /*using var db = new Data.ApplicationContext();

            var existe = db.Database.GetPendingMigrations().Any();
            if(existe){
                db.Database.Migrate();
            }*/

            Console.WriteLine("Hello World!");
            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            //AtualizarDados();
            RemoverRegistro();
        }

        public static void InserirDados(){
            var produto = new Produto{
                Descricao = "Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            //db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine("Total de dados: " + registros);
        }

        public static void InserirDadosEmMassa(){
            var produto = new Produto{
                Descricao = "Produto",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente{
                Nome = "Vitor",
                CEP = "14702246",
                Cidade = "Bebedouro",
                Estado = "SP",
                Telefone = "17999999999"
            };

            var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine("Total de registro(s): " + registros);
        }

        public static void ConsultarDados(){
            using var db = new Data.ApplicationContext();
            var consultarPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultarPorMetodo = db.Clientes.AsNoTracking().Where(p => p.Id > 0).OrderBy(p => p.Nome).ToList();

            foreach(var cliente in consultarPorMetodo){
                //db.Clientes.Find(cliente.Id);
                //db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
                Console.WriteLine("Consultando cliente: " + cliente.Id);
            }
        }

        public static void CadastrarPedido(){
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido{
                ClienteId = cliente.Id ,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                Status = ValueObjects.StatusPedido.Analise,
                TipoFrete = ValueObjects.TipoFrete.SemFrete,
                Itens = new List<PedidoItem>{
                    new PedidoItem{
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
            Console.WriteLine("Cadastrado");
        }

        public static void ConsultarPedidoCarregamentoAdiantado(){
            using var db = new Data.ApplicationContext();
            var pedido = db.Pedidos.Include(p => p.Itens).ThenInclude(p => p.Produto).ToList();

            Console.WriteLine(pedido.Count);
        }
    
        public static void AtualizarDados(){
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(2);
            var cliente = new Cliente{Id = 2};
            var clienteDesconectado = new {
                Nome = "Desconhecido"
            };

            //cliente.Nome = "Atualizado novamente";
            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            //db.Clientes.Update(cliente);
            db.SaveChanges();
            Console.WriteLine("Salvo com sucesso");
        }
    
        public static void RemoverRegistro(){
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(2);
            var cliente = new Cliente{Id= 3};

            db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            //db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }
    }
}
