using System;
using System.Collections.Generic;
using UnityEngine;

namespace UICore
{
	public class Palette
	{
		private Dictionary<string, Color> colors;
		private Dictionary<string, Texture2D> textures;
		private string texturesPath;

		/// <summary>
		/// Gets the colors.
		/// </summary>
		/// <value>The colors.</value>
		public Dictionary<string, Color> Colors {
			get {
				return colors;
			}
		}

		/// <summary>
		/// Gets the textures.
		/// </summary>
		/// <value>The textures.</value>
		public Dictionary<string, Texture2D> Textures {
			get {
				return textures;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UICore.Palette"/> class.
		/// </summary>
		public Palette ()
		{
			colors = new Dictionary<string, Color> ();
			textures = new Dictionary<string, Texture2D> ();
		}

		/// <summary>
		/// Parses the config.
		/// </summary>
		/// <param name="config">Config.</param>
		internal void ParseConfig (ConfigNode config) {
			textures.Clear ();
			colors.Clear ();
			texturesPath = config.GetValue ("TexturesPath");
			ParseColors (config.GetNode ("Colors"));
			ParseTextures (config.GetNode ("Textures"));
		}

		/// <summary>
		/// Parses colors from config.
		/// </summary>
		/// <param name="node">Node.</param>
		private void ParseColors (ConfigNode node)
		{
			ConfigNode [] colorNodes = node.GetNodes ();
			foreach (ConfigNode colorNode in colorNodes) {
				string name = colorNode.GetValue ("name");
				Color color = new Color ();
				colorNode.TryGetValue ("value", ref color);
				colors.Add (name, color);

				// Also create 1x1 plain color texture
				Texture2D texture = TextureFromColor (color);
				textures.Add (name, texture);
			}
		}

		/// <summary>
		/// Parses textures from config.
		/// </summary>
		/// <param name="node">Node.</param>
		private void ParseTextures (ConfigNode node)
		{
			ConfigNode [] textureNodes = node.GetNodes ();
			foreach (ConfigNode textureNode in textureNodes) {
				string name = textureNode.GetValue ("name");
				string file = textureNode.GetValue ("file");
				textures.Add (name, LoadTexture (file));
			}
		}

		/// <summary>
		/// Creates 1x1 texture of plain color
		/// </summary>
		/// <returns>The from color.</returns>
		/// <param name="color">Color.</param>
		private Texture2D TextureFromColor (Color color)
		{
			Texture2D texture = new Texture2D (1, 1);
			texture.SetPixel (0, 0, color);
			texture.Apply ();
			return texture;
		}

		/// <summary>
		/// Loads texture from disk
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="filename">Filename.</param>
		private Texture2D LoadTexture (string filename)
		{
			byte [] bytes = System.IO.File.ReadAllBytes (texturesPath + "/" + filename);
			Texture2D texture = new Texture2D (0, 0);
			texture.LoadImage (bytes);
			return texture;
		}

		/// <summary>
		/// Maps color from config by name to color in existing palette.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="node">Node.</param>
		/// <param name="name">Name.</param>
		/// <param name="def">Def.</param>
		internal Color MapColor (ConfigNode node, string name, Color def)
		{
			string color = "null";
			node.TryGetValue (name, ref color);
			if (color == "null")
				return def;
			else
				return colors [color];
		}

		/// <summary>
		/// Returns color by name from palette.
		/// </summary>
		/// <returns>The c.</returns>
		/// <param name="name">Name.</param>
		public Color Col (string name) {
			if (colors.ContainsKey (name))
				return colors [name];
			else
				return Color.red;
		}

		// C# does not support return types overloading???? Fuck this PL!!!

		/// <summary>
		/// Maps texture from config name to texture in existing palette.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="node">Node.</param>
		/// <param name="name">Name.</param>
		internal Texture2D MapTexture (ConfigNode node, string name, Texture2D def)
		{
			string textureName = "null";
			node.TryGetValue (name, ref textureName);
			if (textureName == "null")
				return def;
			else
				return textures [textureName];
		}

		/// <summary>
		/// Returns texture by name from palette.
		/// </summary>
		/// <returns>The tex.</returns>
		/// <param name="name">Name.</param>
		public Texture2D Tex (string name)
		{
			if (textures.ContainsKey (name))
				return textures [name];
			else
				return null;
		}
	}
}
