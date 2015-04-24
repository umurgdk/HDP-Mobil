using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using ReactiveUI;
using ReactiveUI.AndroidSupport;

using Hdp.CoreRx.ViewModels.Organization;
using Hdp.CoreRx.ViewModels;

namespace Hdp.Droid.Fragments
{
    public class OrganizationFragment : ReactiveUI.AndroidSupport.ReactiveFragment<OrganizationMenuViewModel>
    {
        ReactiveListAdapter<OrganizationMenuItemViewModel> _menuAdapter;
        GridView _gridView;

        public OrganizationFragment (OrganizationMenuViewModel viewModel)
        {
            ViewModel = viewModel;

            _menuAdapter = new ReactiveListAdapter<OrganizationMenuItemViewModel> (ViewModel.OrganizationItems, CreateMenuItemView, InitializeMenuItemView);

            if (ViewModel is IRoutingViewModel)
            {
                ViewModel.RequestNavigation.Subscribe (x => {
                    var fragment = new ReadOrganizationFragment(x as OrganizationPageViewModel);

                    Activity.SupportFragmentManager.BeginTransaction()
                        .Add(Resource.Id.mainFrame, fragment, "MODAL")
                        .AddToBackStack(x.Title)
                        .Commit();
                });
            }
        }

        public View CreateMenuItemView (OrganizationMenuItemViewModel itemViewModel, ViewGroup viewGroup)
        {
            var inflater = LayoutInflater.From (viewGroup.Context);
            var view = inflater.Inflate (Resource.Layout.OrganizationMenuItemLayout, null);

            return view;
        }

        public void InitializeMenuItemView (OrganizationMenuItemViewModel itemViewModel, View view)
        {
            var itemTitle = view.FindViewById<TextView> (Resource.Id.menuTitle);
            itemTitle.Text = itemViewModel.Name;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            _gridView = new GridView (container.Context);
            _gridView.Adapter = _menuAdapter;
            _gridView.NumColumns = 2;
            _gridView.SetVerticalSpacing (10);

            _gridView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                var itemViewModel = ViewModel.OrganizationItems[e.Position];
                itemViewModel.GoToCommand.Execute(null);
            };

            return _gridView;
        }
    }
}

