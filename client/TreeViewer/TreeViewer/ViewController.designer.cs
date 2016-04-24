// WARNING
//
// This file has been generated automatically by Xamarin Studio Community to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TreeViewer
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton btnDraw { get; set; }

		[Outlet]
		AppKit.NSTextField textRootID { get; set; }

		[Outlet]
		TreeViewer.TreeView tree { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnDraw != null) {
				btnDraw.Dispose ();
				btnDraw = null;
			}

			if (textRootID != null) {
				textRootID.Dispose ();
				textRootID = null;
			}

			if (tree != null) {
				tree.Dispose ();
				tree = null;
			}
		}
	}
}
