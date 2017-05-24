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
        protected Sprite texture = new Sprite(Resources.GetTexture("")); //Textbox namen hier einfügen
        protected Sprite portrait;
        protected Vector2f position;
        protected Boolean expiration;

        public TextBox(Vector2f _position, String _text, String _font, String _portrait)
        {
            text = new Text("", Resources.GetFont(_font));
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
            text.DisplayedString = currentDisplayed;
            texture.Position = position;  //Für mögliche Animationen am Konversationsanfang, ggf überflüssig
            text.Position = position;  //muss später noch versetzt werden um die breite des portraits
            portrait.Position = position; //portraits der Charaktere hängen jetzt vorerst in der oberen linken ecke
        }
    }
}