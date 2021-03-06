﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Snake<TCoordinate, TDirection> : ISnake<TCoordinate, TDirection>
    {
        private readonly ICoordinate<TCoordinate, TDirection> _startCoordinate;
        private readonly int _initialSize;
        private readonly ISnakePartFactory<TCoordinate, TDirection> _snakePartFactory;
        private readonly Queue<ISnakePart<TCoordinate, TDirection>> _queue = new Queue<ISnakePart<TCoordinate, TDirection>>();

        public Snake(ICoordinate<TCoordinate, TDirection> startCoordinate, int initialSize, ISnakePartFactory<TCoordinate, TDirection> snakePartFactory)
        {
            _startCoordinate = startCoordinate;
            _initialSize = initialSize;
            _snakePartFactory = snakePartFactory;
            InitSnake();
            
        }
        
        private void InitSnake()
        {
            InitSnakeHead();
            InitSnakeTail();
        }

        private void InitSnakeTail()
        {
            for (var i = 0; i < _initialSize; i++)
                AddNewSnakeTailPart();            
        }

        private void InitSnakeHead()
        {
            var snakeHead = _snakePartFactory.MakeSnakePart(_startCoordinate);
            _queue.Enqueue(snakeHead);
        }


        public void Grow()
        {            
            AddNewSnakeTailPart();
        }

        private void AddNewSnakeTailPart()
        {
            var previousCoordinate = _queue.Last().Coordinate().MakePreviousByDirection();
            var snakeTailPart = _snakePartFactory.MakeSnakePart(previousCoordinate);
            _queue.Enqueue(snakeTailPart);
        }

        public void Move(TDirection newDirection)
        {
            _queue.First().Move(newDirection);
            ISnakePart<TCoordinate, TDirection> previousPart = null;

            foreach (var snakeTailPart in _queue)
            {
                
                if (snakeTailPart == _queue.First())
                {
                    previousPart = _queue.First();
                    continue;
                }

                snakeTailPart.Move(previousPart);
                previousPart = snakeTailPart;
            }
        }

        public IEnumerable<ISnakePart<TCoordinate, TDirection>> Body()
        {
            return _queue;
        }
    }
}
