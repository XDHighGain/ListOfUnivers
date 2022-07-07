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
namespace App1
{
    public class Controller
    {
        SearchFragment searchFragment;
        DetailsFragment detailsFragment;
        About aboutFragment;
        CloseFragment closeFragment;

        Stack<Android.Support.V4.App.Fragment> stack;
        List<string> listOfCountry;
        List<string> listOfUnivers; List<string> listOfWeb;
        int position;
        Android.Support.V4.App.FragmentTransaction trans;

        public Controller(SearchFragment searchFr, DetailsFragment detailsFr, About aboutFr, CloseFragment closeFr, Stack<Android.Support.V4.App.Fragment> st)
        {
            searchFragment = searchFr;
            detailsFragment = detailsFr;
            aboutFragment = aboutFr;
            closeFragment = closeFr;
            stack = st;
        }

        public void AddAllFragments(Android.Support.V4.App.FragmentTransaction tr, Stack<Android.Support.V4.App.Fragment> st)
        {
            tr.Add(Resource.Id.ContainerFrame, searchFragment);
            tr.Add(Resource.Id.ContainerFrame, detailsFragment);
            //tr.Add(Resource.Id.ContainerFrame, aboutFragment);
           // tr.Add(Resource.Id.ContainerFrame, closeFragment);
            tr.Hide(detailsFragment);
            //tr.Hide(aboutFragment);
            //tr.Hide(closeFragment);
            tr.Commit();
            stack.Push(searchFragment);
        }

        public void ChangeFragment()
        {

        }

        public void GoToDetails(List<string> listOfCountry, List<string> listOfUnivers, List<string> listOfWeb, int position, Android.Support.V4.App.FragmentTransaction trans)
        {
            trans.Show(detailsFragment);
            trans.Hide(searchFragment);
            trans.Commit();
            stack.Push(detailsFragment);
            var uni = detailsFragment.View.FindViewById<TextView>(Resource.Id.textUniver);
            uni.Text = listOfUnivers[position];
            var country = detailsFragment.View.FindViewById<TextView>(Resource.Id.textCountry);
            country.Text = listOfCountry[position];
            var web = detailsFragment.View.FindViewById<TextView>(Resource.Id.textWeb);
            web.Text = listOfWeb[position];
        }

        internal void About(Android.Support.V4.App.FragmentTransaction t)
        {
            //t.Show(aboutFragment);
            //t.Hide(detailsFragment);
            //t.Hide(searchFragment);
            //t.Commit();
        }

        internal void BackPressed(Android.Support.V4.App.FragmentTransaction trans, FragmentTransaction trans2)
        {
            if (stack.Count == 1)
            {
                //trans.Hide(searchFragment);
                //trans.Commit();
                trans2.Add(closeFragment, "");
                trans2.Show(closeFragment);
                trans2.Commit();

            }
            else
            {
                //go to previous fragment in stack
                trans.Hide(stack.Pop());
                trans.Show(stack.Peek());
                trans.Commit();
            }
        }

        internal void About(FragmentTransaction t, About about2)
        {
            t.Add(about2, "");
            t.Commit();
        }
    }
}