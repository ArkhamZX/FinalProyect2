using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProyect2.Models.Jose
{
  [Table("CuotaInversion", Schema = "dbo")]
  public partial class CuotaInversion
  {
    [Key]
    public int IdCuotaInversion
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int? Monto
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
    public string Tipo
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Estado
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? FechaPlanificada
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? FechaRealizada
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? CuotaInv
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int Fk_IdInversiones
    {
      get;
      set;
    }
    public Inversione Inversione { get; set; }
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
