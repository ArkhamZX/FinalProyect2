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
    public partial class CuentaBancosComponent : ComponentBase
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
        protected RadzenDataGrid<FinalProyect2.Models.Jose.CuentaBanco> grid0;

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

        IEnumerable<FinalProyect2.Models.Jose.CuentaBanco> _getCuentaBancosResult;
        protected IEnumerable<FinalProyect2.Models.Jose.CuentaBanco> getCuentaBancosResult
        {
            get
            {
                return _getCuentaBancosResult;
            }
            set
            {
                if (!object.Equals(_getCuentaBancosResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCuentaBancosResult", NewValue = value, OldValue = _getCuentaBancosResult };
                    _getCuentaBancosResult = value;
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

            var joseGetCuentaBancosResult = await Jose.GetCuentaBancos(new Query() { Filter = $@"i => i.Banco.Contains(@0) || i.TipoCuenta.Contains(@1)", FilterParameters = new object[] { search, search }, Expand = "Cliente" });
            getCuentaBancosResult = joseGetCuentaBancosResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddCuentaBanco>("Add Cuenta Banco", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Jose.ExportCuentaBancosToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Cliente", Select = "IdCuentaBanco,Banco,TipoCuenta,Cliente.Nombre as ClienteNombre,FechaCreacion" }, $"Cuenta Bancos");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Jose.ExportCuentaBancosToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Cliente", Select = "IdCuentaBanco,Banco,TipoCuenta,Cliente.Nombre as ClienteNombre,FechaCreacion" }, $"Cuenta Bancos");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<FinalProyect2.Models.Jose.CuentaBanco> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditCuentaBanco>("Edit Cuenta Banco", new Dictionary<string, object>() { {"IdCuentaBanco", args.Data.IdCuentaBanco} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var joseDeleteCuentaBancoResult = await Jose.DeleteCuentaBanco(data.IdCuentaBanco);
                    if (joseDeleteCuentaBancoResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception joseDeleteCuentaBancoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete CuentaBanco" });
            }
        }
    }
}
