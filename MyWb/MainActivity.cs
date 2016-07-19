using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
namespace MyWb
{
	[Activity(Label = "MyWb", MainLauncher = true)]
	public class MyWb : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.MyButton);

			button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

			WebView wv2 = (WebView)this.FindViewById(Resource.Id.webView1);
			wv2.SetWebViewClient(new WebViewClient());
			wv2.SetWebChromeClient(new MyWCC(this));
			wv2.Settings.JavaScriptEnabled = true;
			wv2.LoadUrl("http://www.script-tutorials.com/demos/199/index.html");


		}
	}

	partial class MyWCC : WebChromeClient
	{
		private IValueCallback mUploadMessage;
		private static int FILECHOOSER_RESULTCODE = 1;
		private MyWb thisActivity = null;


		public MyWCC(MyWb context)
		{
			thisActivity = context;
		}

		// For Android 3.0+
		// public override void OpenFileChooser(Android.Webkit.IValueCallback uploadMsg, String acceptType)
		// {
		//mUploadMessage = uploadMsg;
		//Intent i = new Intent(Intent.ActionGetContent);
		//i.AddCategory(Intent.CategoryOpenable);
		//i.SetType("*/*");
		// thisActivity.StartActivityForResult(Intent.CreateChooser(i, "File Browser"),
		//FILECHOOSER_RESULTCODE);
		//}

		//For Android 4.1
		[Java.Interop.Export]
		public void OpenFileChooser(IValueCallback uploadMsg, Java.Lang.String acceptType, Java.Lang.String capture)
		{
			mUploadMessage = uploadMsg;
			Intent i = new Intent(Intent.ActionGetContent);
			i.AddCategory(Intent.CategoryOpenable);
			i.SetType("image/*");
			thisActivity.StartActivityForResult(Intent.CreateChooser(i, "File Chooser"), FILECHOOSER_RESULTCODE);

		}


	}
}


