using System;
using ReactiveUI;

namespace Hdp.CoreRx.ViewModels.Organization
{
    public class OrganizationPageViewModel : BaseViewModel
    {
        private string _document;

        public string Document {
            get { return this._document; }
            set { this.RaiseAndSetIfChanged (ref this._document, value); }
        }
    }
}

