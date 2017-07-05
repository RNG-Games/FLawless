using System.Collections.Generic;
using System.Security.Policy;
using SFML.Graphics;
using _Flawless.actors;

namespace _Flawless
{
	class Resources
	{
		private static readonly Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();
		private static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();
        private static readonly Dictionary<string, Image> Images = new Dictionary<string, Image>();
	    private static Player _player;

		public static Texture GetTexture(string textureName)
		{
			if(!Textures.ContainsKey(textureName))
				Textures.Add(textureName, new Texture("content/texture/" + textureName));
			return Textures[textureName];
		}

		public static Font GetFont(string fontName)
		{
			if (!Fonts.ContainsKey(fontName))
				Fonts.Add(fontName, new Font("content/font/" + fontName));
			return Fonts[fontName];
		}

	    public static Image GetImage(string imageName)
	    {
	        if(!Images.ContainsKey(imageName))
                Images.Add(imageName, new Image("content/image/" + imageName));
	        return Images[imageName];
	    }

	    public static void NewPlayer()
	    {
	        _player = new Player();
	    }

	    public static Player GetPlayer()
	    {
	        if (_player == null)
	        {
	            NewPlayer();
	        }
	        return _player;
	    }
	}
}
