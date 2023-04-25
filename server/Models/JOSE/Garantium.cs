using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProyect2.Models.Jose
{
  [Table("Garantia", Schema = "dbo")]
  public partial class Garantium
  {
    [Key]
    public int IdGarantia
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
    public string Valor
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Ubicacion
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
