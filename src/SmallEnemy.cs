using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class SmallEnemy : Enemy
    {
        public SmallEnemy(Bitmap image, float velocity) : base(image, velocity) {
            Blood = 3;
        }
    }
}
