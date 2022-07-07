using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.InputMethodServices;

namespace App1
{
    public class SearchFragment : Android.Support.V4.App.Fragment
    {
        Button button1;
        EditText editText1;
        EditText editText12;
        TextView textMesage;
        List<string> list;
        List<string> listOfUnivers = new List<string>();
        List<string> listOfCountry = new List<string>();
        List<string> listOfWeb = new List<string>();
        public InputMethodManager inputMethodManager;
        public int selectedPos;

        public Controller controller;

        ListView listView1;
        TextView textUniver;
        TextView textCountry;
        TextView textWeb;
        TextView About;

        public View view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.searchFragment, container, false);

            button1 = view.FindViewById<Button>(Resource.Id.button1);
            listView1 = view.FindViewById<ListView>(Resource.Id.listOfUnivers);
            editText12 = view.FindViewById<EditText>(Resource.Id.editText12);
            textMesage = view.FindViewById<TextView>(Resource.Id.textMesage);
            About = view.FindViewById<TextView>(Resource.Id.About);

            About.Click += AboutClick;
            listView1.ItemClick += GoToDetails;
            button1.Click += SearchButton;

            return view;          
        }

        public void SetController(Controller contr)
        {
            controller = contr;
        }

        [Obsolete]
        private void AboutClick(object sender, EventArgs e)
        {
            var t = Activity.FragmentManager.BeginTransaction();
            About about2 = new About();
            controller.About(t, about2);
        }

        public void GoToDetails(object sender, AdapterView.ItemClickEventArgs e)
        {
            var transaction = Activity.SupportFragmentManager.BeginTransaction();
            controller.GoToDetails(listOfCountry, listOfUnivers, listOfWeb, e.Position, transaction);
        }

        public void SearchButton(object sender, EventArgs e)
        {
            listOfUnivers.Clear();
            listOfCountry.Clear();
            listOfWeb.Clear();
            textMesage.Text = "";
            if (editText12.Text != "")
            {
                string json = new WebClient().DownloadString("http://universities.hipolabs.com/search?country=" + editText12.Text);
                var p = JsonConvert.DeserializeObject<List<Uni>>(json);
                foreach (var i in p)
                {
                    listOfUnivers.Add(i.name);
                    listOfCountry.Add(i.country);
                    listOfWeb.Add(i.web_pages[0]);
                }
                if (listOfUnivers.Count > 0)
                {
                    listOfUnivers.Sort();
                    listOfCountry.Sort();
                    listOfWeb.Sort();
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>( view.Context , Android.Resource.Layout.SimpleListItem1, listOfUnivers);
                    listView1.Adapter = adapter;
                }
                else
                {
                    textMesage.Text = "Check spelling";
                }
            }
            else
            {
                textMesage.Text = "No result, choose any country";
            }
        }

        public class Uni
        {
            public List<string> domains { get; set; }
            public List<string> web_pages { get; set; }

            [JsonProperty("state-province")]
            public object StateProvince { get; set; }
            public string name { get; set; }
            public string country { get; set; }
            public string alpha_two_code { get; set; }
        }

    }
}