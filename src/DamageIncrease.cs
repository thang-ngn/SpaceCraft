using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class DamageIncrease : Equipment
    {
        public DamageIncrease(Bitmap image, float x, float y) : base(image, x, y) { }

        public DamageIncrease(Bitmap image) : base(image)
        {
        }

        public override void Affect(SpaceCraft s)
        {
            if (s.BulletDamage < 4)
            {
                //increase the damage of the bullet of the scraft
                s.BulletDamage += 1;
            }

            if (s.BulletDamage == 2)
            {
                s.BulletImg = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Bullet_2.png");
            }
            else if (s.BulletDamage == 3)
            {
                s.BulletImg = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Bullet_3.png");
            }
        }
    }
}

