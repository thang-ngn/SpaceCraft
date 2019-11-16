using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.src
{
    interface ICanShoot
    {
        int Blood { get; set; }

        ObjectType Object_Type { get; set; }

        Bullet CreateBullet();

        void Draw();

        void Move();
    }
}
