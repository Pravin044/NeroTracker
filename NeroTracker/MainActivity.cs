using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using NeroTracker.Fragments;
using System;
using System.Collections.Generic;

namespace NeroTracker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            navigation.SelectedItemId = Resource.Id.navigation_home;


        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    Navigate(typeof(HomeFragment));
                    return true;    
                case Resource.Id.navigation_dashboard:
                    Navigate(typeof(dashboard_fragment));
                    return true;
                case Resource.Id.navigation_notifications:
                    Navigate(typeof(NotificationFragment));
                    return true;
            }
            return false;
        }

        private void Navigate(Type type)
        {
            var fragment = SupportFragmentManager.FindFragmentByTag(type.Name);
            if (fragment == null)
                fragment = (Android.Support.V4.App.Fragment)Activator.CreateInstance(type);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content, fragment, type.Name).Commit();
        }
    }
}

