﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    interface ISnake<TCoordinate, TDirection>
    {

        void Grow();

        void Move(TDirection newDirection);

        IEnumerable<ISnakePart<TCoordinate, TDirection>> Body();
    }
}
