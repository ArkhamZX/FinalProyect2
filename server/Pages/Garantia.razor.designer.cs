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
    public partial class GarantiaComponent : ComponentBase
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
        protected RadzenDataGrid<FinalProyect2.Models.Jose.Garantium> grid0;

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

        IEnumerable<FinalProyect2.Models.Jose.Garantium> _getGarantiaResult;
        protected IEnumerable<FinalProyect2.Models.Jose.Garantium> getGarantiaResult
        {
            get
            {
                return _getGarantiaResult;
            }
            set
            {
                if (!object.Equals(_getGarantiaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getGarantiaResult", NewValue = value, OldValue = _getGarantiaResult };
                    _getGarantiaResult = value;
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

            var joseGetGarantiaResult = await Jose.GetGarantia(new Query() { Filter = $@"i => i.Tipo.Contains(@0) || i.Valor.Contains(@1) || i.Ubicacion.Contains(@2)", FilterParameters = new object[] { search, search, search }, Expand = "Prestamo" });
            getGarantiaResult = joseGetGarantiaResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddGarantium>("Add Garantium", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Jose.ExportGarantiaToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Prestamo", Select = "IdGarantia,Tipo,Valor,Ubicacion,Prestamo.Periodo as PrestamoPeriodo,FechaCreacion" }, $"Garantia");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Jose.ExportGarantiaToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Prestamo", Select = "IdGarantia,Tipo,Valor,Ubicacion,Prestamo.Periodo as PrestamoPeriodo,FechaCreacion" }, $"Garantia");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<FinalProyect2.Models.Jose.Garantium> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditGarantium>("Edit Garantium", new Dictionary<string, object>() { {"IdGarantia", args.Data.IdGarantia} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var joseDeleteGarantiumResult = await Jose.DeleteGarantium(data.IdGarantia);
                    if (joseDeleteGarantiumResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception joseDeleteGarantiumException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Garantium" });
            }
        }
    }
}
