﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile_IB150232.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mobile_IB150232
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new Login();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
