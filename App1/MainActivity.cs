using System;
using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Newtonsoft.Json;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Util;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        public InputMethodManager inputMethodManager;
        // Fragments 
        SearchFragment searchFragment;
        DetailsFragment detailsFragment;
        About aboutFragment;
        CloseFragment closeFragment;
        //Stack and controller
        Controller controller;
        Stack<Android.Support.V4.App.Fragment> stack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            stack = new Stack<Android.Support.V4.App.Fragment>();
            searchFragment = new SearchFragment();
            detailsFragment = new DetailsFragment();
            aboutFragment = new About();
            closeFragment = new CloseFragment();
            controller = new Controller(searchFragment, detailsFragment, aboutFragment, closeFragment, stack);

            var transaction = SupportFragmentManager.BeginTransaction();
            
            controller.AddAllFragments(transaction, stack);

            searchFragment.SetController(controller);

            Window.SetSoftInputMode(SoftInput.StateHidden);
        }

        public override void OnBackPressed()
        {
            var transactionSupport = SupportFragmentManager.BeginTransaction();
            var transaction = FragmentManager.BeginTransaction();
            controller.BackPressed(transactionSupport, transaction);
        }

    }
}
