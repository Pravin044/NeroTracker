using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace NeroTracker.Fragments
{

    class dashboard_fragment : Fragment
    {
        private View view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.Dashboard_Activity, container, false);
            return view;
        }


    }
}