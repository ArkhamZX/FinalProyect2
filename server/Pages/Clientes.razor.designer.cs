using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using FinalProyect2.Models.Jose;
using Microsoft.EntityFrameworkCore;

namespace FinalProyect2.Pages
{
    public partial class ClientesComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected JoseService Jose { get; set; }
        protected RadzenDataGrid<FinalProyect2.Models.Jose.Cliente> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<FinalProyect2.Models.Jose.Cliente> _getClientesResult;
        protected IEnumerable<FinalProyect2.Models.Jose.Cliente> getClientesResult
        {
            get
            {
                return _getClientesResult;
            }
            set
            {
                if (!object.Equals(_getClientesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getClientesResult", NewValue = value, OldValue = _getClientesResult };
                    _getClientesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }

            var joseGetClientesResult = await Jose.GetClientes(new Query() { Filter = $@"i => i.Nombre.Contains(@0) || i.Apellido.Contains(@1) || i.TipoDocumento.Contains(@2) || i.Documento.Contains(@3) || i.Genero.Contains(@4) || i.Correo.Contains(@5) || i.Clave.Contains(@6) || i.Direccion.Contains(@7) || i.Telefono.Contains(@8)", FilterParameters = new object[] { search, search, search, search, search, search, search, search, search } });
            getClientesResult = joseGetClientesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddCliente>("Add Cliente", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Jose.ExportClientesToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "IdCliente,Nombre,Apellido,TipoDocumento,Documento,Genero,Correo,Clave,Direccion,Telefono,FechaCreacion" }, $"Clientes");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Jose.ExportClientesToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "IdCliente,Nombre,Apellido,TipoDocumento,Documento,Genero,Correo,Clave,Direccion,Telefono,FechaCreacion" }, $"Clientes");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<FinalProyect2.Models.Jose.Cliente> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditCliente>("Edit Cliente", new Dictionary<string, object>() { {"IdCliente", args.Data.IdCliente} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var joseDeleteClienteResult = await Jose.DeleteCliente(data.IdCliente);
                    if (joseDeleteClienteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception joseDeleteClienteException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Cliente" });
            }
        }
    }
}
