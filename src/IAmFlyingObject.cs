using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.src
{
    interface IAmFlyingObject
    {
        ObjectType Object_Type { get; set; }
        float X { get; set; }
        float Y { get; set; }
        float Height { get; }
        float Width { get; }
        void MoveY(float YVelocity);
        bool Collide(GameObject obj);
        void Draw();
    }
}
