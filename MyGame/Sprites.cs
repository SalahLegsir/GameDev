using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Sprites
    {
        public Sprites(int widthFrame, int heightFrame)
        {
            _heightFrame = heightFrame;
            _widthFrame = widthFrame;
            _frameRect = new Rectangle(0, 0, _widthFrame , _heightFrame);
        }

        private int _widthFrame { get;  set; }
        private int _heightFrame { get;  set; }

        private Rectangle _frameRect;
 
        public Dictionary<string,List<Rectangle>> MakeDictionary()
        {
            Dictionary<string, List<Rectangle>> movements = new Dictionary<string, List<Rectangle>>();

            movements.Add("idle", MakeList(4, 0));
            movements.Add("walk", MakeList(8, 64));
            movements.Add("powerShot", MakeList(7, 5 * 64));


            return movements;
        }


        public List<Rectangle> MakeList(int frameCount, int row)
        {

            List<Rectangle> frames = new List<Rectangle>();
            _frameRect.Y = row;

            for (int i = 0; i < frameCount; i++)
            {

                frames.Add(_frameRect);

                _frameRect.X += _widthFrame;

            }
            _frameRect.X = 0;
            _frameRect.Y = 0;
            return frames;
        }

        //public List<Rectangle> MakeList()
        //{
        //    List<Rectangle> frames = new List<Rectangle>();

        //    for (int i = 0; i < 10; i++)
        //    {

        //        if (_frameRect.X > 720)
        //        {
        //            _frameRect.X = 0;

        //            _frameRect.Y += _heightFrame; // 2de rij

        //            if (_frameRect.Y > _heightFrame) // terug naar 1ste rij
        //            {
        //                _frameRect.Y = 26;
        //            }
        //        }

        //        frames.Add(_frameRect);

        //        _frameRect.X += _widthFrame;

        //    }

        //    return frames;

        //}

    }
}
