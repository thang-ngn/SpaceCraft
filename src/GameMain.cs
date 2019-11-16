using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame.src
{
    static class Constants
    {
        public const int HEIGHT = 800;
        public const int WIDTH = 600;
        public const int TIMEINTERVAL_BULLET = 7;
        public const int SPACE_CRAFT_BLOOD = 100;   //default amount of blood of the spacecraft
        public const int SPACE_CRAFT_LIVES = 3; //default number of lives of the spacecraft
    }

    public enum ObjectType { SpaceCraft, Enemy, Equipment };
    public class GameMain
    {
        public static void Main()
        {
           

            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", Constants.WIDTH, Constants.HEIGHT);
            //SwinGame.ShowSwinGameSplashScreen();

            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                
                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.Black);
                //SwinGame.DrawFramerate(0, 0);
                
                if (GameController.GameOver() == false)
                {
                    SwinGame.HideMouse();
                    GameController.GameFlow();
                }
                else
                {
                    SwinGame.ClearScreen(Color.Black);
                    SwinGame.ShowMouse();
                    Bitmap gameOver = SwinGame.LoadBitmap(SwinGame.PathToResource("images") + "\\Gameover.png");
                    SwinGame.DrawBitmap(gameOver, Constants.WIDTH / 2 - gameOver.Width / 2, Constants.HEIGHT / 2 - gameOver.Height / 2);
                    
                }

                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}