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

    static class MapHandler
    {
        public static List<TiledMap> availableMaps;
        public static TiledMapRenderer mapRenderer;
        public static TiledMap currentLevel;
        public static string mapName;
        public static TiledMapObjectLayer objectLayer;
        public static Vector2 playerSpawn;

        private static ContentManager Content;
        private static GraphicsDevice graphicsDevice;

        public static void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            Content = content;
            graphicsDevice = graphics;
            availableMaps = new List<TiledMap>();
            mapRenderer = new TiledMapRenderer(graphicsDevice);
            availableMaps.Add(content.Load<TiledMap>("DEMO2"));
            availableMaps.Add(content.Load<TiledMap>("ObjectTest"));
            currentLevel = availableMaps.First();
            objectLayer = currentLevel.GetLayer<TiledMapObjectLayer>("GameObjects");
            mapName = "ObjectTest";
            foreach (var thing in objectLayer.Objects)
            {
                if (thing.Name == "Playerspawn")
                {
                    Console.WriteLine("Object for playerspawn found, position = " + thing.Position);
                    playerSpawn = thing.Position;
                    
                }
            }

        }

        public static void LoadMap(string loadMapName)
        {
            //if the current map is loaded
            //note: make map, mapname and objectlayer a struct called level for easy access
            if (loadMapName != mapName)
            {
                foreach (var map in availableMaps)
                {
                    var nameToComapre = "";
                    map.Properties.TryGetValue("MapName", out nameToComapre);
                    if (nameToComapre == loadMapName)
                    {
                        //map found in available maps
                        currentLevel = map;
                        objectLayer = currentLevel.GetLayer<TiledMapObjectLayer>("GameObjects");
                        mapName = loadMapName;
                        //set parameters from objects in map
                        foreach (var entity in objectLayer.Objects)
                        {
                            //camerastart parameter
                            if (entity.Name == "CameraStart")
                            {
                                Camera.camera.Position = entity.Position;
                            }
                        }
                    }
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            mapRenderer.Update(currentLevel, gameTime);
        }

        public static void UpdateMainMenu(GameTime gameTime)
        {
            LoadMap("Demo");
            mapRenderer.Update(currentLevel, gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Camera.camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(currentLevel, Camera.camera.GetViewMatrix());
            spriteBatch.End();
        }

        public static void DrawMainMenu(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Begin(transformMatrix: Camera.camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(currentLevel, Camera.camera.GetViewMatrix());
            spriteBatch.End();
        }

        public static void DrawLayer(SpriteBatch spriteBatch,string LayerName)
        {
            spriteBatch.Begin(transformMatrix: Camera.camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            try
            {
                TiledMapLayer layer = currentLevel.GetLayer(LayerName);
                mapRenderer.Draw(layer, Camera.camera.GetViewMatrix());
            }
            catch (Exception)
            {

                //if no layer exists nothing will happen
            }
            spriteBatch.End();
        }

    }

 
}
