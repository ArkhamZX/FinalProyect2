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
    public partial class CuotaInversionsComponent : ComponentBase
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
        protected RadzenDataGrid<FinalProyect2.Models.Jose.CuotaInversion> grid0;

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

        IEnumerable<FinalProyect2.Models.Jose.CuotaInversion> _getCuotaInversionsResult;
        protected IEnumerable<FinalProyect2.Models.Jose.CuotaInversion> getCuotaInversionsResult
        {
            get
            {
                return _getCuotaInversionsResult;
            }
            set
            {
                if (!object.Equals(_getCuotaInversionsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCuotaInversionsResult", NewValue = value, OldValue = _getCuotaInversionsResult };
                    _getCuotaInversionsResult = value;
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

            var joseGetCuotaInversionsResult = await Jose.GetCuotaInversions(new Query() { Filter = $@"i => i.Tipo.Contains(@0) || i.Estado.Contains(@1)", FilterParameters = new object[] { search, search }, Expand = "Inversione,CuentaBanco" });
            getCuotaInversionsResult = joseGetCuotaInversionsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddCuotaInversion>("Add Cuota Inversion", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Jose.ExportCuotaInversionsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Inversione,CuentaBanco", Select = "IdCuotaInversion,Monto,Interes,Tipo,Estado,FechaPlanificada,FechaRealizada,CuotaInv,Inversione.IdInversiones as InversioneIdInversiones,CuentaBanco.Banco as CuentaBancoBanco,FechaCreacion" }, $"Cuota Inversions");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Jose.ExportCuotaInversionsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Inversione,CuentaBanco", Select = "IdCuotaInversion,Monto,Interes,Tipo,Estado,FechaPlanificada,FechaRealizada,CuotaInv,Inversione.IdInversiones as InversioneIdInversiones,CuentaBanco.Banco as CuentaBancoBanco,FechaCreacion" }, $"Cuota Inversions");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<FinalProyect2.Models.Jose.CuotaInversion> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditCuotaInversion>("Edit Cuota Inversion", new Dictionary<string, object>() { {"IdCuotaInversion", args.Data.IdCuotaInversion} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var joseDeleteCuotaInversionResult = await Jose.DeleteCuotaInversion(data.IdCuotaInversion);
                    if (joseDeleteCuotaInversionResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception joseDeleteCuotaInversionException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete CuotaInversion" });
            }
        }
    }
}
