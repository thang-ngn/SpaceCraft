using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    class GameObject
    {
        private float _height;
        private float _width;
        private bool _alive;    //object is alive or dead
        private Bitmap _image;  //image of the object
        private ObjectType _objectType;     //type of the object: SpaceCraft, Enemy or Equipment
        protected Sprite _objectSprite;

        public float X { get => SwinGame.SpriteX(_objectSprite); set => SwinGame.SpriteSetX(_objectSprite, value); }  //get and set the X location of the Sprite

        public float Y { get => SwinGame.SpriteY(_objectSprite); set => SwinGame.SpriteSetY(_objectSprite, value); }  //get and set the Y location of the Sprite

        public float Height { get => SwinGame.SpriteHeight(_objectSprite); }  //height of the objectSprite

        public float Width { get => SwinGame.SpriteWidth(_objectSprite); }  //width of the objectSprite

        public bool Alive { get => _alive; set => _alive = value; }

        public Bitmap Image { get => _image; set => _image = value; }

        public ObjectType Object_Type { get => _objectType; set => _objectType = value; }

        public Sprite ObjectSprite { get => _objectSprite; }

        public GameObject(Bitmap image, float x, float y)
        {
            _image = image;

            _objectSprite = SwinGame.CreateSprite(_image);   //create the sprite
            _alive = true;

            SwinGame.SpriteSetX(_objectSprite, x);      //initialise x position of the sprite
            SwinGame.SpriteSetY(_objectSprite, y);      //initialise y position of the sprite
        }

        public GameObject(Bitmap image)
        {
            _image = image;
            _objectSprite = SwinGame.CreateSprite(_image);   //create the sprite
            _alive = true;
        }

        public GameObject() { _alive = true; }     //default constructor

        //object will draw itself to the screen
        public void Draw() {
            SwinGame.DrawSprite(_objectSprite);
        }
    }
}
