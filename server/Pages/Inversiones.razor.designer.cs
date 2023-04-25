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
    public partial class InversionesComponent : ComponentBase
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
        protected RadzenDataGrid<FinalProyect2.Models.Jose.Inversione> grid0;

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

        IEnumerable<FinalProyect2.Models.Jose.Inversione> _getInversionesResult;
        protected IEnumerable<FinalProyect2.Models.Jose.Inversione> getInversionesResult
        {
            get
            {
                return _getInversionesResult;
            }
            set
            {
                if (!object.Equals(_getInversionesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getInversionesResult", NewValue = value, OldValue = _getInversionesResult };
                    _getInversionesResult = value;
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

            var joseGetInversionesResult = await Jose.GetInversiones(new Query() { Expand = "Cliente,CuentaBanco" });
            getInversionesResult = joseGetInversionesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddInversione>("Add Inversione", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Jose.ExportInversionesToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Cliente,CuentaBanco", Select = "IdInversiones,Monto,FechaSolocitud,FechaBeg,FechaEnd,Interes,Cliente.Nombre as ClienteNombre,CuentaBanco.Banco as CuentaBancoBanco,FechaCreacion" }, $"Inversiones");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Jose.ExportInversionesToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Cliente,CuentaBanco", Select = "IdInversiones,Monto,FechaSolocitud,FechaBeg,FechaEnd,Interes,Cliente.Nombre as ClienteNombre,CuentaBanco.Banco as CuentaBancoBanco,FechaCreacion" }, $"Inversiones");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<FinalProyect2.Models.Jose.Inversione> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditInversione>("Edit Inversione", new Dictionary<string, object>() { {"IdInversiones", args.Data.IdInversiones} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var joseDeleteInversioneResult = await Jose.DeleteInversione(data.IdInversiones);
                    if (joseDeleteInversioneResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception joseDeleteInversioneException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Inversione" });
            }
        }
    }
}
