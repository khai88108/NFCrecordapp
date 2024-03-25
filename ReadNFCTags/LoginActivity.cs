using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using SQLite;

namespace SMARTpark
{
    [Activity(Label = "SMARTpark", MainLauncher = true) ]
    public class LoginActivity : Activity
    {
        EditText email;
        EditText password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);


            //Get email & password values from edit text  
            email = FindViewById<EditText>(Resource.Id.userName);
            password = FindViewById<EditText>(Resource.Id.password);
            //Trigger click event of Login Button  
            var logbutton = FindViewById<Button>(Resource.Id.login);
            logbutton.Click += DoLogin;
            var signupbutton = FindViewById<Button>(Resource.Id.Register);
            signupbutton.Click += Btncreate_Click;
        }
        public void DoLogin(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>(); //Call Table  
                var data1 = data.Where(x => x.username == email.Text && x.password == password.Text).FirstOrDefault(); //Linq Query 
                if
                (data1 != null)
                {
                Toast.MakeText(this, "Login successfully done!", ToastLength.Long).Show();
                StartActivity(typeof(MainActivity));
            }
            else
            {
                //Toast.makeText(getActivity(), "Wrong credentials found!", Toast.LENGTH_LONG).show();  
                Toast.MakeText(this, "Username or Password invalid!", ToastLength.Long).Show();
            }
        }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(signupActivity));
        }
        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }
    }
}

