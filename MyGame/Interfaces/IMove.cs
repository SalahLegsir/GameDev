using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Interfaces
{
    internal interface IMove
    {
        bool Move(Rectangle _currentFrame, int speed, Rectangle surface, int level);
        
    }
}
