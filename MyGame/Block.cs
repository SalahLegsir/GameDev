using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Block
    {
        public Rectangle block { get; set; }
        public List<Rectangle> blocks { get; set; }

        public Block(int posX, int posY, int width, int height )
        {
            block = new Rectangle(posX, posY, width, height);
            blocks.Add(block);
        }

      
        


    }
}
