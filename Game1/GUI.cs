using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    static class GUI
    {
        private static SpriteBatch spriteBatch;

        private static GraphicsDevice graphicsDevice;

        public static List<GUIelement> Elements;

        public static SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }


        public static void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred);
            foreach (var item in Elements)
            {
                item.Draw(SpriteBatch);
            }
            SpriteBatch.End();
        }

        public static void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                foreach (var item in Elements)
                {
                    item.Visible = false; 
                }
            }
        }

        public static void Initialize(ContentManager content)
        {
            Elements = new List<GUIelement>();
            //add all UI elements here
            Elements.Add(new GUIelement("Main Menu", true, new Rectangle((int)Camera.camera.BoundingRectangle.X, (int)Camera.camera.BoundingRectangle.Y, (int)Camera.camera.BoundingRectangle.Width, (int)Camera.camera.BoundingRectangle.Height), content.Load<Texture2D>("splash")));
        }
    }

    class GUIelement : Entity
    {
        public bool Visible;

        public GUIelement(string name,bool defaultVisibility,Rectangle size, Texture2D Sprite)
        {
            Visible = defaultVisibility;
            Name = name;
            sprite = Sprite;
            spriteBox = size;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(sprite,spriteBox,Color.White);
            }
        }

        public override void Initialize(ContentManager content, Vector2 position)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
