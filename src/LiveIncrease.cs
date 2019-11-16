using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class LiveIncrease : Equipment
    {
        public LiveIncrease(Bitmap image, float x, float y) : base(image, x, y) { }

        public LiveIncrease(Bitmap image) : base(image)
        {
        }

        public override void Affect(SpaceCraft s)
        {
            if (s.Lives <= 5)
            {
                //increase the damage of the bullet of the scraft
                s.Lives += 1;
            }
        }
    }
}
