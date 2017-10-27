using System;
using System.Collections.Generic;
using UnityEngine;
using KSP.UI;

namespace UICore
{
	public class Skin
	{
		private Palette palette;
		Font ArialFont = (Font)Resources.GetBuiltinResource (typeof (Font), "Arial.ttf");

		internal Dictionary<string, GUIStyle> styles;
		public Dictionary<string, GUIStyle> Styles {
			get {
				return styles;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UICore.Skin"/> class.
		/// </summary>
		/// <param name="palette">Palette.</param>
		public Skin (Palette palette)
		{
			this.palette = palette;
			styles = new Dictionary<string, GUIStyle> ();
		}

		/// <summary>
		/// Parses the config.
		/// </summary>
		/// <param name="config">Config.</param>
		public void ParseConfig (ConfigNode config, string skinName)
		{
			styles.Clear ();
			ConfigNode [] stylesNode = config.GetNode ("Styles").GetNodes ();
			foreach (ConfigNode styleNode in stylesNode) {
				string name = styleNode.GetValue ("name");
				string parent = "null";
				styleNode.TryGetValue ("parent", ref parent);
				GUIStyle parentStyle = null;
				if (styles.ContainsKey (parent))
					parentStyle = styles [parent];

				GUIStyle style = ParseGUIStyle (styleNode, skinName, parentStyle);
				styles.Add (name, style);
			}			
		}


		private GUIStyle ParseGUIStyle (ConfigNode node, string skinName, GUIStyle other = null)
		{
			GUIStyle result;
			if (other == null)
				result = new GUIStyle ();
			else
				result = new GUIStyle (other);

			result.name = skinName + node.GetValue ("name");
			result.font = ArialFont;

			result.normal.background = palette.MapTexture (node, "normal.background", result.normal.background);
			result.normal.textColor = palette.MapColor (node, "normal.textColor", result.normal.textColor);

			result.hover.background = palette.MapTexture (node, "hover.background", result.hover.background);
			result.hover.textColor = palette.MapColor (node, "hover.textColor", result.hover.textColor);

			result.active.background = palette.MapTexture (node, "active.background", result.active.background);
			result.active.textColor = palette.MapColor (node, "active.textColor", result.active.textColor);

			result.onNormal.background = palette.MapTexture (node, "onNormal.background", result.onNormal.background);
			result.onNormal.textColor = palette.MapColor (node, "onNormal.textColor", result.onNormal.textColor);

			result.onHover.background = palette.MapTexture (node, "onHover.background", result.onHover.background);
			result.onHover.textColor = palette.MapColor (node, "onHover.textColor", result.onHover.textColor);

			result.onActive.background = palette.MapTexture (node, "onActive.background", result.onActive.background);
			result.onActive.textColor = palette.MapColor (node, "onActive.textColor", result.onActive.textColor);

			result.onActive.background = palette.MapTexture (node, "focused.background", result.focused.background);
			result.onActive.textColor = palette.MapColor (node, "focused.textColor", result.focused.textColor);

			result.onActive.background = palette.MapTexture (node, "onFocused.background", result.onFocused.background);
			result.onActive.textColor = palette.MapColor (node, "onFocused.textColor", result.onFocused.textColor);

			result.border = ParseRectOffset (node, "border", result.border);
			result.margin = ParseRectOffset (node, "margin", result.margin);
			result.padding = ParseRectOffset (node, "padding", result.padding);
			result.overflow = ParseRectOffset (node, "overflow", result.overflow);

			result.imagePosition = ParseEnum<ImagePosition> (node, "imagePosition", result.imagePosition);

			result.alignment = ParseEnum<TextAnchor> (node, "alignment", result.alignment);
			result.wordWrap = ParseBool (node, "wordWrap", result.wordWrap);
			result.clipping = ParseEnum<TextClipping> (node, "clipping", result.clipping);

			result.contentOffset = ParseVector2 (node, "contentOffset", result.contentOffset);

			result.fixedWidth = ParseFloat (node, "fixedWidth", result.fixedWidth);
			result.fixedHeight = ParseFloat (node, "fixedHeight", result.fixedHeight);

			result.stretchWidth = ParseBool (node, "stretchWidth", result.stretchWidth);
			result.stretchHeight = ParseBool (node, "stretchHeight", result.stretchHeight);

			result.fontSize = ParseInt (node, "fontSize", result.fontSize);
			result.fontStyle = ParseEnum<FontStyle> (node, "fontStyle", result.fontStyle);

			result.richText = ParseBool (node, "richText", result.richText);

			return result;
		}

		/// <summary>
		/// Parses int value from config.
		/// </summary>
		/// <returns>The int.</returns>
		/// <param name="node">Node.</param>
		/// <param name="name">Name.</param>
		private int ParseInt (ConfigNode node, string name, int def)
		{
			int result = def;
			node.TryGetValue (name, ref result);
			return result;
		}

		/// <summary>
		/// Parses float value from config.
		/// </summary>
		/// <returns>The float.</returns>
		/// <param name="node">Node.</param>
		/// <param name="name">Name.</param>
		/// <param name="def">Def.</param>
		private float ParseFloat (ConfigNode node, string name, float def)
		{
			float result = def;
			node.TryGetValue (name, ref result);
			return result;
		}

		/// <summary>
		/// Parses Enum of given type from config.
		/// </summary>
		/// <returns>The enum.</returns>
		/// <param name="node">Node.</param>
		/// <param name="name">Name.</param>
		private T ParseEnum<T> (ConfigNode node, string name, T def)
		{
			T result = def;
			string value = "";

			if (node.TryGetValue (name, ref value)) {
				result = (T)Enum.Parse (typeof (T), node.GetValue (name));
			} else {
				result = def;
			}
			return result;
		}

		/// <summary>
		/// Parses the rect offset. Tricky part as we actually parse Vector4 and convert it to RectOffset
		/// </summary>
		/// <returns>The rect offset.</returns>
		/// <param name="node">Node.</param>
		/// <param name="name">Name.</param>
		private RectOffset ParseRectOffset (ConfigNode node, string name, RectOffset def)
		{
			Vector4 rect = new Vector4 (def.left, def.right, def.top, def.bottom);
			node.TryGetValue (name, ref rect);
			return new RectOffset (
				Convert.ToInt32 (rect.x),
				Convert.ToInt32 (rect.y),
				Convert.ToInt32 (rect.z),
				Convert.ToInt32 (rect.w)
			);
		}

		/// <summary>
		/// Parses bool from config.
		/// </summary>
		/// <returns><c>true</c>, if bool was parsed, <c>false</c> otherwise.</returns>
		/// <param name="node">Node.</param>
		/// <param name="name">Name.</param>
		/// <param name="def">If set to <c>true</c> def.</param>
		private bool ParseBool (ConfigNode node, string name, bool def)
		{
			bool result = def;
			node.TryGetValue (name, ref result);
			return result;
		}

		/// <summary>
		/// Parses Vector2 from config.
		/// </summary>
		/// <returns>The vector2.</returns>
		/// <param name="node">Node.</param>
		/// <param name="name">Name.</param>
		/// <param name="def">Def.</param>
		private Vector2 ParseVector2 (ConfigNode node, string name, Vector2 def)
		{
			Vector2 result = def;
			node.TryGetValue (name, ref result);
			return result;
		}
	}
}
