using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using _Flawless.math;

namespace _Flawless.src.ai2
{
    class Movement
    {
        private float leftBorder = 0;
        private float rightBorder = 600;
        private float topBorder = 0;

        public enum State
        {
            StraightIn,
            StraightOut,
            StandStill5s
        }

        float speed;
        Queue<State> stateList;
        State state;
        Vector2f destination;
        float timePassed;

        public Movement(Queue<State> _stateList, float _speed, Vector2f _destination)
        {
            stateList = _stateList;
            speed = _speed;
            destination = _destination;

            timePassed = float.MinValue;
            state = stateList.Dequeue();
        }

        private Vector2f NewPosition(Vector2f _position, Vector2f _direction, float _deltaTime)
        {
            Vector2f movement = _direction / Maths.length(_direction) * speed * _deltaTime;
            return _position + movement;
        }

        public Vector2f SpawnPosition(Vector2f _destination)
        {
            switch (state)
            {
                case State.StraightIn:
                    if (Math.Abs(_destination.X - leftBorder) < Math.Abs(_destination.X - rightBorder))
                    {
                        if (Math.Abs(_destination.Y - topBorder) > 50) return new Vector2f(leftBorder, _destination.Y - 50);
                        else return new Vector2f(leftBorder, topBorder);
                    }
                    else
                    {
                        if (Math.Abs(_destination.Y - topBorder) > 50) return new Vector2f(rightBorder, _destination.Y - 50);
                        else return new Vector2f(rightBorder, topBorder);
                    }
                default:
                    return new Vector2f(0, 0);
            }

        }

        public Vector2f NewPosition(Vector2f _position, float _deltaTime)
        {
            switch (state)
            {
                case State.StraightIn:
                    Vector2f newPosition = NewPosition(_position, destination - _position, _deltaTime);
                    if (Maths.length(newPosition - destination) >= Maths.length(_position - destination))
                    {
                        state = stateList.Dequeue();
                        return destination;
                    }
                    return newPosition;

                case State.StandStill5s:
                    if (timePassed == float.MinValue) timePassed = 0f;
                    System.Console.WriteLine(timePassed);
                    timePassed += _deltaTime;
                    if (timePassed < 5f) return _position;
                    else
                    {
                        timePassed = float.MinValue;
                        state = stateList.Dequeue();
                        return _position;
                    }

                case State.StraightOut:
                    return _position + new Vector2f(0,50*speed*_deltaTime);

                /*case State.Announcement:
                    return new Vector2f(0, 0);

                case State.TakingPosition:
                    return NewPosition(_position, tpDestination - _position, _deltaTime);

                case State.Loop:
                    return new Vector2f(0, 0);

                case State.LeavingPosition:
                    return NewPosition(_position, _position - lpDestination, _deltaTime);*/

                default:
                    return _position;
            }
        }
    }
}
