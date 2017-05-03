using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace _Flawless.gamestates
{
    class MenuState : GameState
    {
        Sprite cursor = new Sprite(Resources.GetTexture("pixel.png"));
        Text text = new Text(){Font = Resources.getFont("trebuc.ttf") };

        public override void Draw(RenderWindow _window)
        {
            _window.Clear(new Color(0,0,0));
            _window.SetMouseCursorVisible(false);
            cursor.Scale = new Vector2f(16, 16);
            cursor.Position = (Vector2f) Mouse.GetPosition(_window);
            _window.Draw(text);
            _window.Draw(cursor);
        }

        public override void Update(float _deltaTime)
        {
            text.DisplayedString = "Menu \n\n\nPress \"A\" to Test \n\nPress \"Esc\" to Exit";
            cursor.Position = new Vector2f(Mouse.GetPosition().X, Mouse.GetPosition().Y);
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                this.NewState = new PlayState();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                this.IsFinished = true;
        }
    }
}
