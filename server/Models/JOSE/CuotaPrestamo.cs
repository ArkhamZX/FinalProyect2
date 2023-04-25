using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProyect2.Models.Jose
{
  [Table("CuotaPrestamo", Schema = "dbo")]
  public partial class CuotaPrestamo
  {
    [Key]
    public int IdCuotaPrestamo
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
    public string TipoDePrestamos
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
    public int? CuotaPres
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int Fk_IdPrestamos
    {
      get;
      set;
    }
    public Prestamo Prestamo { get; set; }
    [ConcurrencyCheck]
    public DateTime? FechaCreacion
    {
      get;
      set;
    }
  }
}
