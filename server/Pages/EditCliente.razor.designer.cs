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
    public partial class EditClienteComponent : ComponentBase
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
        public dynamic IdCliente { get; set; }

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

        FinalProyect2.Models.Jose.Cliente _cliente;
        protected FinalProyect2.Models.Jose.Cliente cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                if (!object.Equals(_cliente, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "cliente", NewValue = value, OldValue = _cliente };
                    _cliente = value;
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

            var joseGetClienteByIdClienteResult = await Jose.GetClienteByIdCliente(IdCliente);
            cliente = joseGetClienteByIdClienteResult;
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

        protected async System.Threading.Tasks.Task Form0Submit(FinalProyect2.Models.Jose.Cliente args)
        {
            try
            {
                var joseUpdateClienteResult = await Jose.UpdateCliente(IdCliente, cliente);
                DialogService.Close(cliente);
            }
            catch (System.Exception joseUpdateClienteException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Cliente" });

            hasChanges = joseUpdateClienteException is DbUpdateConcurrencyException;

            if (!(joseUpdateClienteException is DbUpdateConcurrencyException)) {
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
