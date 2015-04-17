using System;
using Java.Lang;

using Android.Support.V4.App;
using System.Collections.Generic;
using Hdp.Droid.Fragments;
using Android.Runtime;

namespace Hdp.Droid.Adapters
{
    public class MainTabsAdapter : FragmentStatePagerAdapter
    {
        ICharSequence[] _titles;
        List<Fragment> _fragments;

        public MainTabsAdapter (FragmentManager fm) : base (fm)
        {
            _titles = CharSequence.ArrayFromStringArray(new string[] {"Secim", "Haberler", "Etkinlikler", "Parti"});
            _fragments = new List<Fragment> {
                new DummyFragment(),
                new DummyFragment(),
                new DummyFragment(),
                new DummyFragment()
            };
        }

        public override int Count {
            get {
                return 4;
            }
        }

        public override Fragment GetItem (int position)
        {
            return _fragments [position];
        }

        public override ICharSequence GetPageTitleFormatted (int position)
        {
            return _titles [position];
        }
    }
}

