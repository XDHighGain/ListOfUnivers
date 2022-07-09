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
    public abstract class BaseFragment : Android.Support.V4.App.Fragment
    {

        private Controller _controller;

        protected Controller Controller
        {
            get { return _controller; }
        }

        protected abstract int FragmentId
        {
            get;
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(FragmentId, container, false);
        }

        public void SetController(Controller controller)
        {
            _controller = controller;
        }

    }
}