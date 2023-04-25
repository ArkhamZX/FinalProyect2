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
    public partial class AddCuotaInversionComponent : ComponentBase
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

        IEnumerable<FinalProyect2.Models.Jose.Inversione> _getInversionesForFk_IdInversionesResult;
        protected IEnumerable<FinalProyect2.Models.Jose.Inversione> getInversionesForFk_IdInversionesResult
        {
            get
            {
                return _getInversionesForFk_IdInversionesResult;
            }
            set
            {
                if (!object.Equals(_getInversionesForFk_IdInversionesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getInversionesForFk_IdInversionesResult", NewValue = value, OldValue = _getInversionesForFk_IdInversionesResult };
                    _getInversionesForFk_IdInversionesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<FinalProyect2.Models.Jose.CuentaBanco> _getCuentaBancosForFk_IdCuentaBancoResult;
        protected IEnumerable<FinalProyect2.Models.Jose.CuentaBanco> getCuentaBancosForFk_IdCuentaBancoResult
        {
            get
            {
                return _getCuentaBancosForFk_IdCuentaBancoResult;
            }
            set
            {
                if (!object.Equals(_getCuentaBancosForFk_IdCuentaBancoResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCuentaBancosForFk_IdCuentaBancoResult", NewValue = value, OldValue = _getCuentaBancosForFk_IdCuentaBancoResult };
                    _getCuentaBancosForFk_IdCuentaBancoResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        FinalProyect2.Models.Jose.CuotaInversion _cuotainversion;
        protected FinalProyect2.Models.Jose.CuotaInversion cuotainversion
        {
            get
            {
                return _cuotainversion;
            }
            set
            {
                if (!object.Equals(_cuotainversion, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "cuotainversion", NewValue = value, OldValue = _cuotainversion };
                    _cuotainversion = value;
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
            var joseGetInversionesResult = await Jose.GetInversiones();
            getInversionesForFk_IdInversionesResult = joseGetInversionesResult;

            var joseGetCuentaBancosResult = await Jose.GetCuentaBancos();
            getCuentaBancosForFk_IdCuentaBancoResult = joseGetCuentaBancosResult;

            cuotainversion = new FinalProyect2.Models.Jose.CuotaInversion(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(FinalProyect2.Models.Jose.CuotaInversion args)
        {
            try
            {
                var joseCreateCuotaInversionResult = await Jose.CreateCuotaInversion(cuotainversion);
                DialogService.Close(cuotainversion);
            }
            catch (System.Exception joseCreateCuotaInversionException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new CuotaInversion!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
