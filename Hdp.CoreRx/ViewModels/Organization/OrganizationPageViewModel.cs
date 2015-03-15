using System;
using ReactiveUI;

namespace Hdp.CoreRx.ViewModels.Organization
{
    public class OrganizationPageViewModel : BaseViewModel
    {
        private string body;

        public string Body {
            get { return this.body; }
            set { this.RaiseAndSetIfChanged (ref this.body, value); }
        }
    }
}

