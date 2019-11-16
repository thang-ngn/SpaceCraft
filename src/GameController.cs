using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    public static class GameController
    {
        private static Bitmap background;
        private static SpaceCraft s;
        private static List<IAmFlyingObject> flyingObj;
        private static List<ICanShoot> canShootObj;
        private static Random rand;
        private static int time, timeEnemy, randEnemyMax, randEnemyMin, timeIntervalEnemy, tempScore;
        private static int timeEnemyBullet, randEnemuBulletMax, randEnemuBulletMin, timeIntervalEnemyBullet;
        private static int timeItem, randItemMax, randItemMin, timeIntervalItem;
        private static float enemyVelocity, bulletVelocity;
        private static SwinGameSDK.Timer timer;

        static GameController()
        {
            background = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Background.png");

            s = new SpaceCraft();  //create the SpaceCraft

            flyingObj = new List<IAmFlyingObject>();    //the list containing all IAmFlyingObject objects
            canShootObj = new List<ICanShoot>();        //the list containing all ICanShoot objects
            canShootObj.Add(s);

            rand = new Random();       //random object which is used throughout the program
                                   
            time = 0;   //when time = 0, a new bullet will be created if LeftMouse is down, if time = 6 it will be set back to 0

            timeEnemy = 0;  //an enemy is created when timeEnemy = 0
            randEnemyMax = 210;
            randEnemyMin = 60;
            timeIntervalEnemy = rand.Next(randEnemyMin, randEnemyMax); //when timeEnemy reaches timeIntervalEnemy, it is set back to 0. This will be changed in the future, so it is not a constant
            tempScore = 0;  //temperary score of the player, this is used to compare with the real score later
            enemyVelocity = 1.5F;  //velocity of the enemy; this will change in the future

            bulletVelocity = 5F;    //velocities of bullets

            timeEnemyBullet = 0;
            randEnemuBulletMax = 500;
            randEnemuBulletMin = 200;
            timeIntervalEnemyBullet = rand.Next(randEnemuBulletMin, randEnemuBulletMax);


            timeItem = 0;   //an item will be created when timeItem = 0;
            randItemMax = 1000;
            randItemMin = 400;
            timeIntervalItem = rand.Next(randItemMin, randItemMax);

            timer = SwinGame.CreateTimer();
            timer.Start();
        }

        public static bool GameOver()
        {
            if (s.Lives == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DrawBackground()
        {
            SwinGame.DrawBitmap(background, 0, 0);
            SwinGame.DrawText("Blood:" + s.Blood + "%" + "      " + "Lives: " + s.Lives, Color.Blue, 5, 10); //display the blood of the spacecraft to the screen
            SwinGame.DrawText("Score:" + s.Score, Color.Blue, Constants.WIDTH - 90, 10); //display the blood of the spacecraft to the screen
        }

        public static void DrawCanShootObj()
        {
            for (int i = 0; i < canShootObj.Count; i++)
            {
                canShootObj[i].Draw();
            }
        }

        public static void MoveCanShootObj()
        {
            for (int i = 0; i < canShootObj.Count; i++)
            {
                if (canShootObj[i].Object_Type != ObjectType.SpaceCraft)
                    canShootObj[i].Move();
                else
                {
                    if (timer.Ticks >= 1000)
                    {
                        canShootObj[i].Move();
                    }
                }

            }
        }

        public static void CheckCanShootObj()
        {
            for (int i = 0; i < canShootObj.Count; i++)
            {
                ICanShoot obj = canShootObj[i];

                if (obj.Object_Type == ObjectType.SpaceCraft)
                {
                    SpaceCraft s = (SpaceCraft)obj;

                    if (s.Blood <= 0)
                    {
                        s.Alive = false;
                    }

                    if (s.Alive == false)
                    {
                        s.Lives -= 1;       //reduce the number of lives of the spacecraft when it hits an enemy
                        s.BulletDamage = 1; //set back to the first level of damage
                                            //set back the image of the defualt bullet
                        s.BulletImg = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Bullet_Default.png");
                        timer.Reset();      //reset the timer
                        SwinGame.ShowMouse();  //show the mouse when the spacecraft comes back
                        s.X = Constants.WIDTH / 2 - s.Width / 2;
                        s.Y = Constants.HEIGHT - s.Height - 10;
                        s.Alive = true;    //set back to alive
                    }
                }

                else if (obj.Object_Type == ObjectType.Enemy)
                {
                    Enemy enemy = (Enemy)obj;

                    //remove the enemy if it goes out of the screen
                    if (enemy.Y > Constants.HEIGHT)
                    {
                        enemy.Alive = false;
                        if (s.Score > 0)
                            s.Score -= 1;
                    }

                    if (enemy.Collide(s))
                    {
                        s.Alive = false;
                        enemy.Alive = false;
                    }

                    if (enemy.Blood <= 0)
                    {
                        enemy.Alive = false;
                        s.Score += 1;
                    }

                    if (enemy.Alive == false)
                    {
                        canShootObj.Remove(enemy);
                    }
                }
            }
        }
        
        public static void CreateSpaceCraftBullet()
        {
            if (SwinGame.MouseDown(MouseButton.LeftButton) && time == 0)
            {
                flyingObj.Add(s.CreateBullet());
            }

            time += 1;  //increase time
            if (time == Constants.TIMEINTERVAL_BULLET) { time = 0; }   //reset time to 0 when it reaches TIMEINTERVAL_BULLET = 6
        }


        public static void CreateEnemy()
        {
            int i;

            //create a new enemy whenever timeEnemy reaches 0
            if (timeEnemy == 0 && s.Score < 20)
            {
                Enemy e = new SmallEnemy(SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\SmallEnemy.png"), enemyVelocity);
                canShootObj.Add(e);
            }
            else if (timeEnemy == 0 && s.Score >= 20 && s.Score < 50)
            {
                i = rand.Next(0, 2);
                if (i == 0)
                {
                    Enemy e = new SmallEnemy(SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\SmallEnemy.png"), enemyVelocity);
                    canShootObj.Add(e);
                }
                else if (i == 1)
                {
                    Enemy e = new MediumEnemy(SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\MediumEnemy.png"), enemyVelocity);
                    canShootObj.Add(e);
                }
            }
            else if (timeEnemy == 0 && s.Score >= 50)
            {
                i = rand.Next(0, 3);
                if (i == 0)
                {
                    Enemy e = new SmallEnemy(SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\SmallEnemy.png"), enemyVelocity);
                    canShootObj.Add(e);
                }
                else if (i == 1)
                {
                    Enemy e = new MediumEnemy(SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\MediumEnemy.png"), enemyVelocity);
                    canShootObj.Add(e);
                }
                else if (i == 2)
                {
                    Enemy e = new LargeEnemy(SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\LargeEnemy.png"), enemyVelocity);
                    canShootObj.Add(e);
                }
            }

            timeEnemy += 1;
            if (timeEnemy == timeIntervalEnemy) { timeEnemy = 0; timeIntervalEnemy = rand.Next(randEnemyMin, randEnemyMax); }
        }

        public static void IncreaseDifficulty()
        {
            //only when temperary score is not the same as real score, timeIntervalEnemy is increased
            if (s.Score % 10 == 0 && tempScore != s.Score)
            {
                if (randEnemyMin > 10)
                    randEnemyMin -= 10;
                if (randEnemyMax > 40)
                    randEnemyMax -= 20;
                if (enemyVelocity <= 8.5)
                    enemyVelocity += 1F;
                tempScore = s.Score;

                if (randEnemuBulletMax > 300)
                {
                    randEnemuBulletMax -= 20;
                }
                if (randEnemuBulletMin > 100)
                {
                    randEnemuBulletMin -= 10;
                }
            }  //increase the density of enemies


            //increase the density of items
            if (s.Score % 20 == 0 && tempScore != s.Score && s.Score <= 80) {
                randItemMax -= 100;
                randItemMin -= 100;
            }
        }


        public static void CreateEnemyBullet()
        {
            timeEnemyBullet += 1;
            if (timeEnemyBullet == timeIntervalEnemyBullet) { timeEnemyBullet = 0; timeIntervalEnemyBullet = rand.Next(randEnemuBulletMin, randEnemuBulletMax); }
            if (timeEnemyBullet == 0)
            {
                int i = rand.Next(1, canShootObj.Count);    //enemy has index greater than 0

                try
                {
                   flyingObj.Add(canShootObj[i].CreateBullet());
                }
                catch (Exception e) { }
            }
        }

        public static void CreateItem()
        {
            int randNum;
            timeItem += 1;
            if (timeItem == timeIntervalItem) { timeItem = 0; timeIntervalItem = rand.Next(randItemMin, randItemMax); }
            if (timeItem == 0)
            {
                randNum = rand.Next(0, 100);

                if (randNum % 2 == 0)
                {
                    DamageIncrease di = new DamageIncrease(SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Bullet_Increase.png"));
                    flyingObj.Add(di);
                }
                else if (randNum % 2 == 1)
                {
                    LiveIncrease li = new LiveIncrease(SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Live_Increase.png"));
                    flyingObj.Add(li);
                }

            }
        }

        public static void MoveFlyingObject()
        {
            for (int i = 0; i < flyingObj.Count; i++)
            {
                IAmFlyingObject obj = flyingObj[i];
                obj.Draw();

                if (obj.Object_Type == ObjectType.SpaceCraft && obj.GetType().Equals(typeof(Bullet))) 
                {
                    obj.MoveY(-bulletVelocity);
                }
                else if (obj.Object_Type == ObjectType.Enemy && obj.GetType().Equals(typeof(Bullet)))
                {
                    obj.MoveY(enemyVelocity + 2);
                }
                else if (obj.Object_Type == ObjectType.Equipment)
                {
                    obj.MoveY(enemyVelocity);
                }
                
                if (obj.Y > Constants.HEIGHT || obj.Y < 0 - obj.Height)
                {
                    flyingObj.Remove(obj);
                }
            }
        }

        public static void FlyingObjectHits()
        {
            for (int i = 0; i < flyingObj.Count; i++)
            {
                IAmFlyingObject obj = flyingObj[i];

                if (obj.Object_Type == ObjectType.SpaceCraft && obj.GetType().Equals(typeof(Bullet)))
                {
                    Bullet bullet = (Bullet) obj;

                    for (int j = 0; j < canShootObj.Count; j++)
                    {
                        //Console.WriteLine(enemies[j].Blood);
                        if (obj.Collide((GameObject) canShootObj[j]) && canShootObj[j].Object_Type == ObjectType.Enemy)
                        {
                            canShootObj[j].Blood -= bullet.Damage;
                            bullet.Alive = false;
                        }
                    }

                    //if the bullet goes out of the screen, set its Alive status to false
                    if (bullet.Y < 0 - bullet.Height)
                    {
                        bullet.Alive = false;
                    }

                    //remove bullet when it is dead
                    if (bullet.Alive == false)
                    {
                        flyingObj.Remove(bullet);
                    }
                }
                else if (obj.Object_Type == ObjectType.Enemy && obj.GetType().Equals(typeof(Bullet)))
                {
                    Bullet bullet = (Bullet)obj;

                    if (bullet.Collide(s))
                    {
                        s.Blood -= bullet.Damage;

                        //set back one level of the bullet
                        if (s.BulletDamage > 1)
                        {
                            //increase the damage of the bullet of the scraft
                            s.BulletDamage -= 1;
                        }

                        if (s.BulletDamage == 2)
                        {
                            s.BulletImg = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Bullet_2.png");
                        }
                        else if (s.BulletDamage == 1)
                        {
                            s.BulletImg = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Bullet_Default.png");
                        }

                        flyingObj.Remove(bullet);
                    }
                }
                else if (obj.Object_Type == ObjectType.Equipment)
                {
                    Equipment equipment = (Equipment)obj;

                    if (equipment.Collide(s))
                    {
                        equipment.Affect(s);
                        flyingObj.Remove(equipment);
                    }
                }
            }
        }

        public static void GameFlow()
        {
            DrawBackground();

            DrawCanShootObj();
            MoveCanShootObj();

            IncreaseDifficulty();
            
            CreateSpaceCraftBullet();

            CreateEnemy();
            CreateEnemyBullet();

            CheckCanShootObj();

            CreateItem();

            MoveFlyingObject();
            FlyingObjectHits();
        }
    }
}
