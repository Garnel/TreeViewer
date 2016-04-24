using System;
using AppKit;
using Newtonsoft.Json.Linq;
using CoreGraphics;
using System.Collections.Generic;

namespace TreeViewer
{
	[Foundation.Register ("TreeView")]
	public class TreeView: NSControl
	{
		private void Initialize() {
			this.WantsLayer = true;
			this.LayerContentsRedrawPolicy = NSViewLayerContentsRedrawPolicy.OnSetNeedsDisplay;

			NodeWidth = 80;
			NodeHeight = 80;
			NodeRadius = 20;

			_nodes = new List<CGPoint> ();
			_paths = new List<Tuple<CGPoint, CGPoint>> ();
		}

		public TreeView (IntPtr handle) : base (handle)
		{
			Initialize();
		}

		#region Private variables
		private string _treeString;
		private JObject _treeData;
		private const string NODE_WIDTH_TAG = "__WIDTH";
		private const string NODE_HEIGHT_TAG = "__HEIGHT";
		private List<CGPoint> _nodes;
		private List<Tuple<CGPoint, CGPoint>> _paths;
		#endregion

		#region Computer properties
		public string TreeString {
			get { return _treeString; }
			set {
				if (_treeString != value) {
					_treeString = value;
					initTree ();
					NeedsDisplay = true;
				}
			}
		}

		public int NodeWidth { get; set; }
		public int NodeHeight { get; set; }
		public int TreeWidth { get; private set; }
		public int TreeHeight { get; private set; }
		public int NodeRadius { get; set; }

		#endregion

		private void initTree() {
			parseTreeString();
			TreeWidth = calcTreeWidth(_treeData);
			TreeHeight = calcTreeHeight(_treeData);

			_nodes.Clear ();
			_paths.Clear ();

			calcTreePos (_treeData, new CGPoint(0, 0));
		}

		private JObject parseTreeString() {
			_treeData = JObject.Parse(_treeString);
			return _treeData;
		}

		private int calcTreeWidth(JObject root) {
			if (root == null) {
				return 0;
			}

			// no children, the leaf node
			if (root ["children"] == null) {
				root.Add (NODE_WIDTH_TAG, NodeWidth);
				return NodeWidth;
			}

			int childrenWidth = 0;
			foreach (var child in root["children"]) {
				childrenWidth += calcTreeWidth (child.Value<JObject>());
			}
			root.Add(NODE_WIDTH_TAG, Math.Max (childrenWidth, NodeWidth));
			return Math.Max (childrenWidth, NodeWidth);
		}

		private int calcTreeHeight(JObject root) {
			if (root == null) {
				return 0;
			}

			// leaf
			if (root ["children"] == null) {
				root.Add(NODE_HEIGHT_TAG, NodeHeight);
				return NodeHeight;
			}

			int childrenHeight = 0;
			foreach (var child in root["children"]) {
				int height = calcTreeHeight (child.Value<JObject>());
				childrenHeight = Math.Max (height, childrenHeight);
			}
			root.Add (NODE_HEIGHT_TAG, NodeHeight + childrenHeight);
			return NodeHeight + childrenHeight;
		}

		private CGPoint calcTreePos (JObject root, CGPoint origin) {
			if (root == null) {
				return origin;
			}

			int width = (int)root [NODE_WIDTH_TAG];
			int height = (int)root [NODE_HEIGHT_TAG];

			// root
			CGPoint rootPos = new CGPoint(origin.X + width / 2, origin.Y + NodeHeight / 2);
			_nodes.Add (rootPos);
			if (root ["children"] != null) {
				var curX = origin.X;
				var curY = origin.Y + NodeHeight;
				foreach (var child in root["children"]) {
					JObject childObj = child.Value<JObject> ();
					CGPoint childPos = calcTreePos(childObj, new CGPoint(curX, curY));
					// add path
					_paths.Add(new Tuple<CGPoint, CGPoint>(rootPos, childPos)); 
					int childWidth = (int)childObj [NODE_WIDTH_TAG];
					curX += childWidth;
				}
			}

			return rootPos;
		}

		public override void DrawRect (CGRect dirtyRect)
		{
			base.DrawRect (dirtyRect);

			// Use Core Graphic routines to draw our UI
			NSBezierPath path = new NSBezierPath();
			foreach (var p in _paths) {
				path.MoveTo (p.Item1);
				path.LineTo (p.Item2);
			}

			foreach (var n in _nodes) {
				path.AppendPathWithOvalInRect (new CGRect (
					n.X - NodeRadius, n.Y - NodeRadius,
					2 * NodeRadius, 2 * NodeRadius));
			}

			NSColor.Cyan.SetStroke ();
			path.Stroke ();
			NSColor.Orange.SetFill ();
			path.Fill ();
		}

	}
}

