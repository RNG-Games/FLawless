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
		static RenderWindow window;
		static Stack<GameState> states = new Stack<GameState>();

		static void Main(string[] args)
		{
			window = new RenderWindow(new VideoMode(1366, 768), "Window Title"/*, Styles.Fullscreen , new ContextSettings() {AntialiasingLevel = 16 }*/);
		    window.Closed += (sender, e) =>{ var o = sender as Window; o?.Close(); Environment.Exit(0);};

		    var text = new Text {Font = Resources.getFont("trebuc.ttf")};

		    // initialize GameTime
			var clock = new Clock();
			states.Push(new MainState());

			while (states.Count > 0)
			{
				var deltaTime = clock.ElapsedTime.AsSeconds();
				clock.Restart();
				text.DisplayedString = deltaTime.ToString();

				GameState current = states.Peek();
				current.Update(deltaTime);
				// draw
				window.Clear(new Color(100, 149, 237));

                

				current.Draw(window);
				//window.Draw(text); //display deltaTime in seconds
				window.Display();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    states.Push(new PauseState());
                window.DispatchEvents();

				// handle states
				if (current.IsFinished) states.Pop();
				if (current.NewState != null) states.Push(current.NewState);
			}
		}

	}
}
