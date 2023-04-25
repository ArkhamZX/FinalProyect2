using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using FinalProyect2.Models.Jose;

namespace FinalProyect2.Data
{
  public partial class JoseContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public JoseContext(DbContextOptions<JoseContext> options):base(options)
    {
    }

    public JoseContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<FinalProyect2.Models.Jose.SpGetCacularCuotum>().HasNoKey();
        builder.Entity<FinalProyect2.Models.Jose.CuentaBanco>()
              .HasOne(i => i.Cliente)
              .WithMany(i => i.CuentaBancos)
              .HasForeignKey(i => i.Fk_IdCliente)
              .HasPrincipalKey(i => i.IdCliente);
        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .HasOne(i => i.Inversione)
              .WithMany(i => i.CuotaInversions)
              .HasForeignKey(i => i.Fk_IdInversiones)
              .HasPrincipalKey(i => i.IdInversiones);
        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .HasOne(i => i.CuentaBanco)
              .WithMany(i => i.CuotaInversions)
              .HasForeignKey(i => i.Fk_IdCuentaBanco)
              .HasPrincipalKey(i => i.IdCuentaBanco);
        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .HasOne(i => i.Prestamo)
              .WithMany(i => i.CuotaPrestamos)
              .HasForeignKey(i => i.Fk_IdPrestamos)
              .HasPrincipalKey(i => i.IdPrestamos);
        builder.Entity<FinalProyect2.Models.Jose.Garantium>()
              .HasOne(i => i.Prestamo)
              .WithMany(i => i.Garantia)
              .HasForeignKey(i => i.Fk_IdPrestamos)
              .HasPrincipalKey(i => i.IdPrestamos);
        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .HasOne(i => i.Cliente)
              .WithMany(i => i.Inversiones)
              .HasForeignKey(i => i.Fk_IdCliente)
              .HasPrincipalKey(i => i.IdCliente);
        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .HasOne(i => i.CuentaBanco)
              .WithMany(i => i.Inversiones)
              .HasForeignKey(i => i.Fk_IdCuentaBanco)
              .HasPrincipalKey(i => i.IdCuentaBanco);
        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .HasOne(i => i.Cliente)
              .WithMany(i => i.Prestamos)
              .HasForeignKey(i => i.Fk_IdCliente)
              .HasPrincipalKey(i => i.IdCliente);

        builder.Entity<FinalProyect2.Models.Jose.Cliente>()
              .Property(p => p.FechaCreacion)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<FinalProyect2.Models.Jose.CuentaBanco>()
              .Property(p => p.FechaCreacion)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.FechaCreacion)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.FechaCreacion)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<FinalProyect2.Models.Jose.Garantium>()
              .Property(p => p.FechaCreacion)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.FechaCreacion)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.FechaCreacion)
              .HasDefaultValueSql("(getdate())");


        builder.Entity<FinalProyect2.Models.Jose.Cliente>()
              .Property(p => p.FechaCreacion)
              .HasColumnType("datetime");

        builder.Entity<FinalProyect2.Models.Jose.CuentaBanco>()
              .Property(p => p.FechaCreacion)
              .HasColumnType("datetime");

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.FechaPlanificada)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.FechaRealizada)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.FechaCreacion)
              .HasColumnType("datetime");

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.FechaPlanificada)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.FechaRealizada)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.FechaCreacion)
              .HasColumnType("datetime");

        builder.Entity<FinalProyect2.Models.Jose.Garantium>()
              .Property(p => p.FechaCreacion)
              .HasColumnType("datetime");

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.FechaSolocitud)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.FechaBeg)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.FechaEnd)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.FechaCreacion)
              .HasColumnType("datetime");

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.FechaSolocitud)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.FechaBeg)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.FechaEnd)
              .HasColumnType("date");

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.FechaCreacion)
              .HasColumnType("datetime");

        builder.Entity<FinalProyect2.Models.Jose.Cliente>()
              .Property(p => p.IdCliente)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuentaBanco>()
              .Property(p => p.IdCuentaBanco)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuentaBanco>()
              .Property(p => p.Fk_IdCliente)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.IdCuotaInversion)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.Monto)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.Interes)
              .HasPrecision(38, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.CuotaInv)
              .HasPrecision(38, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.Fk_IdInversiones)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaInversion>()
              .Property(p => p.Fk_IdCuentaBanco)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.IdCuotaPrestamo)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.Monto)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.Interes)
              .HasPrecision(38, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.CuotaPres)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.CuotaPrestamo>()
              .Property(p => p.Fk_IdPrestamos)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Garantium>()
              .Property(p => p.IdGarantia)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Garantium>()
              .Property(p => p.Fk_IdPrestamos)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.IdInversiones)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.Monto)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.Interes)
              .HasPrecision(38, 0);

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.Fk_IdCliente)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Inversione>()
              .Property(p => p.Fk_IdCuentaBanco)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.IdPrestamos)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.Monto)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.Interes)
              .HasPrecision(38, 0);

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.IdCliente)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.Prestamo>()
              .Property(p => p.Fk_IdCliente)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.SpGetCacularCuotum>()
              .Property(p => p.IdPrestamos)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.SpGetCacularCuotum>()
              .Property(p => p.Monto)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.SpGetCacularCuotum>()
              .Property(p => p.Interes)
              .HasPrecision(38, 0);

        builder.Entity<FinalProyect2.Models.Jose.SpGetCacularCuotum>()
              .Property(p => p.CuotaPres)
              .HasPrecision(10, 0);

        builder.Entity<FinalProyect2.Models.Jose.SpGetCacularCuotum>()
              .Property(p => p.MontoCuota)
              .HasPrecision(38, 6);
        this.OnModelBuilding(builder);
    }


    public DbSet<FinalProyect2.Models.Jose.Cliente> Clientes
    {
      get;
      set;
    }

    public DbSet<FinalProyect2.Models.Jose.CuentaBanco> CuentaBancos
    {
      get;
      set;
    }

    public DbSet<FinalProyect2.Models.Jose.CuotaInversion> CuotaInversions
    {
      get;
      set;
    }

    public DbSet<FinalProyect2.Models.Jose.CuotaPrestamo> CuotaPrestamos
    {
      get;
      set;
    }

    public DbSet<FinalProyect2.Models.Jose.Garantium> Garantia
    {
      get;
      set;
    }

    public DbSet<FinalProyect2.Models.Jose.Inversione> Inversiones
    {
      get;
      set;
    }

    public DbSet<FinalProyect2.Models.Jose.Prestamo> Prestamos
    {
      get;
      set;
    }

    public DbSet<FinalProyect2.Models.Jose.SpGetCacularCuotum> SpGetCacularCuota
    {
      get;
      set;
    }
  }
}
