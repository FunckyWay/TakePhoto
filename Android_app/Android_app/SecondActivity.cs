using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Views.View;
using Android.Util;

namespace Android_app
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        //Button btn = null;
        //private int mode = 0;
        //float oldDist;
        //float textSize = 0;

         
        //public bool OnTouch(View v, MotionEvent e)
        //{
        //    btn = (Button)v;
        //    if (textSize == 0)
        //    {
        //        textSize = btn.TextSize;
        //    }
        //    switch (e.Action & MotionEventActions.Mask)
        //    {
        //        case MotionEventActions.Down:
        //            mode = 1;
        //            break;
        //        case MotionEventActions.Up:
        //            mode = 0;
        //            break;
        //        case MotionEventActions.PointerUp:
        //            mode -= 1;
        //            break;
        //        case MotionEventActions.PointerDown:
        //            mode += 1;
        //            break;
        //        case MotionEventActions.Move:
        //            if (mode >= 2)
        //            {
        //                float newDist = spacing(e);
        //                if (newDist > oldDist + 1)
        //                {
        //                    zoom(newDist / oldDist);
        //                    oldDist = newDist;
        //                }
        //                if (newDist < oldDist - 1)
        //                {
        //                    zoom(newDist / oldDist);
        //                    oldDist = newDist;
        //                }
        //            }
        //                break;  
        //    }
        //    return true;
        //}

        //private void zoom(float f)
        //{
        //    btn.TextSize = textSize*=f;
        //}
        //private float spacing(MotionEvent e)
        //{
        //    float x = e.GetX(0) - e.GetX(1);
        //    float y = e.GetY(0) - e.GetY(1);
        //    return FloatMath.Sqrt(x * x + y * y);
        //}

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            

            SetContentView(Resource.Layout.Second);
            // Create your application here
        }
    }
  
}