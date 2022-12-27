using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class FPS
    {
        public FPS()
        {
           

        }

        Rectangle currentFrame;
         int counter = 0;
        double secondCounter = 0;
      
        
        public Rectangle Fps(GameTime gametime, List<Rectangle> frames)
        {
            currentFrame = frames[counter];

            secondCounter += gametime.ElapsedGameTime.TotalSeconds;
            int fps = 10;
            

            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }

            return currentFrame;
        }

    }
}
