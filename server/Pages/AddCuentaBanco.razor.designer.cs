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
    public partial class AddCuentaBancoComponent : ComponentBase
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

        IEnumerable<FinalProyect2.Models.Jose.Cliente> _getClientesForFk_IdClienteResult;
        protected IEnumerable<FinalProyect2.Models.Jose.Cliente> getClientesForFk_IdClienteResult
        {
            get
            {
                return _getClientesForFk_IdClienteResult;
            }
            set
            {
                if (!object.Equals(_getClientesForFk_IdClienteResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getClientesForFk_IdClienteResult", NewValue = value, OldValue = _getClientesForFk_IdClienteResult };
                    _getClientesForFk_IdClienteResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        FinalProyect2.Models.Jose.CuentaBanco _cuentabanco;
        protected FinalProyect2.Models.Jose.CuentaBanco cuentabanco
        {
            get
            {
                return _cuentabanco;
            }
            set
            {
                if (!object.Equals(_cuentabanco, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "cuentabanco", NewValue = value, OldValue = _cuentabanco };
                    _cuentabanco = value;
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
            var joseGetClientesResult = await Jose.GetClientes();
            getClientesForFk_IdClienteResult = joseGetClientesResult;

            cuentabanco = new FinalProyect2.Models.Jose.CuentaBanco(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(FinalProyect2.Models.Jose.CuentaBanco args)
        {
            try
            {
                var joseCreateCuentaBancoResult = await Jose.CreateCuentaBanco(cuentabanco);
                DialogService.Close(cuentabanco);
            }
            catch (System.Exception joseCreateCuentaBancoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new CuentaBanco!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
