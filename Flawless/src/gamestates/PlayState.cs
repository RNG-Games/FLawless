using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using _Flawless.actors;

namespace _Flawless.gamestates
{
    class PlayState : GameState
    {
        //private Player _player;
        private List<IActable> actors = new List<IActable>();
        private float escPause = 2f;
        private float time = 0f;

        public PlayState() : this("")
        {
            actors.Add(Resources.GetPlayer());
            Resources.GetPlayer().position = new Vector2f(Program.window.Size.X / 2.0f, 500);
            actors.Add(EnemyFactory.GetEnemy("A",100,100));
            actors.Add(EnemyFactory.GetEnemy("A",200,200));
        }

        public PlayState(string StagePath)
        {
            
        }

        public override void Draw(RenderWindow _window)
        {
            _window.Clear(new Color(100,100,100));
            foreach (var act in actors.Where(a => a.StartTime() <= time))
            {
                act.Draw(_window);
            }
          
        }

        public override void Update(float _deltaTime)
        {
            escPause -= _deltaTime;
            time += _deltaTime;

            actors = actors.Where(a => !a.IsExpired()).ToList();
            foreach (var act in actors.Where(a => a.StartTime() <= time))
            {
                act.Update(_deltaTime);
            }

            if (escPause <= 0f && Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Program.states.Push(new PauseState());
                escPause = 2f;
            }
        }
    }
}
