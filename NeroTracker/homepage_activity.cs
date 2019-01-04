using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace NeroTracker
{
    [Activity(Label = "HomePage", Theme = "@style/AppTheme")]


     
    class homepage_activity:AppCompatActivity
    {
        private IList<string> items = new List<string>() { "SaMPLE1", "Sample2", "SaMPLE3", "Sample4", "SaMPLE5", "Sample6", "SaMPLE7", "Sample8" };
        private RecyclerAdapter adapter;
        private View layout;
        private SwipeRefreshLayout swipeLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.homepageActivity);
            var listView = FindViewById<RecyclerView>(Resource.Id.list_view);
            listView.SetLayoutManager(new GridLayoutManager(this, 2));
            adapter = new RecyclerAdapter(items);
            listView.SetAdapter(adapter);
            layout = FindViewById(Resource.Id.container);
            listView.AddItemDecoration(new SpaceDecorator());
            swipeLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_layout);
            swipeLayout.Refresh += SwipeLayout_Refresh;
        }


        private void SwipeLayout_Refresh(object sender, System.EventArgs e)
        {
            items.Clear();
            adapter.NotifyDataSetChanged();
            swipeLayout.Refreshing = false;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            items.Add($"Sample {items.Count + 1}");
            adapter.NotifyItemInserted(items.Count - 1);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //if (item.ItemId == Android.Resource.Id.Home)
            //{
            //    OnBackPressed();

            //}
            if (item.ItemId == Resource.Id.action_config)
            {
                //Intent intent = new Intent(this, typeof(second_activity));
                //StartActivity(intent);
                return true;
            }
            if (item.ItemId == Resource.Id.action_about)
            {
                Snackbar.Make(layout, "Ver 1.0", Snackbar.LengthShort).Show();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ConfigMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }



        private class RecyclerAdapter : RecyclerView.Adapter
        {
            private IList<string> list = new List<string>();

            public RecyclerAdapter(IList<string> list)
            {
                this.list = list;
            }

            public override int ItemCount => list.Count;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var vh = holder as ViewHolder;
                vh.Update(list[position]);
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_item, parent, false);
                return new ViewHolder(view);
            }

            private class ViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
            {
                private readonly TextView textView;
                public ViewHolder(View itemView) : base(itemView)
                {
                    textView = itemView.FindViewById<TextView>(Resource.Id.txt_pk_value);
                    itemView.SetOnClickListener(this);
                }

                public void OnClick(View v)
                {
                    Toast.MakeText(v.Context, textView.Tag.ToString(), ToastLength.Long).Show();
                }

                internal void Update(string value)
                {
                    textView.Text = value;
                    textView.Tag = value;
                }
            }
        }

        private class SpaceDecorator : RecyclerView.ItemDecoration
        {
            public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
            {
                outRect.Left = 10;
                outRect.Top = 10;
                outRect.Right = 10;
                outRect.Bottom = 10;
            }
        }
    }
}
