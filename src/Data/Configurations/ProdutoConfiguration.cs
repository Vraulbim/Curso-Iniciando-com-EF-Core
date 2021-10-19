using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> p)
        {
            p.ToTable("Produtos");
            p.HasKey(p => p.Id);
            p.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            p.Property(p => p.Descricao).HasColumnType("VARCHAR(60)");
            p.Property(p => p.Valor).IsRequired();
            p.Property(p => p.TipoProduto).HasConversion<string>();
        }
    }
}