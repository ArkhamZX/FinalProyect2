using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProyect2.Models.Jose
{
  [Table("sp_GetCacularCuota", Schema = "dbo")]
  public partial class SpGetCacularCuotum
  {
    public int IdPrestamos
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
    public int? CuotaPres
    {
      get;
      set;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal? MontoCuota
    {
      get;
      set;
    }
  }
}
