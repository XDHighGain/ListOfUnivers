using System;
using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button button1;
        EditText editText1;
        EditText editText12; 
        List<string> list;
        List<string> listOfUnivers = new List<string>();
        List<string> listOfCountry = new List<string>();
        List<string> listOfWeb = new List<string>();

        public int selectedPos;

        ListView listView1;
        TextView textUniver;
        TextView textCountry;
        TextView textWeb;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            button1 = FindViewById<Button>(Resource.Id.button1);
            //editText1 = FindViewById<EditText>(Resource.Id.editText1);
            listView1 = FindViewById<ListView>(Resource.Id.listView1);
            editText12 = FindViewById<EditText>(Resource.Id.editText12);

            listView1.ItemClick += GoToDetails;
            button1.Click += OnClickButton;
            editText12.Touch += TextViewTouched;
            listView1.ItemClick += SelectedItem;

            //list = new List<string> { "111", "222", "333" };
            //listView1 = FindViewById<ListView>(Resource.Id.listView1);
            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list);
            //listView1.Adapter = adapter;

        }

        public void SelectedItem(object sender, AdapterView.ItemClickEventArgs e)
        {
            
            
        }

        public void GoToDetails(object sender, AdapterView.ItemClickEventArgs e)
        {

            SetContentView(Resource.Layout.content_main);
            selectedPos = e.Position;
            textCountry = FindViewById<TextView>(Resource.Id.textCountry);
            textCountry.Text = listOfCountry[selectedPos];

            textWeb = FindViewById<TextView>(Resource.Id.textWeb);
            textWeb.Text = listOfWeb[selectedPos];

            textUniver = FindViewById<TextView>(Resource.Id.textUniver);
            textUniver.Text = listOfUnivers[selectedPos];
        }

        public void OnClickButton(object sender, EventArgs e)
        {
            
            //button1.Text = "ffff";
            string json = new WebClient().DownloadString("http://universities.hipolabs.com/search?country=" + editText12.Text);

            var p = JsonConvert.DeserializeObject<List<Uni>>(json);
            foreach (var i in p)
            {
                listOfUnivers.Add(i.name);
                listOfCountry.Add(i.country);
                listOfWeb.Add(i.web_pages[0]);
            }
            listOfUnivers.Sort();
            listOfCountry.Sort();
            listOfWeb.Sort();


            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listOfUnivers);
            listView1.Adapter = adapter;
        }

        public void TextViewTouched(object sender, EventArgs e)
        {
            editText12.Text = "";
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
