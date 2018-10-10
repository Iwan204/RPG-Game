using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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


        public static void Initialise(GraphicsDevice graphicsDevice)
        {
            camera = new Camera2D(graphicsDevice);
            pointOne = new Vector2();
            pointTwo = new Vector2();
            randPointOne = new Vector2();
            randPointTwo = new Vector2();
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
            randomNum = new Random(DateTime.UtcNow.Millisecond);
            randPointTwo = new Vector2(randomNum.Next(1,50), randomNum.Next(1, 50));
            float camX = (randPointOne.X - randPointTwo.X);
            float camY = (randPointOne.Y - randPointTwo.Y);
            camera.Move(new Vector2(camX, camY));
            pointOne = new Vector2(randomNum.Next(1, 50), randomNum.Next(1, 50));
        }

        }
}

