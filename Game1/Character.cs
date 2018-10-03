using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;


namespace Game1
{
    //31,16
    public abstract class Entity
    {
        public abstract void Initialize(ContentManager content, Vector2 position);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        private Vector2 position;
        private string name;
        private Texture2D Sprite;
        private Rectangle Spritebox;
        private Rectangle BoundingBox;
        private bool Collision;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool IsCollidable
        {
            get { return Collision; }
            set { Collision = value; }
        }

        public Rectangle spriteBox
        {
            get { return Spritebox; }
            set { Spritebox = value; }
        }

        public Rectangle boundingBox
        {
            get { return BoundingBox; }
            set { BoundingBox = value; }
        }

        public Texture2D sprite
        {
            get { return Sprite; }
            set { Sprite = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class Character : Entity
    {
        private int elevation;

        public bool IsSelected
        {
            get { return Selected; }
            set { Selected = value; }
        }

        private bool Selected;

        public int Elevation
        {
            get { return elevation; }
            set { elevation = value; }
        }
        public Character(ContentManager content, Vector2 position)
        {
            //Initialize(content, position);
        }

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Name = "Default";
            IsCollidable = true;
            IsSelected = false;
            sprite = content.Load<Texture2D>("default");
            spriteBox = new Rectangle((int)Position.X - 32, (int)Position.Y - 64, 32, 32);
            boundingBox = new Rectangle((int)Position.X - 32, (int)Position.Y - 64, 64, 32);
            Elevation = 0;
        }

        public override void Update(GameTime gameTime)
        {
            //spriteBox = new Rectangle((int)Position.X - 32, (int)Position.Y - 64, 64, 64);
            spriteBox = new Rectangle((int)Position.X - 32, (int)Position.Y - 64, 32, 32);
            boundingBox = new Rectangle((int)Position.X - 32, (int)Position.Y - 64, 64, 32);
            //updates here
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, spriteBox, Color.White);
        }

    }

    public class NonPlayer : Character
    {
        private stats stats;

        public stats Stats
        {
            get { return stats; }
            set { }
        }

        public NonPlayer(ContentManager content, Vector2 position, string name, stats stats) : base(content, position)
        {
            Initialize(content, position);
        }

        public override void Initialize(ContentManager content, Vector2 position)
        {
            base.Initialize(content, position);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    } //wip

    public class PlayerControlled : Character
    {

        private stats stats;

        public stats Stats
        {
            get { return stats; }
            set { }
        }

        public PlayerControlled(ContentManager content, Vector2 position, string name, stats stats,Texture2D Passedsprite) : base(content, position)
        {
            Initialize(content, position);
            Name = name;
            Position = position;
            sprite = Passedsprite;
        }

        public override void Initialize(ContentManager content, Vector2 position)
        {
            base.Initialize(content, position);
            sprite = content.Load<Texture2D>("player");
            Console.WriteLine("Initialisation of " + Name + " character is complete");
        }

        public override void Update(GameTime gameTime)
        {

            if (IsSelected)
            {
                DebugMove();
            }

            base.Update(gameTime);
        }

        public void DebugMove()
        {
            Vector2 positionn = Position;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //Console.WriteLine("up key pressed!");
                positionn.Y += -1;
                positionn.X += -2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //Console.WriteLine("down key pressed!");
                positionn.Y += 1;
                positionn.X += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // Console.WriteLine("right key pressed!");
                positionn.Y += -1;
                positionn.X += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                // Console.WriteLine("left key pressed!");
                positionn.Y += 1;
                positionn.X += -2;
            }
            Position = positionn;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, spriteBox, Color.White);
        }
    }

    public static class PlayerManager
    {
        private static List<PlayerControlled> PlayerParty;
        private static ContentManager Content;
        private static GraphicsDevice graphicsDevice;
        private static Vector2 SelectedVector;

        public static Vector2 selectedVector
        {
            get { return SelectedVector; }
            set { SelectedVector = selectedVector; }
        }

        public static List<PlayerControlled> GetRawPlayerParty
        {
            get { return PlayerParty; }
        }

        public static void Initialize(ContentManager content, GraphicsDevice GraphicsDevice)
        {
            PlayerParty = new List<PlayerControlled>();
            Content = content;
            graphicsDevice = GraphicsDevice;
            SelectedVector = new Vector2(0, 0);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var player in PlayerParty)
            {
                player.Update(gameTime);
                if (player.IsSelected && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Console.WriteLine("MOVING " + player.Name + " TO " + selectedVector);
                    //reset vector
                    if (player.Position == selectedVector)
                    {
                        selectedVector = new Vector2(0, 0);
                    }
                }
            }
        }

        public static void MovePlayer()
        {

        }

        public static void Draw()
        {
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            spriteBatch.Begin(transformMatrix: Camera.camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            foreach (var player in PlayerParty)
            {
                player.Draw(spriteBatch);
            }
            spriteBatch.End();
            spriteBatch.Dispose();
        }

        public static void DrawElevation(SpriteBatch spriteBatch,int elevation)
        {
            spriteBatch.Begin(transformMatrix: Camera.camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            foreach (var player in PlayerParty)
            {
                if (player.Elevation == elevation)
                {
                    player.Draw(spriteBatch);
                }
            }
            spriteBatch.End();
        }

        public static void AddPlayer(PlayerControlled player)
        {
            if (!PlayerParty.Contains(player))
            {
                PlayerParty.Add(player);
            }
        }

        public static void NewPlayer(string name, stats stats, Vector2 position, Texture2D PassedSprite)
        {
            PlayerControlled Character = new PlayerControlled(Content, position, name, stats,PassedSprite);
            PlayerParty.Add(Character);
        }

        public static void LeaveParty()
        {

        }

        public static void Kill(PlayerControlled character)
        {
            foreach (var player in PlayerParty)
            {
                if (character.Name == player.Name)
                {
                    PlayerParty.Remove(player);
                }
            }
        }

        public static List<PlayerData> GetPlayerParty()
        {
            List<PlayerData> data = new List<PlayerData>();
            foreach (var player in PlayerParty)
            {
                PlayerData Data = new PlayerData();
                Data.PositionX = (int)player.Position.X;
                Data.PositionY = (int)player.Position.Y;
                Data.name = player.Name;
                Data.SpritePath = player.sprite.Name;
                Data.Elevation = player.Elevation;
                data.Add(Data);
            }

            return data;
        }

        public static void SetPlayerParty(List<PlayerData> list)
        {
            PlayerParty.Clear();
            foreach (var data in list)
            {
                PlayerControlled player = new PlayerControlled(Content,new Vector2(data.PositionX,data.PositionY),data.name,new stats(), Content.Load<Texture2D>(data.SpritePath));
                player.Elevation = data.Elevation;
                PlayerParty.Add(player);
            }
        }
    } 

    public static class NPCmanager
    { 

        private static List<NonPlayer> CharacterList;
        private static ContentManager Content;

        public static void Initialize(ContentManager content)
        {
            CharacterList = new List<NonPlayer>();
            Content = content;
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var character in CharacterList)
            {
                character.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var character in CharacterList)
            {
                character.Draw(spriteBatch);
            }
        }

        public static void AddPlayer(NonPlayer character)
        {
            if (!CharacterList.Contains(character))
            {
                CharacterList.Add(character);
            }
        }

        public static void NewCharacter(string name, stats stats, Vector2 position)
        {
            NonPlayer Character = new NonPlayer(Content, position, name, stats);
            CharacterList.Add(Character);
        }

        public static void ToPlayer(NonPlayer character) // if NPC joins the player party
        {
            NewCharacter(character.Name, character.Stats, character.Position);
            Kill(character);
        }

        public static void Kill(NonPlayer character)
        {
            foreach (var individual in CharacterList)
            {
                if (character.Name == individual.Name)
                {
                    CharacterList.Remove(individual);
                }
            }
        }

    }  //wip
}
