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
    public class CloseFragment : DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.CloseFragment, container, false);
            Button button = view.FindViewById<Button>(Resource.Id.ExitButton);
            button.Click += CloseApplication;
            return view;
        }

        private void CloseApplication(object sender, EventArgs e)
        {
            Activity.Finish();
        }
    }
}