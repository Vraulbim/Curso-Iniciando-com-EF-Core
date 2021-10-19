using CursoEFCore.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> p)
        {
            p.ToTable("Pedidos");
            p.HasKey(p => p.Id);
            p.Property(p => p.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            p.Property(p => p.Status).HasConversion<string>();
            p.Property(p => p.TipoFrete).HasConversion<int>();
            p.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

            p.HasMany(p => p.Itens)
                .WithOne(p => p.Pedido)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}