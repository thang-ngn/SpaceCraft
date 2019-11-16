using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class MediumEnemy : Enemy
    {
        public MediumEnemy (Bitmap image, float velocity) : base(image, velocity)
        {
            Blood = 5;
        }
    }
}
