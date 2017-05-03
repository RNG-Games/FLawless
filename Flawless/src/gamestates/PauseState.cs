using SFML.Graphics;
using SFML.Window;

namespace _Flawless.gamestates
{
    class PauseState : GameState
    {
        private Text text = new Text { Font = Resources.getFont("trebuc.ttf") };
        private bool escPressed = true;

        public override void Draw(RenderWindow _window)
        {
            _window.Clear(new Color(0,0,0));
            _window.Draw(text);
        }

        public override void Update(float _deltaTime)
        {
            if (!Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                escPressed = false;

            text.DisplayedString = "Pause \n\n\nPress Esc to go back";
            if (!escPressed && Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                this.IsFinished = true;
                
        }
    }
}
