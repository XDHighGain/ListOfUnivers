using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1
{
    public class TestDetails : BaseFragment
    {
        public TextView _textUniver;
        public TextView _textCountry;
        public TextView _textWeb;
        private string _univer;
        private string _country;
        private string _web;

        public string Univer
        {           
            set { _univer = value; }
            private get { return _univer; }
        }
        public string Country
        {
            set { _country = value; }
            private get { return _country; }
        }
        public string Web
        {
            set { _web = value; }
            private get { return _web; }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {         
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _textUniver = view.FindViewById<TextView>(Resource.Id.textUniver);
            _textCountry = view.FindViewById<TextView>(Resource.Id.textCountry);
            _textWeb = view.FindViewById<TextView>(Resource.Id.textWeb);
            _textUniver.Text = Univer;
            _textCountry.Text = Country;
            _textWeb.Text = Web;
            return view;
        }
        protected override int FragmentId 
        {
            get => Resource.Layout.DetailsFragment; 
        }
    }
}