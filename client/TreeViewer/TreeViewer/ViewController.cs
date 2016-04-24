using System;

using AppKit;
using Foundation;

namespace TreeViewer
{
	public partial class ViewController : NSViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view.
		}

		public override NSObject RepresentedObject {
			get {
				return base.RepresentedObject;
			}
			set {
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

		[Action ("updateTree:")]
		private void updateTree (AppKit.NSButton sender) {
			string treeString = @"
{
  ""id"": 1,
  ""name"": ""root"",
  ""description"": ""description"",
  ""children"": [
    {
      ""id"": 2,
      ""name"": ""level1"",
      ""description"": ""description"",
      ""children"": [
        {
          ""id"": 4,
          ""name"": ""level22222"",
          ""description"": ""This is level2""
        },
        {
          ""id"": 5,
          ""name"": ""level222"",
          ""description"": ""This is level2"",
          ""children"": [
            {
              ""id"": 10,
              ""name"": ""level333"",
              ""description"": ""Here are a few key resources to get you started with building mobile apps quickly"",
              ""children"": [
                {
                  ""id"": 11,
                  ""name"": ""level444444"",
                  ""description"": ""Introduces Android development. Covers the tool chain, Xamarin.Android projects, and Android fundamentals.""
                },
                {
                  ""id"": 12,
                  ""name"": ""level 4444"",
                  ""description"": ""This guide walks through the installation steps and configuration details required to install Xamarin.Android on Windows.""
                }
              ]
            }
          ]
        }
      ]
    },
    {
      ""id"": 3,
      ""name"": ""level1111"",
      ""description"": ""description"",
      ""children"": [
        {
          ""id"": 6,
          ""name"": ""level2222222"",
          ""description"": ""Never know this level: level2, 233333""
        }
      ]
    },
    {
      ""id"": 3,
      ""name"": ""level111111111"",
      ""description"": ""description"",
      ""children"": [
        {
          ""id"": 7,
          ""name"": ""level22222222222"",
          ""description"": ""blahblahblah""
        },
        {
          ""id"": 8,
          ""name"": ""level222"",
          ""description"": ""The AppDelegate.cs file contains our AppDelegate class""
        },
        {
          ""id"": 9,
          ""name"": ""level two"",
          ""description"": ""which is responsible for creating our window and listening to OS events""
        }
      ]
    }
  ]
}";

			tree.TreeString = treeString;
		}
	}
}
