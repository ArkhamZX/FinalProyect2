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
    public partial class AddGarantiumComponent : ComponentBase
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

        IEnumerable<FinalProyect2.Models.Jose.Prestamo> _getPrestamosForFk_IdPrestamosResult;
        protected IEnumerable<FinalProyect2.Models.Jose.Prestamo> getPrestamosForFk_IdPrestamosResult
        {
            get
            {
                return _getPrestamosForFk_IdPrestamosResult;
            }
            set
            {
                if (!object.Equals(_getPrestamosForFk_IdPrestamosResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPrestamosForFk_IdPrestamosResult", NewValue = value, OldValue = _getPrestamosForFk_IdPrestamosResult };
                    _getPrestamosForFk_IdPrestamosResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        FinalProyect2.Models.Jose.Garantium _garantium;
        protected FinalProyect2.Models.Jose.Garantium garantium
        {
            get
            {
                return _garantium;
            }
            set
            {
                if (!object.Equals(_garantium, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "garantium", NewValue = value, OldValue = _garantium };
                    _garantium = value;
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
            var joseGetPrestamosResult = await Jose.GetPrestamos();
            getPrestamosForFk_IdPrestamosResult = joseGetPrestamosResult;

            garantium = new FinalProyect2.Models.Jose.Garantium(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(FinalProyect2.Models.Jose.Garantium args)
        {
            try
            {
                var joseCreateGarantiumResult = await Jose.CreateGarantium(garantium);
                DialogService.Close(garantium);
            }
            catch (System.Exception joseCreateGarantiumException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Garantium!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
