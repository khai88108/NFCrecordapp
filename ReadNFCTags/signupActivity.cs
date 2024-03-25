﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.Threading.Tasks;
using SQLite;
using System.IO;

namespace SMARTpark
{
    [Activity(Label = "SMARTPark")]
    public class signupActivity : Activity
    {
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signup);
            btncreate = FindViewById<Button>(Resource.Id.Register);
            txtusername = FindViewById<EditText>(Resource.Id.userName);
            txtPassword = FindViewById<EditText>(Resource.Id.password);
            btncreate.Click += Btncreate_Click;
            // Create your application here
        }
        private void Btncreate_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<LoginTable>();
                LoginTable tbl = new LoginTable();
                tbl.username = txtusername.Text;
                tbl.password = txtPassword.Text;
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                StartActivity(typeof(LoginActivity));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}
