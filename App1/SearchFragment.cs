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
    public class SearchFragment : BaseFragment
    {
        Button button1;
        EditText editText12;
        TextView _textMesage;

        List<string> listOfUnivers = new List<string>();
        List<string> listOfCountry = new List<string>();
        List<string> listOfWeb = new List<string>();
        public InputMethodManager inputMethodManager;
        public int selectedPos;

        ListView _listUnivers;
        ArrayAdapter<string> adapter;

        protected override int FragmentId
        {
            get => Resource.Layout.searchFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            button1 = view.FindViewById<Button>(Resource.Id.button1);
            _listUnivers = view.FindViewById<ListView>(Resource.Id.listOfUnivers);
            editText12 = view.FindViewById<EditText>(Resource.Id.editText12);
            _textMesage = view.FindViewById<TextView>(Resource.Id.textMesage);
            _listUnivers.ItemClick += GoToDetails;
            button1.Click += SearchButton;

            return view;          
        }


        public void GoToDetails(object sender, AdapterView.ItemClickEventArgs e)
        {
            TestDetails testDetails = new TestDetails();
            testDetails.Univer = listOfUnivers[e.Position];
            testDetails.Country = listOfCountry[e.Position];
            testDetails.Web = listOfWeb[e.Position];

            Controller.NavigateTo(testDetails);
        }

        public void SearchButton(object sender, EventArgs e)
        {
            listOfUnivers.Clear();
            listOfCountry.Clear();
            listOfWeb.Clear();
            _textMesage.Text = "";
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
                    adapter = new ArrayAdapter<string>( base.Context , Android.Resource.Layout.SimpleListItem1, listOfUnivers);
                    _listUnivers.Adapter = adapter;
                }
                else
                {
                    _textMesage.Text = "Check spelling";
                }
            }
            else
            {
                _textMesage.Text = "No result, choose any country";
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