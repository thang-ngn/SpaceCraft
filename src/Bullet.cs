using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class Bullet : GameObject, IAmFlyingObject
    {
        private int _damage;    //the number of damage that the bullet has

        public int Damage { get => _damage; set => _damage = value; }

        public Bullet(Bitmap image, float x, float y) : base(image, x, y) {
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
    }
}
