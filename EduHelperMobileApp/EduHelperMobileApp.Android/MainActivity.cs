using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Graphics.Drawable;
using Android.Graphics;
using Com.Github.Mzule.Fantasyslide;
using Android.Support.V4.View;
using Android.Animation;
using Android.Support.V7.App;

namespace EduHelperMobileApp.Droid
{
    [Activity(Label = "EduHelperMobileApp", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, DrawerLayout.IDrawerListener
    {
        private DrawerLayout drawerLayout;
        private DrawerArrowDrawable indicator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            indicator = new DrawerArrowDrawable(this)
            {
                Color = Color.White
            };
            SupportActionBar.SetHomeAsUpIndicator(indicator);

            SetTransformer();
            // setListener();
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            drawerLayout.SetScrimColor(Color.Transparent);
            drawerLayout.AddDrawerListener(this);
        }

        private void SetTransformer()
        {
            float spacing = Resources.GetDimensionPixelSize(Resource.Dimension.mtrl_btn_letter_spacing);
            SideBar rightSideBar = FindViewById<SideBar>(Resource.Id.rightSideBar);
            rightSideBar.SetTransformer(new Transformer(spacing));
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                if (drawerLayout.IsDrawerOpen(GravityCompat.Start))
                {
                    drawerLayout.CloseDrawer(GravityCompat.Start);
                }
                else
                {
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                }
            }
            return true;
        }

        public void OnDrawerClosed(View drawerView)
        {
        }

        public void OnDrawerOpened(View drawerView)
        {
        }

        public void OnDrawerSlide(View drawerView, float slideOffset)
        {
            if (((ViewGroup)drawerView).GetChildAt(1).Id == Resource.Id.leftSideBar)
            {
                indicator.Progress = slideOffset;
            }
        }

        public void OnDrawerStateChanged(int newState)
        {
        }


        public void OnClick(View view)
        {
            if (view is TextView)
            {
                String title = ((TextView)view).Text.ToString();
                if (title.StartsWith("星期"))
                {
                    Toast.MakeText(this, title, ToastLength.Short).Show();
                }
                else
                {
                    //StartActivity(UniversalActivity.newIntent(this, title));
                }
            }
            else if (view.Id == Resource.Id.userInfo)
            {
                //StartActivity(UniversalActivity.newIntent(this, "个人中心"));
            }
        }
    }

    public class Transformer : Java.Lang.Object, ITransformer
    {
        private View lastHoverView;
        private readonly float spacing;

        public Transformer(float spacing)
        {
            this.spacing = spacing;
        }

        public void Apply(ViewGroup sideBar, View itemView, float touchY, float slideOffset, bool isLeft)
        {
            bool hovered = itemView.Pressed;
            if (hovered && lastHoverView != itemView)
            {
                AnimateIn(itemView);
                AnimateOut(lastHoverView);
                lastHoverView = itemView;
            }
        }

        private void AnimateOut(View view)
        {
            if (view == null)
            {
                return;
            }
            ObjectAnimator translationX = ObjectAnimator.OfFloat(view, "translationX", -spacing, 0);
            translationX.SetDuration(200);
            translationX.Start();
        }

        private void AnimateIn(View view)
        {
            ObjectAnimator translationX = ObjectAnimator.OfFloat(view, "translationX", 0, -spacing);
            translationX.SetDuration(200);
            translationX.Start();
        }
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    TabLayoutResource = Resource.Layout.Tabbar;
        //    ToolbarResource = Resource.Layout.Toolbar;

        //    base.OnCreate(savedInstanceState);

        //    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        //    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
        //    LoadApplication(new App());
        //}
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}