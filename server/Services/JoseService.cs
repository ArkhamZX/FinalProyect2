using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using FinalProyect2.Data;

namespace FinalProyect2
{
    public partial class JoseService
    {
        JoseContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly JoseContext context;
        private readonly NavigationManager navigationManager;

        public JoseService(JoseContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportClientesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/clientes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/clientes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportClientesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/clientes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/clientes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnClientesRead(ref IQueryable<Models.Jose.Cliente> items);

        public async Task<IQueryable<Models.Jose.Cliente>> GetClientes(Query query = null)
        {
            var items = Context.Clientes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnClientesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnClienteCreated(Models.Jose.Cliente item);
        partial void OnAfterClienteCreated(Models.Jose.Cliente item);

        public async Task<Models.Jose.Cliente> CreateCliente(Models.Jose.Cliente cliente)
        {
            OnClienteCreated(cliente);

            var existingItem = Context.Clientes
                              .Where(i => i.IdCliente == cliente.IdCliente)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Clientes.Add(cliente);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cliente).State = EntityState.Detached;
                throw;
            }

            OnAfterClienteCreated(cliente);

            return cliente;
        }
        public async Task ExportCuentaBancosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/cuentabancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/cuentabancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCuentaBancosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/cuentabancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/cuentabancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCuentaBancosRead(ref IQueryable<Models.Jose.CuentaBanco> items);

        public async Task<IQueryable<Models.Jose.CuentaBanco>> GetCuentaBancos(Query query = null)
        {
            var items = Context.CuentaBancos.AsQueryable();

            items = items.Include(i => i.Cliente);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCuentaBancosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCuentaBancoCreated(Models.Jose.CuentaBanco item);
        partial void OnAfterCuentaBancoCreated(Models.Jose.CuentaBanco item);

        public async Task<Models.Jose.CuentaBanco> CreateCuentaBanco(Models.Jose.CuentaBanco cuentaBanco)
        {
            OnCuentaBancoCreated(cuentaBanco);

            var existingItem = Context.CuentaBancos
                              .Where(i => i.IdCuentaBanco == cuentaBanco.IdCuentaBanco)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CuentaBancos.Add(cuentaBanco);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cuentaBanco).State = EntityState.Detached;
                cuentaBanco.Cliente = null;
                throw;
            }

            OnAfterCuentaBancoCreated(cuentaBanco);

            return cuentaBanco;
        }
        public async Task ExportCuotaInversionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/cuotainversions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/cuotainversions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCuotaInversionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/cuotainversions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/cuotainversions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCuotaInversionsRead(ref IQueryable<Models.Jose.CuotaInversion> items);

        public async Task<IQueryable<Models.Jose.CuotaInversion>> GetCuotaInversions(Query query = null)
        {
            var items = Context.CuotaInversions.AsQueryable();

            items = items.Include(i => i.Inversione);

            items = items.Include(i => i.CuentaBanco);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCuotaInversionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCuotaInversionCreated(Models.Jose.CuotaInversion item);
        partial void OnAfterCuotaInversionCreated(Models.Jose.CuotaInversion item);

        public async Task<Models.Jose.CuotaInversion> CreateCuotaInversion(Models.Jose.CuotaInversion cuotaInversion)
        {
            OnCuotaInversionCreated(cuotaInversion);

            var existingItem = Context.CuotaInversions
                              .Where(i => i.IdCuotaInversion == cuotaInversion.IdCuotaInversion)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CuotaInversions.Add(cuotaInversion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cuotaInversion).State = EntityState.Detached;
                cuotaInversion.Inversione = null;
                cuotaInversion.CuentaBanco = null;
                throw;
            }

            OnAfterCuotaInversionCreated(cuotaInversion);

            return cuotaInversion;
        }
        public async Task ExportCuotaPrestamosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/cuotaprestamos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/cuotaprestamos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCuotaPrestamosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/cuotaprestamos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/cuotaprestamos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCuotaPrestamosRead(ref IQueryable<Models.Jose.CuotaPrestamo> items);

        public async Task<IQueryable<Models.Jose.CuotaPrestamo>> GetCuotaPrestamos(Query query = null)
        {
            var items = Context.CuotaPrestamos.AsQueryable();

            items = items.Include(i => i.Prestamo);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCuotaPrestamosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCuotaPrestamoCreated(Models.Jose.CuotaPrestamo item);
        partial void OnAfterCuotaPrestamoCreated(Models.Jose.CuotaPrestamo item);

        public async Task<Models.Jose.CuotaPrestamo> CreateCuotaPrestamo(Models.Jose.CuotaPrestamo cuotaPrestamo)
        {
            OnCuotaPrestamoCreated(cuotaPrestamo);

            var existingItem = Context.CuotaPrestamos
                              .Where(i => i.IdCuotaPrestamo == cuotaPrestamo.IdCuotaPrestamo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CuotaPrestamos.Add(cuotaPrestamo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cuotaPrestamo).State = EntityState.Detached;
                cuotaPrestamo.Prestamo = null;
                throw;
            }

            OnAfterCuotaPrestamoCreated(cuotaPrestamo);

            return cuotaPrestamo;
        }
        public async Task ExportGarantiaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/garantia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/garantia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGarantiaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/garantia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/garantia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGarantiaRead(ref IQueryable<Models.Jose.Garantium> items);

        public async Task<IQueryable<Models.Jose.Garantium>> GetGarantia(Query query = null)
        {
            var items = Context.Garantia.AsQueryable();

            items = items.Include(i => i.Prestamo);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnGarantiaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGarantiumCreated(Models.Jose.Garantium item);
        partial void OnAfterGarantiumCreated(Models.Jose.Garantium item);

        public async Task<Models.Jose.Garantium> CreateGarantium(Models.Jose.Garantium garantium)
        {
            OnGarantiumCreated(garantium);

            var existingItem = Context.Garantia
                              .Where(i => i.IdGarantia == garantium.IdGarantia)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Garantia.Add(garantium);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(garantium).State = EntityState.Detached;
                garantium.Prestamo = null;
                throw;
            }

            OnAfterGarantiumCreated(garantium);

            return garantium;
        }
        public async Task ExportInversionesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/inversiones/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/inversiones/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportInversionesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/inversiones/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/inversiones/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnInversionesRead(ref IQueryable<Models.Jose.Inversione> items);

        public async Task<IQueryable<Models.Jose.Inversione>> GetInversiones(Query query = null)
        {
            var items = Context.Inversiones.AsQueryable();

            items = items.Include(i => i.Cliente);

            items = items.Include(i => i.CuentaBanco);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnInversionesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnInversioneCreated(Models.Jose.Inversione item);
        partial void OnAfterInversioneCreated(Models.Jose.Inversione item);

        public async Task<Models.Jose.Inversione> CreateInversione(Models.Jose.Inversione inversione)
        {
            OnInversioneCreated(inversione);

            var existingItem = Context.Inversiones
                              .Where(i => i.IdInversiones == inversione.IdInversiones)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Inversiones.Add(inversione);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(inversione).State = EntityState.Detached;
                inversione.Cliente = null;
                inversione.CuentaBanco = null;
                throw;
            }

            OnAfterInversioneCreated(inversione);

            return inversione;
        }
        public async Task ExportPrestamosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/prestamos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/prestamos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPrestamosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/prestamos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/prestamos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPrestamosRead(ref IQueryable<Models.Jose.Prestamo> items);

        public async Task<IQueryable<Models.Jose.Prestamo>> GetPrestamos(Query query = null)
        {
            var items = Context.Prestamos.AsQueryable();

            items = items.Include(i => i.Cliente);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPrestamosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPrestamoCreated(Models.Jose.Prestamo item);
        partial void OnAfterPrestamoCreated(Models.Jose.Prestamo item);

        public async Task<Models.Jose.Prestamo> CreatePrestamo(Models.Jose.Prestamo prestamo)
        {
            OnPrestamoCreated(prestamo);

            var existingItem = Context.Prestamos
                              .Where(i => i.IdPrestamos == prestamo.IdPrestamos)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Prestamos.Add(prestamo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(prestamo).State = EntityState.Detached;
                prestamo.Cliente = null;
                throw;
            }

            OnAfterPrestamoCreated(prestamo);

            return prestamo;
        }

      public async Task ExportSpGetCacularCuotaToExcel(Query query = null, string fileName = null)
      {
          navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/spgetcacularcuota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/spgetcacularcuota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
      }

      public async Task ExportSpGetCacularCuotaToCSV(Query query = null, string fileName = null)
      {
          navigationManager.NavigateTo(query != null ? query.ToUrl($"export/jose/spgetcacularcuota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/jose/spgetcacularcuota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
      }

      public async Task<IQueryable<Models.Jose.SpGetCacularCuotum>> GetSpGetCacularCuota(Query query = null)
      {
          var items = Context.SpGetCacularCuota.FromSqlRaw("EXEC [dbo].[sp_GetCacularCuota]").ToList().AsQueryable();

          if (query != null)
          {
              if (!string.IsNullOrEmpty(query.Expand))
              {
                  var propertiesToExpand = query.Expand.Split(',');
                  foreach(var p in propertiesToExpand)
                  {
                      items = items.Include(p.Trim());
                  }
              }

              if (!string.IsNullOrEmpty(query.Filter))
              {
                  if (query.FilterParameters != null)
                  {
                      items = items.Where(query.Filter, query.FilterParameters);
                  }
                  else
                  {
                      items = items.Where(query.Filter);
                  }
              }

              if (!string.IsNullOrEmpty(query.OrderBy))
              {
                  items = items.OrderBy(query.OrderBy);
              }

              if (query.Skip.HasValue)
              {
                  items = items.Skip(query.Skip.Value);
              }

              if (query.Top.HasValue)
              {
                  items = items.Take(query.Top.Value);
              }
          }
          
          OnSpGetCacularCuotaInvoke(ref items);

          return await Task.FromResult(items);
      }

      partial void OnSpGetCacularCuotaInvoke(ref IQueryable<Models.Jose.SpGetCacularCuotum> items);

        partial void OnClienteDeleted(Models.Jose.Cliente item);
        partial void OnAfterClienteDeleted(Models.Jose.Cliente item);

        public async Task<Models.Jose.Cliente> DeleteCliente(int? idCliente)
        {
            var itemToDelete = Context.Clientes
                              .Where(i => i.IdCliente == idCliente)
                              .Include(i => i.CuentaBancos)
                              .Include(i => i.Prestamos)
                              .Include(i => i.Inversiones)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnClienteDeleted(itemToDelete);

            Context.Clientes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterClienteDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnClienteGet(Models.Jose.Cliente item);

        public async Task<Models.Jose.Cliente> GetClienteByIdCliente(int? idCliente)
        {
            var items = Context.Clientes
                              .AsNoTracking()
                              .Where(i => i.IdCliente == idCliente);

            var itemToReturn = items.FirstOrDefault();

            OnClienteGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Jose.Cliente> CancelClienteChanges(Models.Jose.Cliente item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnClienteUpdated(Models.Jose.Cliente item);
        partial void OnAfterClienteUpdated(Models.Jose.Cliente item);

        public async Task<Models.Jose.Cliente> UpdateCliente(int? idCliente, Models.Jose.Cliente cliente)
        {
            OnClienteUpdated(cliente);

            var itemToUpdate = Context.Clientes
                              .Where(i => i.IdCliente == idCliente)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cliente);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterClienteUpdated(cliente);

            return cliente;
        }

        partial void OnCuentaBancoDeleted(Models.Jose.CuentaBanco item);
        partial void OnAfterCuentaBancoDeleted(Models.Jose.CuentaBanco item);

        public async Task<Models.Jose.CuentaBanco> DeleteCuentaBanco(int? idCuentaBanco)
        {
            var itemToDelete = Context.CuentaBancos
                              .Where(i => i.IdCuentaBanco == idCuentaBanco)
                              .Include(i => i.Inversiones)
                              .Include(i => i.CuotaInversions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCuentaBancoDeleted(itemToDelete);

            Context.CuentaBancos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCuentaBancoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCuentaBancoGet(Models.Jose.CuentaBanco item);

        public async Task<Models.Jose.CuentaBanco> GetCuentaBancoByIdCuentaBanco(int? idCuentaBanco)
        {
            var items = Context.CuentaBancos
                              .AsNoTracking()
                              .Where(i => i.IdCuentaBanco == idCuentaBanco);

            items = items.Include(i => i.Cliente);

            var itemToReturn = items.FirstOrDefault();

            OnCuentaBancoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Jose.CuentaBanco> CancelCuentaBancoChanges(Models.Jose.CuentaBanco item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCuentaBancoUpdated(Models.Jose.CuentaBanco item);
        partial void OnAfterCuentaBancoUpdated(Models.Jose.CuentaBanco item);

        public async Task<Models.Jose.CuentaBanco> UpdateCuentaBanco(int? idCuentaBanco, Models.Jose.CuentaBanco cuentaBanco)
        {
            OnCuentaBancoUpdated(cuentaBanco);

            var itemToUpdate = Context.CuentaBancos
                              .Where(i => i.IdCuentaBanco == idCuentaBanco)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cuentaBanco);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCuentaBancoUpdated(cuentaBanco);

            return cuentaBanco;
        }

        partial void OnCuotaInversionDeleted(Models.Jose.CuotaInversion item);
        partial void OnAfterCuotaInversionDeleted(Models.Jose.CuotaInversion item);

        public async Task<Models.Jose.CuotaInversion> DeleteCuotaInversion(int? idCuotaInversion)
        {
            var itemToDelete = Context.CuotaInversions
                              .Where(i => i.IdCuotaInversion == idCuotaInversion)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCuotaInversionDeleted(itemToDelete);

            Context.CuotaInversions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCuotaInversionDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCuotaInversionGet(Models.Jose.CuotaInversion item);

        public async Task<Models.Jose.CuotaInversion> GetCuotaInversionByIdCuotaInversion(int? idCuotaInversion)
        {
            var items = Context.CuotaInversions
                              .AsNoTracking()
                              .Where(i => i.IdCuotaInversion == idCuotaInversion);

            items = items.Include(i => i.Inversione);

            items = items.Include(i => i.CuentaBanco);

            var itemToReturn = items.FirstOrDefault();

            OnCuotaInversionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Jose.CuotaInversion> CancelCuotaInversionChanges(Models.Jose.CuotaInversion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCuotaInversionUpdated(Models.Jose.CuotaInversion item);
        partial void OnAfterCuotaInversionUpdated(Models.Jose.CuotaInversion item);

        public async Task<Models.Jose.CuotaInversion> UpdateCuotaInversion(int? idCuotaInversion, Models.Jose.CuotaInversion cuotaInversion)
        {
            OnCuotaInversionUpdated(cuotaInversion);

            var itemToUpdate = Context.CuotaInversions
                              .Where(i => i.IdCuotaInversion == idCuotaInversion)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cuotaInversion);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCuotaInversionUpdated(cuotaInversion);

            return cuotaInversion;
        }

        partial void OnCuotaPrestamoDeleted(Models.Jose.CuotaPrestamo item);
        partial void OnAfterCuotaPrestamoDeleted(Models.Jose.CuotaPrestamo item);

        public async Task<Models.Jose.CuotaPrestamo> DeleteCuotaPrestamo(int? idCuotaPrestamo)
        {
            var itemToDelete = Context.CuotaPrestamos
                              .Where(i => i.IdCuotaPrestamo == idCuotaPrestamo)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCuotaPrestamoDeleted(itemToDelete);

            Context.CuotaPrestamos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCuotaPrestamoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCuotaPrestamoGet(Models.Jose.CuotaPrestamo item);

        public async Task<Models.Jose.CuotaPrestamo> GetCuotaPrestamoByIdCuotaPrestamo(int? idCuotaPrestamo)
        {
            var items = Context.CuotaPrestamos
                              .AsNoTracking()
                              .Where(i => i.IdCuotaPrestamo == idCuotaPrestamo);

            items = items.Include(i => i.Prestamo);

            var itemToReturn = items.FirstOrDefault();

            OnCuotaPrestamoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Jose.CuotaPrestamo> CancelCuotaPrestamoChanges(Models.Jose.CuotaPrestamo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCuotaPrestamoUpdated(Models.Jose.CuotaPrestamo item);
        partial void OnAfterCuotaPrestamoUpdated(Models.Jose.CuotaPrestamo item);

        public async Task<Models.Jose.CuotaPrestamo> UpdateCuotaPrestamo(int? idCuotaPrestamo, Models.Jose.CuotaPrestamo cuotaPrestamo)
        {
            OnCuotaPrestamoUpdated(cuotaPrestamo);

            var itemToUpdate = Context.CuotaPrestamos
                              .Where(i => i.IdCuotaPrestamo == idCuotaPrestamo)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cuotaPrestamo);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCuotaPrestamoUpdated(cuotaPrestamo);

            return cuotaPrestamo;
        }

        partial void OnGarantiumDeleted(Models.Jose.Garantium item);
        partial void OnAfterGarantiumDeleted(Models.Jose.Garantium item);

        public async Task<Models.Jose.Garantium> DeleteGarantium(int? idGarantia)
        {
            var itemToDelete = Context.Garantia
                              .Where(i => i.IdGarantia == idGarantia)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGarantiumDeleted(itemToDelete);

            Context.Garantia.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGarantiumDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnGarantiumGet(Models.Jose.Garantium item);

        public async Task<Models.Jose.Garantium> GetGarantiumByIdGarantia(int? idGarantia)
        {
            var items = Context.Garantia
                              .AsNoTracking()
                              .Where(i => i.IdGarantia == idGarantia);

            items = items.Include(i => i.Prestamo);

            var itemToReturn = items.FirstOrDefault();

            OnGarantiumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Jose.Garantium> CancelGarantiumChanges(Models.Jose.Garantium item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGarantiumUpdated(Models.Jose.Garantium item);
        partial void OnAfterGarantiumUpdated(Models.Jose.Garantium item);

        public async Task<Models.Jose.Garantium> UpdateGarantium(int? idGarantia, Models.Jose.Garantium garantium)
        {
            OnGarantiumUpdated(garantium);

            var itemToUpdate = Context.Garantia
                              .Where(i => i.IdGarantia == idGarantia)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(garantium);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterGarantiumUpdated(garantium);

            return garantium;
        }

        partial void OnInversioneDeleted(Models.Jose.Inversione item);
        partial void OnAfterInversioneDeleted(Models.Jose.Inversione item);

        public async Task<Models.Jose.Inversione> DeleteInversione(int? idInversiones)
        {
            var itemToDelete = Context.Inversiones
                              .Where(i => i.IdInversiones == idInversiones)
                              .Include(i => i.CuotaInversions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnInversioneDeleted(itemToDelete);

            Context.Inversiones.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterInversioneDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnInversioneGet(Models.Jose.Inversione item);

        public async Task<Models.Jose.Inversione> GetInversioneByIdInversiones(int? idInversiones)
        {
            var items = Context.Inversiones
                              .AsNoTracking()
                              .Where(i => i.IdInversiones == idInversiones);

            items = items.Include(i => i.Cliente);

            items = items.Include(i => i.CuentaBanco);

            var itemToReturn = items.FirstOrDefault();

            OnInversioneGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Jose.Inversione> CancelInversioneChanges(Models.Jose.Inversione item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnInversioneUpdated(Models.Jose.Inversione item);
        partial void OnAfterInversioneUpdated(Models.Jose.Inversione item);

        public async Task<Models.Jose.Inversione> UpdateInversione(int? idInversiones, Models.Jose.Inversione inversione)
        {
            OnInversioneUpdated(inversione);

            var itemToUpdate = Context.Inversiones
                              .Where(i => i.IdInversiones == idInversiones)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(inversione);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterInversioneUpdated(inversione);

            return inversione;
        }

        partial void OnPrestamoDeleted(Models.Jose.Prestamo item);
        partial void OnAfterPrestamoDeleted(Models.Jose.Prestamo item);

        public async Task<Models.Jose.Prestamo> DeletePrestamo(int? idPrestamos)
        {
            var itemToDelete = Context.Prestamos
                              .Where(i => i.IdPrestamos == idPrestamos)
                              .Include(i => i.CuotaPrestamos)
                              .Include(i => i.Garantia)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPrestamoDeleted(itemToDelete);

            Context.Prestamos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPrestamoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnPrestamoGet(Models.Jose.Prestamo item);

        public async Task<Models.Jose.Prestamo> GetPrestamoByIdPrestamos(int? idPrestamos)
        {
            var items = Context.Prestamos
                              .AsNoTracking()
                              .Where(i => i.IdPrestamos == idPrestamos);

            items = items.Include(i => i.Cliente);

            var itemToReturn = items.FirstOrDefault();

            OnPrestamoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Jose.Prestamo> CancelPrestamoChanges(Models.Jose.Prestamo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPrestamoUpdated(Models.Jose.Prestamo item);
        partial void OnAfterPrestamoUpdated(Models.Jose.Prestamo item);

        public async Task<Models.Jose.Prestamo> UpdatePrestamo(int? idPrestamos, Models.Jose.Prestamo prestamo)
        {
            OnPrestamoUpdated(prestamo);

            var itemToUpdate = Context.Prestamos
                              .Where(i => i.IdPrestamos == idPrestamos)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(prestamo);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterPrestamoUpdated(prestamo);

            return prestamo;
        }
    }
}
