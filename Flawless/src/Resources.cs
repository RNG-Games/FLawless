using System.Collections.Generic;
using SFML.Graphics;

namespace _Flawless
{
	class Resources
	{
		static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
		static Dictionary<string, Font> fonts = new Dictionary<string, Font>();

		public static Texture GetTexture(string textureName)
		{
			if(!textures.ContainsKey(textureName))
			{
				textures.Add(textureName, new Texture("content/texture/" + textureName));
			}
			return textures[textureName];
		}

		public static Font getFont(string name)
		{
			if (!fonts.ContainsKey(name))
			{
				fonts.Add(name, new Font("content/font/" + name));
			}
			return fonts[name];
		}
	}
}
