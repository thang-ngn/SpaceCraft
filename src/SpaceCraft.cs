using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class SpaceCraft : GameObject, ICanShoot
    {
        private int _blood = Constants.SPACE_CRAFT_BLOOD;     //blood of the spacecraft
        private int _lives = Constants.SPACE_CRAFT_LIVES;     //default number of lives of the spacecraft
        private int _score;     //score that the player has gained
        private Bitmap _bulletImg = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Bullet_Default.png");    //default bullet
        private int _bulletDamage;  //the damage of the bullet, this can be updated later
        private Bitmap _defaultImage = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\SpaceCraft.png");  //default image of the space craft

        public int Blood { get => _blood; set => _blood = value; }

        public int Lives { get => _lives; set => _lives = value; }

        public int Score { get => _score; set => _score = value; }

        public Bitmap DefaultImage { get => _defaultImage; }

        public Bitmap BulletImg { get => _bulletImg; set => _bulletImg = value; }

        public int BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }

        public SpaceCraft(Bitmap image, float x, float y) : base(image, x, y) {
            Object_Type = ObjectType.SpaceCraft;
            _bulletDamage = 1;  //default value of bullet damage
        }

        public SpaceCraft() : base()        //default constructor
        {
            //image of the SpaceCraft
            Image = _defaultImage;
            _objectSprite = new Sprite(Image);
            SwinGame.SpriteSetX(_objectSprite, Constants.WIDTH / 2 - _objectSprite.Width / 2);
            SwinGame.SpriteSetY(_objectSprite, Constants.HEIGHT - _objectSprite.Height - 10);
            _bulletDamage = 1;  //default value of bullet damage
        }

        //public List<Bullet> Bullets { get => _bullets; }

        public void Move() {

            float x = SwinGame.MouseX() - Width/2;
            float y = SwinGame.MouseY() - Height/2;

            //ensure that the spacecraft always stays inside the screen
            if (x > Constants.WIDTH - Image.Width)
            {
                x = Constants.WIDTH - Image.Width;
            }
            else if (x < 0)
            {
                x = 0;
            }

            if (y > Constants.HEIGHT - Image.Height)
            {
                y = Constants.HEIGHT - Image.Height;
            }
            else if (y < 0) {
                y = 0;
            }
            SwinGame.SpriteSetX(ObjectSprite, x);    //update value of X
            SwinGame.SpriteSetY(ObjectSprite, y);    //update value of Y
        }

        public Bullet CreateBullet()
        {
            Bullet b = new Bullet(_bulletImg, X + Width / 2 - _bulletImg.Width / 2, Y - _bulletImg.Height);
            b.Damage = _bulletDamage;
            b.Object_Type = ObjectType.SpaceCraft;

            return b;
        }
    }
}
