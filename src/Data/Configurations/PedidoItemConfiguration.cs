using CursoEFCore.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> p)
        {
            p.ToTable("PedidoItens");
            p.HasKey(p => p.Id);
            p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
            p.Property(p => p.Valor).IsRequired();
            p.Property(p => p.Desconto).IsRequired();
        }
    }
}