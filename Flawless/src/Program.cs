using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static bool fullscreen = false;
        public static float scale;
        public static bool sixteenToNine;
        internal static bool debug = false;

        static void Main(string[] args)
        {
            
            
            sixteenToNine = Properties.Settings.Default.format.Equals("16:9");

            if (sixteenToNine)
            {
                window = new RenderWindow(new VideoMode(1280,720), "Window Title");
                scale = 2 / 3f;
            }
            else
            {
                window = new RenderWindow(new VideoMode(960, 720), "Window Title"/*, Styles.Fullscreen*/);
                scale = 2 / 3f;
            }

            window.Closed += (sender, e) => { var o = sender as Window; o?.Close(); Environment.Exit(0); };
            window.KeyPressed += (sender, e) =>
            {
                if (e.Code == Keyboard.Key.J)
                    debug = !debug;
            };
            var text = new Text { Font = Resources.GetFont("rabelo.ttf") };

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

                window.SetView(new View(new Vector2f(960, 540), new Vector2f(1920, 1080)));
                
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
