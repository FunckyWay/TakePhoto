using Android.App;
using Android.Widget;
using Android.OS;

using Android.Content;
using Android.Provider;
using Android.Graphics;
using System.Collections.Generic;
using Android.Content.PM;
using System;
using Android.Runtime;
using Java.IO;


namespace Android_app
{
    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }

    [Activity(Label = "Android_app", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        
        private ImageView _imageView;
        public static readonly int PickImageId = 1000;
        //用onActivityResult()接收传回的图像，当用户拍完照片或者录像，或者取消后，系统都会调用这个函数。
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if ((requestCode == PickImageId) && (resultCode == Result.Ok) )
            {
                Android.Net.Uri uri = data.Data;
                _imageView.SetImageURI(uri);
            }
            else
            {

            
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            //要存储的目标File
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
            //把它作为URI传给启动的intent
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;

            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null)
            {
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }
            }
            // Dispose of the Java side bitmap.
            GC.Collect();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            
            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button button = FindViewById<Button>(Resource.Id.myButton);
                Button button_ph = FindViewById<Button>(Resource.Id.myButtons);
                Button button_nx = FindViewById<Button>(Resource.Id.myButtone);
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                button.Click += TakeAPicture;
                button_ph.Click += OpenPhoto;
                button_nx.Click += delegate { StartActivity(typeof(Scale_Image)); };
            }
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }


        /// <summary>
        /// 检查设备是否存在SDCard的工具方法
        /// </summary>
        /// <returns></returns>
        public static bool HasSdcard
        {
            get
            {
                String state = Android.OS.Environment.ExternalStorageState;
                if (state.Equals(Android.OS.Environment.MediaMounted))
                    return true;
                else
                    return false;
            }
        }

        private void CreateDirectoryForPictures()
        {
            //Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_PICTURES)
            //指定图像的存储位置，一般图像都是存储在外部存储设备，即SD卡上
            //Context.getExternalFilesDir(Environment.DIRECTORY_PICTURES)
            //返回的路径是和你的应用相关的一个存储图像和视频的方法。如果应用被卸载，这个路径下的东西全都会被删除。
            //string filename =  Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).ToString();
            //App._dir = new File(filename);
            App._dir = new File(
                 Android.OS.Environment.GetExternalStoragePublicDirectory(
                     Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                if (!App._dir.Mkdirs())
                {
                    System.Console.WriteLine("Created Failed");
                };
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            //获取拍照的权限
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            //开始调用系统相机
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            App._file = new File(App._dir.Path, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            //使用startActivityForResult()方法，并传入上面的intent对象。
            //之后，系统自带的相机应用就会启动，用户就可以用它来拍照或者录像
            StartActivityForResult(intent, 0);
        }

        private void OpenPhoto(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }


    }
    //public static class BitmapHelpers
    //{
    //    //裁剪图片
    //    public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
    //    {
    //        // First we get the the dimensions of the file on disk
    //        BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
    //        Bitmap maps =  BitmapFactory.DecodeFile(fileName, options);

    //        // Next we calculate the ratio that we need to resize the image by
    //        // in order to fit the requested dimensions.
    //        int outHeight = options.OutHeight;
    //        int outWidth = options.OutWidth;
    //        int inSampleSize = 1;

    //        if (outHeight > height || outWidth > width)
    //        {
    //            inSampleSize = outWidth > outHeight
    //                               ? outHeight / height
    //                               : outWidth / width;
    //        }

    //        // Now we will load the image and have BitmapFactory resize it for us.
    //        options.InSampleSize = inSampleSize;
    //        options.InJustDecodeBounds = false;
    //        options.InPurgeable = true;
    //        Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

    //        return resizedBitmap;
    //    }
    //}
}

