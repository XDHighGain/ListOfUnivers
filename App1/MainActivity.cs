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


        Controller controller;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var manager = SupportFragmentManager;

            controller = new Controller(manager, Resource.Id.ContainerFrame);
            controller.NavigateTo(new SearchFragment());

            Window.SetSoftInputMode(SoftInput.StateHidden);
        }

        public override void OnBackPressed()
        {
            if (controller._stack.Count == 1)
            {
                base.OnBackPressed();
            }
            else
            {
                controller.GoBack();
            }
            
        }

      
    }
}
