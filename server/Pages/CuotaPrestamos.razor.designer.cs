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
    public partial class CuotaPrestamosComponent : ComponentBase
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
        protected RadzenDataGrid<FinalProyect2.Models.Jose.CuotaPrestamo> grid0;

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

        IEnumerable<FinalProyect2.Models.Jose.CuotaPrestamo> _getCuotaPrestamosResult;
        protected IEnumerable<FinalProyect2.Models.Jose.CuotaPrestamo> getCuotaPrestamosResult
        {
            get
            {
                return _getCuotaPrestamosResult;
            }
            set
            {
                if (!object.Equals(_getCuotaPrestamosResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCuotaPrestamosResult", NewValue = value, OldValue = _getCuotaPrestamosResult };
                    _getCuotaPrestamosResult = value;
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

            var joseGetCuotaPrestamosResult = await Jose.GetCuotaPrestamos(new Query() { Filter = $@"i => i.TipoDePrestamos.Contains(@0) || i.Estado.Contains(@1)", FilterParameters = new object[] { search, search }, Expand = "Prestamo" });
            getCuotaPrestamosResult = joseGetCuotaPrestamosResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddCuotaPrestamo>("Add Cuota Prestamo", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Jose.ExportCuotaPrestamosToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Prestamo", Select = "IdCuotaPrestamo,Monto,Interes,TipoDePrestamos,Estado,FechaPlanificada,FechaRealizada,CuotaPres,Prestamo.Periodo as PrestamoPeriodo,FechaCreacion" }, $"Cuota Prestamos");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Jose.ExportCuotaPrestamosToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Prestamo", Select = "IdCuotaPrestamo,Monto,Interes,TipoDePrestamos,Estado,FechaPlanificada,FechaRealizada,CuotaPres,Prestamo.Periodo as PrestamoPeriodo,FechaCreacion" }, $"Cuota Prestamos");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<FinalProyect2.Models.Jose.CuotaPrestamo> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditCuotaPrestamo>("Edit Cuota Prestamo", new Dictionary<string, object>() { {"IdCuotaPrestamo", args.Data.IdCuotaPrestamo} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var joseDeleteCuotaPrestamoResult = await Jose.DeleteCuotaPrestamo(data.IdCuotaPrestamo);
                    if (joseDeleteCuotaPrestamoResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception joseDeleteCuotaPrestamoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete CuotaPrestamo" });
            }
        }
    }
}
