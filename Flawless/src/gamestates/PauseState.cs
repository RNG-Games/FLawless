using SFML.Graphics;
using SFML.Window;

namespace _Flawless.gamestates
{
    class PauseState : GameState
    {

        private Text text = new Text { Font = Resources.getFont("trebuc.ttf") };
        

        public override void Draw(RenderWindow _window)
        {
            _window.Clear(new Color(0,0,0));
            _window.Draw(text);
        }

        public override void Update(float _deltaTime)
        {
            text.DisplayedString = "Press A to go back";
            //TODO: Fix Drücken von Esc um pause aufzurufen beendet es wieder, weil der PC schneller ist als der User
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                this.IsFinished = true;
                return;
            }
        }
    }
}
