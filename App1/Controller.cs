using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Android;
using Xamarin;
using FragmentManager = Android.Support.V4.App.FragmentManager;
namespace App1
{
    public class Controller
    {

        public Stack<BaseFragment> _stack;
        private FragmentManager _manager;
        private int _containerId;
        private bool _backFlag;
        public Controller(FragmentManager manager, int containerId)
        {
            _stack = new Stack<BaseFragment>();
            _manager = manager;
            _containerId = containerId;
        }


        public void NavigateTo(BaseFragment fragment)
        {
            if (_backFlag == false)
            {
                _stack.Push(fragment);
                fragment.SetController(this);
                var trans = _manager.BeginTransaction();
                trans.Add(_containerId, fragment, "");
                foreach (var i in _stack)
                {
                    trans.Hide(i);
                }
                trans.Show(fragment);
                trans.Commit();
                _backFlag = false;
            }
            else
            {
                fragment.SetController(this);
                var trans = _manager.BeginTransaction();
                trans.Remove(fragment);
                _stack.Pop();
                foreach (var i in _stack)
                {
                    trans.Hide(i);
                }
                trans.Show(_stack.Peek());
             
                trans.Commit();
                _backFlag = false;
            }
        }

        public void GoBack()
        {
            _backFlag = true;
            NavigateTo(_stack.Peek());
        }

    }
}