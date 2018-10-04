using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.Extended;

namespace Game1
{
    public static class Camera
    {
        public static Camera2D camera;
        static Vector2 pointOne;
        static Vector2 pointTwo;


        public static void Initialise(GraphicsDevice graphicsDevice)
        {
            camera = new Camera2D(graphicsDevice);
            pointOne = new Vector2();
            pointTwo = new Vector2();
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
            camera.Move(new Vector2(50,50) * time.ElapsedGameTime.Milliseconds);
        }

        }
}

