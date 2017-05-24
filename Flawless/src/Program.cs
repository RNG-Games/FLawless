using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using _Flawless.gamestates;

namespace _Flawless
{
    class Program
    {
        public static RenderWindow window;
        public static Stack<GameState> states = new Stack<GameState>();

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(1280, 720), "Window Title"/*, Styles.Fullscreen , new ContextSettings() {AntialiasingLevel = 16 }*/);
            window.Closed += (sender, e) => { var o = sender as Window; o?.Close(); Environment.Exit(0); };
            var text = new Text { Font = Resources.GetFont("trebuc.ttf") };

            // initialize GameTime
            var clock = new Clock();
            states.Push(new MenuState());
            var time = 0f;

            while (states.Count > 0)
            {
                var deltaTime = clock.ElapsedTime.AsSeconds();
                time += deltaTime;
                clock.Restart();
                //text.DisplayedString = time.ToString();

                // update
                var current = states.Peek();
                current.Update(deltaTime);

                // draw
                window.Clear(new Color(100, 149, 237));
                current.Draw(window);

                window.Draw(text); //display deltaTime in seconds
                window.Display();

                window.DispatchEvents();

                // handle states
                if (current.IsFinished) states.Pop();
                if (current.NewState != null){ states.Push(current.NewState);
                    current.NewState = null;
                }
            }
        }

    }
}
