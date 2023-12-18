﻿using Comercio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.AccesoDatos.Configuracion
{
    public class BodegaConfiguracion : IEntityTypeConfiguration<BodegaModel>
    {
        public void Configure(EntityTypeBuilder<BodegaModel> builder)
        {
            builder.Property( x => x.Id).IsRequired();
            builder.Property( x => x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property( x => x.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Estado).IsRequired();
        }
    }
}
