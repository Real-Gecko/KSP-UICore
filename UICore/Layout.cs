using System;
using UnityEngine;

namespace UICore
{
	public class Layout
	{
		private Skin skin;
		private Palette palette;
		private GUIStyle labelStyle;
		private GUIStyle buttonStyle;

		public Layout (Skin skin, Palette palette)
		{
			this.skin = skin;
			this.palette = palette;
		}

		internal void Init ()
		{
			labelStyle = skin.styles ["label"];
			buttonStyle = skin.styles ["button"];
		}

		/// <summary>
		/// Styled label with white text color.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="options">Options.</param>
		public void Label (string text, params GUILayoutOption [] options)
		{
			labelStyle.normal.textColor = Color.white;
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.stretchWidth = false;
			GUILayout.Label (text, labelStyle, options);
		}

		/// <summary>
		/// Styled label with text color accepted as argument.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="color">Color.</param>
		/// <param name="options">Options.</param>
		public void Label (string text, Color color, params GUILayoutOption [] options)
		{
			labelStyle.normal.textColor = color;
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.stretchWidth = false;
			GUILayout.Label (text, labelStyle, options);
		}

		/// <summary>
		/// Styled label with center text alignment
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="options">Options.</param>
		public void LabelCentered (string text, params GUILayoutOption [] options)
		{
			labelStyle.normal.textColor = Color.white;
			labelStyle.alignment = TextAnchor.MiddleCenter;
			labelStyle.stretchWidth = true;
			GUILayout.Label (text, labelStyle, options);
		}

		/// <summary>
		/// Styled label with center text alignmet and color
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="color">Color.</param>
		/// <param name="options">Options.</param>
		public void LabelCentered (string text, Color color, params GUILayoutOption [] options)
		{
			labelStyle.normal.textColor = color;
			labelStyle.alignment = TextAnchor.MiddleCenter;
			labelStyle.stretchWidth = true;
			GUILayout.Label (text, labelStyle, options);
		}

		/// <summary>
		/// Styled label with text aligned to the right
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="options">Options.</param>
		public void LabelRight (string text, params GUILayoutOption [] options)
		{
			labelStyle.normal.textColor = Color.white;
			labelStyle.alignment = TextAnchor.MiddleRight;
			labelStyle.stretchWidth = false;
			GUILayout.Label (text, labelStyle, options);
		}

		/// <summary>
		/// Styled label with colored text aligned to the right
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="color">Color.</param>
		/// <param name="options">Options.</param>
		public void LabelRight (string text, Color color, params GUILayoutOption [] options)
		{
			labelStyle.normal.textColor = color;
			labelStyle.alignment = TextAnchor.MiddleRight;
			labelStyle.stretchWidth = false;
			GUILayout.Label (text, labelStyle, options);
		}

		/// <summary>
		/// Styled button with white text color
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="options">Options.</param>
		public bool Button (string text, params GUILayoutOption [] options)
		{
			buttonStyle.normal.textColor = Color.white;
			buttonStyle.alignment = TextAnchor.MiddleCenter;
			buttonStyle.stretchWidth = true;
			return GUILayout.Button (text, buttonStyle, options);
		}

		/// <summary>
		/// Styled button with text color accepted as argument.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="color">Color.</param>
		/// <param name="options">Options.</param>
		public bool Button (string text, Color color, params GUILayoutOption [] options)
		{
			buttonStyle.normal.textColor = color;
			buttonStyle.alignment = TextAnchor.MiddleCenter;
			buttonStyle.stretchWidth = true;
			return GUILayout.Button (text, buttonStyle, options);
		}

		/// <summary>
		/// Styled button with text aligned to the left
		/// </summary>
		/// <returns><c>true</c>, if left was buttoned, <c>false</c> otherwise.</returns>
		/// <param name="text">Text.</param>
		/// <param name="options">Options.</param>
		public bool ButtonLeft (string text, params GUILayoutOption [] options)
		{
			buttonStyle.normal.textColor = Color.white;
			buttonStyle.alignment = TextAnchor.MiddleLeft;
			buttonStyle.stretchWidth = true;
			return GUILayout.Button (text, buttonStyle, options);
		}

		/// <summary>
		/// Styled button with text aligned to the left and color as argument
		/// </summary>
		/// <returns><c>true</c>, if left was buttoned, <c>false</c> otherwise.</returns>
		/// <param name="text">Text.</param>
		/// <param name="color">Color.</param>
		/// <param name="options">Options.</param>
		public bool ButtonLeft (string text, Color color, params GUILayoutOption [] options)
		{
			buttonStyle.normal.textColor = color;
			buttonStyle.alignment = TextAnchor.MiddleLeft;
			buttonStyle.stretchWidth = true;
			return GUILayout.Button (text, buttonStyle, options);
		}

		/// <summary>
		/// Creates label with "label: text" with different colors in one line
		/// </summary>
		/// <param name="label">Label.</param>
		/// <param name="text">Text.</param>
		public void LabelAndText (string label, string text, Color lColor, Color tColor)
		{
			GUILayout.BeginHorizontal ();
			Label (label + ": ", lColor);
			Label (text, tColor);
			GUILayout.EndHorizontal ();
		}

		/// <summary>
		/// Margin with the specified width.
		/// </summary>
		/// <param name="width">Width.</param>
		public void Margin (int width)
		{
			GUILayout.Label ("", labelStyle, GUILayout.Width (width));
		}

		/// <summary>
		/// Styled scrollview
		/// </summary>
		/// <returns>The scroll view.</returns>
		/// <param name="scrollPos">Scroll position.</param>
		/// <param name="options">Options.</param>
		public Vector2 BeginScrollView (Vector2 scrollPos, params GUILayoutOption [] options)
		{
			return GUILayout.BeginScrollView (
				scrollPos,
				false,
				true,
				skin.Styles["verticalScrollbarThumb"],
				skin.Styles["verticalScrollbarThumb"],
				skin.Styles["scrollView"],
				options
			);	            
		}

		/// <summary>
		/// Horizontal separator of the specified height
		/// </summary>
		public void HR (int height = 20)
		{
			GUILayout.Label ("", labelStyle, GUILayout.Height (height));
		}

		/// <summary>
		/// Selection Grid.
		/// </summary>
		/// <returns>The grid.</returns>
		/// <param name="selected">Selected.</param>
		/// <param name="captions">Captions.</param>
		/// <param name="count">Count.</param>
		/// <param name="options">Options.</param>
		public int SelectionGrid (int selected, string [] captions, int count, params GUILayoutOption [] options)
		{
			return GUILayout.SelectionGrid (
				selected,
				captions,
				count,
				skin.Styles["selectionGrid"],
				options
			);
		}

		public bool Toggle (bool value, string text, params GUILayoutOption [] options)
		{
			string prefix = value ? "● " : "○ ";
			return GUILayout.Toggle (value, prefix + text, skin.Styles["toggle"], options);
		}

		/// <summary>
		/// Styled window
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="screenRect">Screen rect.</param>
		/// <param name="func">Func.</param>
		/// <param name="title">Title.</param>
		/// <param name="options">Options.</param>
		public Rect Window (int id, Rect screenRect, GUI.WindowFunction func, string title, params GUILayoutOption [] options)
		{
			// Fix rect width and height not being integers to avoid blurry font rendering
			screenRect.width = (float)Math.Floor (screenRect.width);
			screenRect.height = (float)Math.Floor (screenRect.height);
			return GUILayout.Window (id, screenRect, func, title, skin.Styles ["window"], options);
		}
	}
}
