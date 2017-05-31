using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace _Flawless.actors
{
    public class TextBox : IActable
    {
        protected Text text; 
        protected String toDisplay;
        protected Sprite texture = new Sprite(Resources.GetTexture("TestTB.png")); 
        protected Sprite portrait;
        protected Vector2f position;
        protected Boolean expiration;
        protected float frameCounter;

        public TextBox(Vector2f _position, String _text, String _font, String _portrait)
        {
            text = new Text("", Resources.GetFont(_font));
            text.Color = new Color(0, 0, 0);
            toDisplay = _text;
            position = _position;
            expiration = false;
            portrait = new Sprite(Resources.GetTexture(_portrait));
        }

        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
            _window.Draw(portrait);
            _window.Draw(text);
        }

        public bool IsExpired()
        {
            return expiration;
        }

        public float StartTime()
        {
            return 0; //vorläufig zur exeption vermeidung
        }


        public virtual void Update(float _deltaTime)
        {
            frameCounter += _deltaTime;
            if (frameCounter > 0.2 && toDisplay.Length > 0)
            {
                text.DisplayedString += toDisplay.ElementAt(0);
                toDisplay = toDisplay.Substring(1);  //adjust toDisplay
                frameCounter = 0;
            }
            texture.Position = position;  //Für mögliche Animationen am Konversationsanfang, ggf überflüssig
            text.Position = new Vector2f (position.X + 100, position.Y + 20);  
            portrait.Position = new Vector2f(position.X + -25, position.Y -25); //portraits der Charaktere hängen jetzt vorerst in der oberen linken ecke
        }
    }
}