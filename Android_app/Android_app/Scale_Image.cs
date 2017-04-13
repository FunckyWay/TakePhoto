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
using Android.Graphics;

namespace Android_app
{
    [Activity(Label = "Scale_Image")]
    public class Scale_Image : Activity, ScaleGestureDetector.IOnScaleGestureListener, View.IOnClickListener
    {
        public Button mButton = null;
        public static SurfaceView mSurfaceView = null;
        public static ISurfaceHolder mSurfaceHolder = null;
        public  ScaleGestureDetector mScaleGestureDetector ;
        public static Bitmap mBitmap = null;
        private static Android.Content.Res.Resources res;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            res = this.Resources;
            
            SetContentView(Resource.Layout.scaleImage);

            mSurfaceView = this.FindViewById<SurfaceView>(Resource.Id.surfaceview);
            mSurfaceHolder = mSurfaceView.Holder;
            mScaleGestureDetector = new ScaleGestureDetector(this, this);
            mButton = this.FindViewById<Button>(Resource.Id.button);
            mButton.SetOnClickListener(this);
            // Create your application here
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            return mScaleGestureDetector.OnTouchEvent(e);
        }
        

        public bool OnScale(ScaleGestureDetector detector)
        {
            Matrix mMatrix = new Matrix();

            float scale = detector.ScaleFactor ;
            mMatrix.SetScale(scale, scale);
            Canvas mCanvas = mSurfaceHolder.LockCanvas();
            mCanvas.DrawColor(Color.Black);
            mCanvas.DrawBitmap(mBitmap, mMatrix, null);
            mSurfaceHolder.UnlockCanvasAndPost(mCanvas);
            mSurfaceHolder.LockCanvas(new Rect(0, 0, 0, 0));
            mSurfaceHolder.UnlockCanvasAndPost(mCanvas);
            return false;
        }

        public bool OnScaleBegin(ScaleGestureDetector detector)
        {
            return true;
        }

        public void OnScaleEnd(ScaleGestureDetector detector)
        {
            //throw new NotImplementedException();
        }

        public void OnClick(View v)
        {
            mBitmap = BitmapFactory.DecodeResource(res, Resource.Drawable.app);
            Canvas mCanvas = mSurfaceHolder.LockCanvas();
            mCanvas.DrawBitmap(mBitmap, 0f, 0f, null);
            mSurfaceHolder.UnlockCanvasAndPost(mCanvas);
            mSurfaceHolder.LockCanvas(new Rect(0, 0, 0, 0));
            mSurfaceHolder.UnlockCanvasAndPost(mCanvas);
        }

    }
}
