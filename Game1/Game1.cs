using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    ///
     
    [Serializable]
    public struct stats
    {
        int Health;
        int Speed;
        int DT; //damage threshold
        int DR; //damage resistance
    }

    [Serializable]
    public struct AttributesStruct
    {
        int Strength;
        int Dexterity;
        int Constitution;
        int Intelligence;
        int Charisma;
        int Age;
    }

    [Serializable]
    public struct PlayerData
    {
        public stats stats;
        public string name;
        public int PositionX;
        public int PositionY;
        public string SpritePath;
        public int width;
        public int height;
        public int Elevation;
    }

    [Serializable]
    public struct SaveGame
    {
        public string SaveName;
        public List<PlayerData> SavedPlayerData;
        //save nonplayer objects
        //save names of available maps
        public string SavedCurrentLevelName;
        //save other
    }




    public class Game1 : Game
    {
        //Final Variables
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Cursor cursor;
        public string ConsoleOutput;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Final Initialisations
            GameManager.Initialise();
            Camera.Initialise(GraphicsDevice);
            PlayerManager.Initialize(Content,GraphicsDevice);
            GUI.Initialize(Content,GraphicsDevice);
            MapHandler.Initialize(Content, GraphicsDevice);
            cursor = new Cursor(Content);

            //Player = Content.Load<Texture2D>("player");

            //PlayerManager.NewPlayer("Test Character 2", new stats(), Vector2.Zero, Player);
            //PlayerManager.NewPlayer("Test Character 3", new stats(), new Vector2(0,100), Player)

            ConsoleStreamWriter ConsWtr = new ConsoleStreamWriter(ConsoleOutput);
            //Console.SetOut(ConsWtr);


            base.Initialize();
        }


        //old - update
        public void SaveGame()
        {
            SaveGame Save = new SaveGame();

            Save.SaveName = "default";
            Save.SavedPlayerData = PlayerManager.GetPlayerParty();
            Save.SavedCurrentLevelName = MapHandler.currentLevel.MapName;
            //actually write the file here:
            Stream streamwrite = File.Create("default.bin");

            BinaryFormatter binarywrite = new BinaryFormatter();
            binarywrite.Serialize(streamwrite, Save);

            streamwrite.Close();
        }
        //old - update
        public void LoadGame()
        {
            Stream streamread = File.OpenRead("default.bin");
            BinaryFormatter binaryread = new BinaryFormatter();
            SaveGame Save = (SaveGame)binaryread.Deserialize(streamread);
            streamread.Close();

            PlayerManager.SetPlayerParty(Save.SavedPlayerData);
            MapHandler.LoadMap(Save.SavedCurrentLevelName); //WILL NOT WORK PROPERLY WITHOUT ADDITION OF CODE TO SAVE/LOAD AVAILABLE MAPS
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //Final Updates
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //gamestate switch
            switch (GameManager.gameState)
            {
                case GameState.MainMenu:
                    Camera.UpdateMainMenu(gameTime);

                    MapHandler.Update(gameTime);

                    MapHandler.UpdateMainMenu(gameTime);

                    GUI.UpdateMainMenu(cursor.boundingBox);

                    GUI.UpdateMainMenu(cursor.spriteBox);

                    break;
                case GameState.Pause:
                    break;
                case GameState.GameplayLoop: //normal gameplay
                    MapHandler.Update(gameTime);
                    PlayerManager.Update(gameTime);
                    Camera.Update();
                    GUI.Update();
                    break;
                case GameState.Combat: //in combat
                    break;
                default:
                    break;
            }

            cursor.Update(gameTime); //independent of gamestate

            //testing updates
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                SaveGame();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                LoadGame();
            }
            //Console.WriteLine(ConsoleOutput); // uncomment after text implemented

            base.Update(gameTime);
        } 

        public void UpdateMainMenu()
        {

        }

        public void UpdatePaused()
        {

        }

        protected override void Draw(GameTime gameTime)
        {
            //Final Draws
            GraphicsDevice.Clear(Color.GhostWhite);

            switch (GameManager.gameState)
            {
                case GameState.MainMenu:
                    MapHandler.DrawMainMenu(spriteBatch);
                    GUI.DrawMainMenu(spriteBatch);
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameplayLoop:
                    //Use Elevation to order draws
                    MapHandler.DrawLayer(spriteBatch, "Tile Layer 1"); //ground layer
                    MapHandler.DrawLayer(spriteBatch, "Tile Layer 2"); //decoration layer
                    PlayerManager.DrawElevation(spriteBatch, 0);
                    MapHandler.DrawLayer(spriteBatch, "Tile Layer 3"); //elevated layer
                    PlayerManager.DrawElevation(spriteBatch, 1);
                    GUI.Draw(spriteBatch);
                    break;
                default:
                    break;
            }

            cursor.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        
    }
}
