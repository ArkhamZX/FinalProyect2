using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProyect2.Models.Jose
{
  [Table("Prestamos", Schema = "dbo")]
  public partial class Prestamo
  {
    [Key]
    public int IdPrestamos
    {
      get;
      set;
    }

    public ICollection<CuotaPrestamo> CuotaPrestamos { get; set; }
    public ICollection<Garantium> Garantia { get; set; }
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
    public string Periodo
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
    public string MetodoDePago
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int IdCliente
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
    public DateTime? FechaCreacion
    {
      get;
      set;
    }
  }
}
