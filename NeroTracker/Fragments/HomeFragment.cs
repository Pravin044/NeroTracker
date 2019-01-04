using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace NeroTracker.Fragments
{
    internal class HomeFragment : Fragment
    {
        private View view;

        private IList<string> items = new List<string>() { "Sample1", "Sample2" };
        private RecyclerAdapter adapter;
        private View layout;
        private SwipeRefreshLayout swipeLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.homepageActivity, container, false);
            var button = view.FindViewById<FloatingActionButton>(Resource.Id.button1);
            button.Click += Button_Click;
            var listView = view.FindViewById<RecyclerView>(Resource.Id.list_view);
            listView.SetLayoutManager(new GridLayoutManager(Context, 2));
            adapter = new RecyclerAdapter(items);
            listView.SetAdapter(adapter);
            layout = view.FindViewById(Resource.Id.layout_main);
            listView.AddItemDecoration(new SpaceDecorator());
            swipeLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_layout);
            swipeLayout.Refresh += SwipeLayout_Refresh;
            return view;
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