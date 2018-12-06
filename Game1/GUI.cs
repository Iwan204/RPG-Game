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
        public static string Console;

        private static SpriteBatch spriteBatch;

        private static GraphicsDevice graphicsDevice;

        //public static List<GUIelement> Elements;

        public static List<Button> MenuGui;
        public static List<Button> NewGameGui;

        public static SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        private static Texture2D buttonTexture;

        public static void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred);
            //foreach (var item in Elements)
            //{
            //    item.Draw(SpriteBatch);
            //}
            SpriteBatch.End();
        }

        public static void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
               // foreach (var item in Elements)
               // {
                //    item.Visible = false; 
               // }
            }
        }

        public static void UpdateMainMenu(Rectangle MouseRect)
        {
            foreach (var button in MenuGui)
            {
                button.Update(MouseRect);
            }
        }

        public static void UpdateNewGameMenu(Rectangle MouseRect)
        {
            foreach (var button in NewGameGui)
            {
                button.Update(MouseRect);
            }
        }

        public static void DrawNewGameMenu(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred);
            foreach (var item in NewGameGui)
            {
                item.Draw(SpriteBatch);
            }
            SpriteBatch.End();
        }

        public static void DrawMainMenu(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred);
            foreach (var item in MenuGui)
            {
                item.Draw(SpriteBatch);
            }
            SpriteBatch.End();
        }

        public static void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            //buttonTexture = content.Load<Texture2D>("256x64button");
            //GUIelement NewGameButton = new GUIelement("New Game Button",true,new Rectangle(),buttonTexture);
            //NewGameButton.spriteBox = new Rectangle(0, 0, 256, 64);
            //Elements = new List<GUIelement>();
            //add all UI elements here
            //Elements.Add(NewGameButton);
            //Elements.Add(new GUIelement("Main Menu", false, new Rectangle((int)Camera.camera.BoundingRectangle.X, (int)Camera.camera.BoundingRectangle.Y, (int)Camera.camera.BoundingRectangle.Width, (int)Camera.camera.BoundingRectangle.Height), content.Load<Texture2D>("splash")));
            Console = "";
            MenuGui = new List<Button>();
            MenuGui.Add(new Button(new Point((graphics.Viewport.Width / 2) - 128, graphics.Viewport.Bounds.Bottom - 64),"New Game",content, ButtonAction.NewGame));
            MenuGui.Add(new Button(new Point((graphics.Viewport.Width / 2) - 384, graphics.Viewport.Bounds.Bottom - 64), "Load", content, ButtonAction.LoadGame));
            MenuGui.Add(new Button(new Point((graphics.Viewport.Width / 2) + 128, graphics.Viewport.Bounds.Bottom - 64), "Quit", content, ButtonAction.Quit));

            NewGameGui = new List<Button>();
            NewGameGui.Add(new Button(new Point(0, 0),"Default",content,ButtonAction.PlayerClass));
        }
    }

    public enum ButtonAction
    {
        NewGame,
        LoadGame,
        Quit,

        PlayerClass,
    }


    public class Button
    {
        private Texture2D texture;
        private Rectangle bounds;
        private Point buttonSize;
        private ButtonAction buttonAction;
        private ContentManager content;


        public Button(Point origin,string Text, ContentManager content, ButtonAction action)
        {
            buttonSize = new Point(256,64);
            bounds = new Rectangle(origin, buttonSize);
            texture = content.Load<Texture2D>("256x64button");
            buttonAction = action;
        }

        public void Update(Rectangle MouseRect)
        {
            var mouseclick = Mouse.GetState().LeftButton;
            var mousePos = Mouse.GetState().Position;
            if (mouseclick == ButtonState.Pressed)
            {
                //clicked
                if (bounds.Contains(mousePos))
                {
                    Clicked();
                }
            }
        }

        public void Clicked()
        {
            switch (buttonAction)
            {
                case ButtonAction.NewGame:
                    GameManager.ToNewGame(content);
                    break;
                case ButtonAction.LoadGame:
                    break;
                case ButtonAction.Quit:
                    break;
                case ButtonAction.PlayerClass:
                    if (GameManager.gameState == GameState.NewGame)
                    {
                        
                        
                    }
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
            
        }
    }

    static class Fonts
    {
        public static SpriteFont MenuFont;

        public static void Initialise(ContentManager content)
        {
            //MenuFont = content.Load<SpriteFont>("MenuFont");
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
