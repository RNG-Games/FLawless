using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace _Flawless.actors
{
    abstract class TextBox : IActable
    {
        protected Text text; //full text
        protected String currentDisplayed; 
        protected Sprite texture;
        protected Vector2f position;
        protected Font font; // maybe redundant, seemed practical to have here for the unprofessional eye of mine tho

        public TextBox (Vector2f _position)
        {
            position = _position;
        }

        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
            _window.Draw(text);
        }

        public bool IsExpired()
        {
            throw new NotImplementedException();
        }

        public float StartTime()
        {
            throw new NotImplementedException();
        }

        public virtual void Update(float _deltaTime)
        {
            text.DisplayedString = currentDisplayed;

        }
    }
}
