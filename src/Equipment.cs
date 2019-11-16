using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class Equipment : GameObject, IAmFlyingObject
    {
        private static Random rand = new Random();

        public Equipment(Bitmap image, float x, float y) : base(image, x, y) {
            Object_Type = ObjectType.Equipment;
        }

        public Equipment(Bitmap image) : base(image)
        {
            Object_Type = ObjectType.Equipment;

            Thread.Sleep(1);    //setting a new seed for the Random
            //Random rand = new Random();

            X = (float)(rand.NextDouble() * Constants.WIDTH);
            Y = 0;

            //ensure that the sprite is always inside the screen
            if (X < 0)
            {
                X = 0;
            }
            else if (X > Constants.WIDTH - Width)
            {
                X = Constants.WIDTH - Width;
            }
        }

        public void MoveY(float YVelocity)
        {
            SwinGame.SpriteSetY(ObjectSprite, SwinGame.SpriteY(ObjectSprite) + YVelocity);
        }

        public bool Collide(GameObject obj)
        {
            if (SwinGame.SpriteCollision(ObjectSprite, obj.ObjectSprite))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Affect(SpaceCraft s) { }
    }
}
