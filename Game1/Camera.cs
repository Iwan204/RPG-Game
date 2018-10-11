using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Collections.Generic;
using MonoGame.Extended;

namespace Game1
{
    public static class Camera
    {
        private static Random randomNum;
        public static Camera2D camera;
        static Vector2 pointOne;
        static Vector2 pointTwo;
        static Vector2 randPointOne;
        static Vector2 randPointTwo;
        static Queue<Vector2> demoCameraQueue;
        static Vector2 currentDemoVector;

        private static Timer aTimer;

        public static void Initialise(GraphicsDevice graphicsDevice)
        {
            camera = new Camera2D(graphicsDevice);
            pointOne = new Vector2();
            pointTwo = new Vector2();
            randPointOne = new Vector2();
            randPointTwo = new Vector2();
            demoCameraQueue = new Queue<Vector2>();
            currentDemoVector = new Vector2(0,0);
            randomNum = new Random(DateTime.UtcNow.Millisecond);

            SetDemoTimer();
            //aTimer.Stop();
            //aTimer.Dispose();
        }

        public static void Update()
        {
                //move camera with mouse
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    pointTwo = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
                    float camX = (pointOne.X - pointTwo.X);
                    float camY = (pointOne.Y - pointTwo.Y);
                    camera.Move(new Vector2(camX, camY));
                }
                pointOne = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
                //rotate camera
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    Console.WriteLine("R key pressed!");
                    camera.Rotate(0.0075f);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    Console.WriteLine("Q key pressed!");
                    camera.Rotate(-0.0075f);
                }
            }

        public static void UpdateMainMenu(GameTime time)
        {

            randPointTwo = new Vector2(randomNum.Next(-2,2), randomNum.Next(-2, 2));
            float camX = (randPointOne.X - randPointTwo.X);
            float camY = (randPointOne.Y - randPointTwo.Y);
            if (camera.BoundingRectangle.Center.X > 800 || camera.BoundingRectangle.Center.X < -800)
            {
                if (camera.BoundingRectangle.Center.Y > 800 || camera.BoundingRectangle.Center.Y < -800)
                {
                    var newpos = new Vector2(-800,800);
                    camX = (randPointOne.X - newpos.X);
                    camY = (randPointOne.Y - newpos.Y);
                }
            }
            var camVec = new Vector2(camX, camY);
            demoCameraQueue.Enqueue(camVec);
            pointOne = new Vector2(randomNum.Next(-2, 2), randomNum.Next(-2, 2));

            camera.Move(currentDemoVector);
        }

        public static void OnDemoMovement(Object source, ElapsedEventArgs e)
        {
            currentDemoVector = demoCameraQueue.Dequeue();
            //camera.Move(demoCameraQueue.Dequeue() / 50f);
        }

        private static void SetDemoTimer()
        {
            aTimer = new Timer(1000);
            aTimer.Elapsed += OnDemoMovement;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }


    }
}
//25 * 32 = 800 middle of map Y
