using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProyect2.Models.Jose
{
  [Table("Cliente", Schema = "dbo")]
  public partial class Cliente
  {
    [Key]
    public int IdCliente
    {
      get;
      set;
    }

    public ICollection<CuentaBanco> CuentaBancos { get; set; }
    public ICollection<Prestamo> Prestamos { get; set; }
    public ICollection<Inversione> Inversiones { get; set; }
    [ConcurrencyCheck]
    public string Nombre
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Apellido
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string TipoDocumento
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Documento
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Genero
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Correo
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Clave
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Direccion
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Telefono
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? FechaCreacion
    {
      get;
      set;
    }
  }
}
