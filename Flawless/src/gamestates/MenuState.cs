using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace _Flawless.gamestates
{
	internal enum MenuItems
	{
		#if DEBUG
		Teststage = 0,
		#endif
		Play = 1,
		Exit = 2
	}

	class MenuState : GameState
    {
	    private Text text;
	    private Text nextText;
	    private bool nextDraw = false;
	    private Vector2f textPos;
	    private MenuItems currentChoice;
	    private Queue<int> itemChanges;
	    private bool moving = false;
	    private float distNext;
	    private float distText;
		private const float itemSpeed = 0.5f;

	    public MenuState()
	    {
		    text = new Text(){Font = Resources.GetFont("rabelo.ttf")};
		    text.DisplayedString = "AAA";
			text.Origin = new Vector2f(text.GetLocalBounds().Width / 2f, text.GetLocalBounds().Height / 2f);
			nextText = new Text(){Font = Resources.GetFont("rabelo.ttf")};
			#if DEBUG
			currentChoice = MenuItems.Teststage;
			#else
			currentChoice = MenuItems.Play;
			#endif
			itemChanges = new Queue<int>();
		}

        public override void Draw(RenderWindow _window)
        {
            _window.Clear(new Color(0,0,0));
            _window.SetMouseCursorVisible(false);
            _window.Draw(text);
			if(nextDraw)
				_window.Draw(nextText);
        }

        public override void Update(float _deltaTime)
        {
	        text.DisplayedString = getText(currentChoice);
	        text.Origin = new Vector2f(text.GetLocalBounds().Width / 2f, text.Origin.Y);
	        text.Position = Program.Window.GetView().Center;

			if (itemChanges.Count > 0)
	        {
		        if (itemChanges.Peek() > 0)
		        {
			        nextItem(_deltaTime);
		        }
				else if (itemChanges.Peek() < 0)
		        {
			        previousItem(_deltaTime);
		        }
	        }
	        text.DisplayedString = getText(currentChoice);

		}

	    private void previousItem(float _deltaTime)
	    {
			#if DEBUG
		    if (!moving && currentChoice == MenuItems.Teststage)
		    {
				itemChanges.Dequeue();
			    return;
		    }
			#else
			if(!moving && currentChoice == MenuItems.Play)
			{
				itemChanges.Dequeue();
				return;
			}
			#endif

		    if (!moving)
		    {
			    nextText.DisplayedString = getText(currentChoice);
			    nextText.Origin = text.Origin;
			    nextText.Position = text.Position;
			    nextDraw = true;

				currentChoice = currentChoice - 1;

			    text.DisplayedString = getText(currentChoice);
				text.Origin = new Vector2f(text.GetLocalBounds().Width / 2f, text.Origin.Y);
			    textPos = new Vector2f(0 - text.Origin.X, text.Position.Y);
			    text.Position = textPos;

			    distNext = (Program.Window.GetView().Size.X + nextText.Origin.X - nextText.Position.X) / itemSpeed;
			    distText = (Program.Window.GetView().Center.X - textPos.X) / itemSpeed;

				moving = true;
		    }

			if ((int)textPos.X < (int)Program.Window.GetView().Center.X)
		    {
			    textPos += new Vector2f(distText * _deltaTime, 0);
			    text.Position = textPos;
		    }

		    if ((int)nextText.Position.X < (int)(Program.Window.GetView().Size.X + nextText.Origin.X))
		    {
			    nextText.Position += new Vector2f(distNext * _deltaTime, 0);
		    }

		    if ((int)textPos.X >= (int)Program.Window.GetView().Center.X &&
		        (int)nextText.Position.X >= (int)(Program.Window.GetView().Size.X + nextText.Origin.X))
		    {
			    itemChanges.Dequeue();
			    moving = false;
			    nextDraw = false;
			    nextText.DisplayedString = "";
		    }

		}

		private void nextItem(float _deltaTime)
	    {
		    if (!moving && currentChoice == MenuItems.Exit)
		    {
			    itemChanges.Dequeue();
				return;
		    }

		    if (!moving)
		    {
			    nextText.DisplayedString = getText(currentChoice);
			    nextText.Origin = text.Origin;
			    nextText.Position = text.Position;
			    nextDraw = true;

			    currentChoice = currentChoice + 1;

				text.DisplayedString = getText(currentChoice);
			    text.Origin = new Vector2f(text.GetLocalBounds().Width / 2f, text.Origin.Y);
			    textPos = new Vector2f(Program.Window.GetView().Size.X + text.Origin.X, text.Position.Y);
			    text.Position = textPos;

			    distNext = (0 - nextText.Origin.X - nextText.Position.X) / itemSpeed;
			    distText = (Program.Window.GetView().Center.X - textPos.X) / itemSpeed;

				moving = true;
		    }

			if ((int) textPos.X > (int) Program.Window.GetView().Center.X)
			{
			    textPos += new Vector2f(distText * _deltaTime, 0);
			    text.Position = textPos;
			}

		    if ((int) nextText.Position.X > (int) (0 - nextText.Origin.X))
		    {
			    nextText.Position += new Vector2f( distNext * _deltaTime, 0);
		    }
		    if ((int) textPos.X <= (int) Program.Window.GetView().Center.X &&
		        (int) nextText.Position.X <= (int) (0 - nextText.Origin.X))
		    {
			    itemChanges.Dequeue();
			    moving = false;
			    nextDraw = false;
			    nextText.DisplayedString = "";
		    }
	    }

	    private string getText(MenuItems item)
	    {
			switch (item)
			{
				case MenuItems.Teststage:
					return "Play Teststage";
				case MenuItems.Play:
					return "Play";
				case MenuItems.Exit:
					return "Exit";
				default:
					return "Something wrong...";
			}
		}

	    public override void KeyPressed(object sender, KeyEventArgs e)
	    {
		    switch (e.Code)
		    {
				case Keyboard.Key.Return:
					if (!moving)
					{
						switch (currentChoice)
						{
							#if DEBUG
							case MenuItems.Teststage:
								IsFinished = true;
								NewState = new PlayState();
								break;
							#endif
							case MenuItems.Play:
								//TODO Write this menu
								break;
							case MenuItems.Exit:
								IsFinished = true;
								break;
						}
					}
					break;
				case Keyboard.Key.Left:
					itemChanges.Enqueue(-1);
					break;
				case Keyboard.Key.Right:
					itemChanges.Enqueue(+1);
					break;
				case Keyboard.Key.Escape:
					IsFinished = true;
					break;
		    }
	    }

	}
}
