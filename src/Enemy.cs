using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class Enemy : GameObject, ICanShoot
    { 
        private int _blood;     //blood of the spacecraft
        private Bitmap _bulletImg = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Enemy_Bullet.png");    //default bullet    
        private float _velocity = 1.5F;
        private static Random rand = new Random();

        public int Blood { get => _blood; set => _blood = value; }

        public Bitmap BulletImg { get => _bulletImg; set => _bulletImg = value; }

        public float Velocity { get => _velocity; set => _velocity = value; }

        public Enemy(Bitmap image, float x, float y, float velocity) : base(image, x, y)
        {
            Object_Type = ObjectType.Enemy;
            _velocity = velocity;
        }

        public Enemy(Bitmap image, float velocity) : base(image)
        {
            _velocity = velocity;  

            Object_Type = ObjectType.Enemy;
            Thread.Sleep(1);    //setting a new seed for the Random
            //Random rand = new Random();

            SwinGame.SpriteSetX(_objectSprite, (float) (rand.NextDouble() * Constants.WIDTH));
            SwinGame.SpriteSetY(_objectSprite, 0);

            //ensure that the sprite is always inside the screen
            if (_objectSprite.X < 0)
            {
                SwinGame.SpriteSetX(_objectSprite, 0);
            }
            else if (_objectSprite.X > Constants.WIDTH - _objectSprite.Width)
            {
                SwinGame.SpriteSetX(_objectSprite, Constants.WIDTH - _objectSprite.Width);
            }
        }

        public Bullet CreateBullet()
        {
            Bullet b = new Bullet(_bulletImg, X + Width / 2 - _bulletImg.Width / 2, Y + _bulletImg.Height);
            b.Damage = 10;
            b.Object_Type = ObjectType.Enemy;

            return b;
        }

        public void Move()
        {
            SwinGame.SpriteSetY(ObjectSprite, SwinGame.SpriteY(ObjectSprite) + _velocity);
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
