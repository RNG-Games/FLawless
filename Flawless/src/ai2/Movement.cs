using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using _Flawless.math;

namespace _Flawless.ai2
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
            StandStill5s,
            StandStill10s
        }

        float speed;
        Queue<State> stateList;
        State state;
        Vector2f destination;
        bool outDestination;
        float timePassed;

        public Movement(Queue<State> _stateList, float _speed, Vector2f _destination)
        {
            stateList = _stateList;
            speed = _speed;
            destination = _destination;

            timePassed = float.MinValue;
            outDestination = false;
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
                    timePassed += _deltaTime;
                    if (timePassed < 5f) return _position;
                    else
                    {
                        timePassed = float.MinValue;
                        state = stateList.Dequeue();
                        return _position;
                    }

                case State.StandStill10s:
                    if (timePassed == float.MinValue) timePassed = 0f;
                    timePassed += _deltaTime;
                    if (timePassed < 10f) return _position;
                    else
                    {
                        timePassed = float.MinValue;
                        state = stateList.Dequeue();
                        return _position;
                    }

                case State.StraightOut:
                    if (Math.Abs(_position.X - leftBorder) < Math.Abs(_position.X - rightBorder)) return NewPosition(_position, new Vector2f(leftBorder-20, topBorder-10) - _position, _deltaTime);
                    else return NewPosition(_position, new Vector2f(rightBorder+20, topBorder-10) - _position, _deltaTime);

                default:
                    return _position;
            }
        }
    }
}
