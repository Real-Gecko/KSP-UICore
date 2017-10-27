using System;
using System.Collections.Generic;
using UnityEngine;

namespace UICore
{
	public class UICore
	{
		#region Members

		private ConfigNode config;
		private Palette palette;
		private Skin skin;
		private Layout layout;

		#endregion

		#region Getters

		public Palette Palette { get { return palette; } }
		public Skin Skin { get { return skin; } }
		public Layout Layout { get { return layout; } }

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UICore.UICore"/> class.
		/// </summary>
		public UICore ()
		{
			palette = new Palette ();
			skin = new Skin (palette);
			layout = new Layout (skin, palette);
		}

		/// <summary>
		/// Loads the config from file.
		/// </summary>
		/// <param name="filename">Filename.</param>
		public void LoadConfig (string filename)
		{
			config = ConfigNode.Load (filename).GetNode ("UICoreSkin");
			string skinName = config.GetValue ("Name");
			palette.ParseConfig (config.GetNode ("Palette"));
			skin.ParseConfig (config.GetNode ("Skin"), skinName);
			layout.Init ();
		}

		/// <summary>
		/// Loads the config from ConfigNode.
		/// </summary>
		/// <param name="config">Config.</param>
		public void LoadConfig (ConfigNode config)
		{
			this.config = config;
			string skinName = config.GetValue ("Name");
			palette.ParseConfig (config.GetNode ("Palette"));
			skin.ParseConfig (config.GetNode ("Skin"), skinName);
			layout.Init ();
		}

		/// <summary>
		/// Log the specified message.
		/// </summary>
		/// <returns>The log.</returns>
		/// <param name="message">Message.</param>
		internal void Log (string message)
		{
			Debug.Log ("UICore.Debug: " + message);
		}
	}
}
