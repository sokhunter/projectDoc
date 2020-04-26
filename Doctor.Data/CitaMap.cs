using Doctor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Doctor.Data
{
    class CitaMap : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Cita")
            .HasKey(c => c.CitaId);
            builder.Property(c => c.Motivo)
                .HasColumnName("Motivo")
                .HasMaxLength(50); //.IsRequired()
            builder.Property(c => c.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(100); //.IsRequired()
            builder.Property(c => c.Sintomas)
                .HasColumnName("Sintomas")
                .HasMaxLength(100); //.IsRequired()
            builder.Property(c => c.Exploracion)
                .HasColumnName("Exploracion")
                .HasMaxLength(100); //.IsRequired()
            builder.Property(c => c.Indicacion)
                .HasColumnName("Indicacion")
                .HasMaxLength(100); //.IsRequired()
            builder.Property(c => c.PacienteId)
                .HasColumnName("PacienteId");
            builder.Property(c => c.DoctorId)
                .HasColumnName("DoctorId");
            
        }
    }
}
