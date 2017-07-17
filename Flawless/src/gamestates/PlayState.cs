using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using _Flawless.actors;
using _Flawless.actors.enemies;
using _Flawless.math;

namespace _Flawless.gamestates
{
    class PlayState : GameState
    {
        private List<IActable> actors = new List<IActable>();
        private float escPause = 1f;
        private float time;
        private Sprite overlay;

        public PlayState() : this("content\\stages\\Test.bin"){}

        public PlayState(string StagePath)
        {
            actors.Add(Resources.GetPlayer());
            //util.Loader.LoadFromFile(StagePath,actors);
            List<Vector2f> testList = new List<Vector2f>()
            {
                new Vector2f(100,100),
                new Vector2f(150,150),
                new Vector2f(500,0)
            };
            for (float i = 0; i <= 1; i+=0.01f)
            {
                actors.Add(new TestEnemy(i, Maths.bezier(i, testList)));
            }

            overlay = new Sprite(Resources.GetTexture("hud16-9.png"))
            {
                Position = new Vector2f(0, 0),
            };
        }

        public override void Draw(RenderWindow _window)
        {
            _window.Clear(new Color(255,255,255));
            foreach (var act in actors.Where(a => a.StartTime() <= time))
            {
                act.Draw(_window);
            }

        #if DEBUG
            if(Program.Debug)
                return;
        #endif
            _window.Draw(overlay);
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
                Program.States.Push(new PauseState());
                escPause = 2f;
            }
        }
    }
}
