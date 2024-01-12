using Comercio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comercio.AccesoDatos.Configuracion
{
    public class MarcaConfiguracion : IEntityTypeConfiguration<MarcaModel>
    {
        public void Configure(EntityTypeBuilder<MarcaModel> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Estado).IsRequired();
        }
    }
}
