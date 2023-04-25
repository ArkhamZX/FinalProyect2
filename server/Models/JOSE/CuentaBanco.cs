using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProyect2.Models.Jose
{
  [Table("CuentaBanco", Schema = "dbo")]
  public partial class CuentaBanco
  {
    [Key]
    public int IdCuentaBanco
    {
      get;
      set;
    }

    public ICollection<Inversione> Inversiones { get; set; }
    public ICollection<CuotaInversion> CuotaInversions { get; set; }
    [ConcurrencyCheck]
    public string Banco
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string TipoCuenta
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
