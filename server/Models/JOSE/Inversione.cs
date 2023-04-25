using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProyect2.Models.Jose
{
  [Table("Inversiones", Schema = "dbo")]
  public partial class Inversione
  {
    [Key]
    public int IdInversiones
    {
      get;
      set;
    }

    public ICollection<CuotaInversion> CuotaInversions { get; set; }
    [ConcurrencyCheck]
    public int? Monto
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? FechaSolocitud
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? FechaBeg
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? FechaEnd
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? Interes
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int Fk_IdCliente
    {
      get;
      set;
    }
    public Cliente Cliente { get; set; }
    [ConcurrencyCheck]
    public int Fk_IdCuentaBanco
    {
      get;
      set;
    }
    public CuentaBanco CuentaBanco { get; set; }
    [ConcurrencyCheck]
    public DateTime? FechaCreacion
    {
      get;
      set;
    }
  }
}
