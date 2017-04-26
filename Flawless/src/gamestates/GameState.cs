using SFML.Graphics;

namespace _Flawless.gamestates
{
	abstract class GameState
	{
		public abstract void Draw(RenderWindow _window);
		public abstract void Update(float _deltaTime);


		public bool IsFinished { get; protected set; }
		public GameState NewState { get; private set; }
	}
}
