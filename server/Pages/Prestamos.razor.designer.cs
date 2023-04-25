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
    public partial class PrestamosComponent : ComponentBase
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
        protected RadzenDataGrid<FinalProyect2.Models.Jose.Prestamo> grid0;

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

        IEnumerable<FinalProyect2.Models.Jose.Prestamo> _getPrestamosResult;
        protected IEnumerable<FinalProyect2.Models.Jose.Prestamo> getPrestamosResult
        {
            get
            {
                return _getPrestamosResult;
            }
            set
            {
                if (!object.Equals(_getPrestamosResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPrestamosResult", NewValue = value, OldValue = _getPrestamosResult };
                    _getPrestamosResult = value;
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

            var joseGetPrestamosResult = await Jose.GetPrestamos(new Query() { Filter = $@"i => i.Periodo.Contains(@0) || i.MetodoDePago.Contains(@1)", FilterParameters = new object[] { search, search }, Expand = "Cliente" });
            getPrestamosResult = joseGetPrestamosResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddPrestamo>("Add Prestamo", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Jose.ExportPrestamosToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Cliente", Select = "IdPrestamos,Monto,FechaSolocitud,FechaBeg,FechaEnd,Periodo,Interes,MetodoDePago,IdCliente,Cliente.Nombre as ClienteNombre,FechaCreacion" }, $"Prestamos");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Jose.ExportPrestamosToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Cliente", Select = "IdPrestamos,Monto,FechaSolocitud,FechaBeg,FechaEnd,Periodo,Interes,MetodoDePago,IdCliente,Cliente.Nombre as ClienteNombre,FechaCreacion" }, $"Prestamos");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<FinalProyect2.Models.Jose.Prestamo> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditPrestamo>("Edit Prestamo", new Dictionary<string, object>() { {"IdPrestamos", args.Data.IdPrestamos} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var joseDeletePrestamoResult = await Jose.DeletePrestamo(data.IdPrestamos);
                    if (joseDeletePrestamoResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception joseDeletePrestamoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Prestamo" });
            }
        }
    }
}
