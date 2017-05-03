using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using _Flawless.actors;
using _Flawless;

namespace _Flawless.gamestates
{
    class PlayState : GameState
    {
        private Player _player;

        private float escPause = 2f;

        public PlayState() : this(""){ }

        public PlayState(string StagePath)
        {
            _player = Resources.GetPlayer();
        }

        public override void Draw(RenderWindow _window)
        {
            _player.Draw(_window);
        }

        public override void Update(float _deltaTime)
        {
            escPause -= _deltaTime;
            _player.Update(_deltaTime);
            if (escPause <= 0f && Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Program.states.Push(new PauseState());
                escPause = 2f;
            }
        }
    }
}
