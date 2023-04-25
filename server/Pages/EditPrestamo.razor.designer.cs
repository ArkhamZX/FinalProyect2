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
    public partial class EditPrestamoComponent : ComponentBase
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

        [Parameter]
        public dynamic IdPrestamos { get; set; }

        bool _hasChanges;
        protected bool hasChanges
        {
            get
            {
                return _hasChanges;
            }
            set
            {
                if (!object.Equals(_hasChanges, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "hasChanges", NewValue = value, OldValue = _hasChanges };
                    _hasChanges = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if (!object.Equals(_canEdit, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "canEdit", NewValue = value, OldValue = _canEdit };
                    _canEdit = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        FinalProyect2.Models.Jose.Prestamo _prestamo;
        protected FinalProyect2.Models.Jose.Prestamo prestamo
        {
            get
            {
                return _prestamo;
            }
            set
            {
                if (!object.Equals(_prestamo, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "prestamo", NewValue = value, OldValue = _prestamo };
                    _prestamo = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            hasChanges = false;

            canEdit = true;

            var joseGetPrestamoByIdPrestamosResult = await Jose.GetPrestamoByIdPrestamos(IdPrestamos);
            prestamo = joseGetPrestamoByIdPrestamosResult;

            var joseGetClientesResult = await Jose.GetClientes();
            getClientesForFk_IdClienteResult = joseGetClientesResult;
        }

        protected async System.Threading.Tasks.Task CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            Jose.Reset();

            await this.Load();
        }

        protected async System.Threading.Tasks.Task Form0Submit(FinalProyect2.Models.Jose.Prestamo args)
        {
            try
            {
                var joseUpdatePrestamoResult = await Jose.UpdatePrestamo(IdPrestamos, prestamo);
                DialogService.Close(prestamo);
            }
            catch (System.Exception joseUpdatePrestamoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Prestamo" });

            hasChanges = joseUpdatePrestamoException is DbUpdateConcurrencyException;

            if (!(joseUpdatePrestamoException is DbUpdateConcurrencyException)) {
                canEdit = false;
            }
            }
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
