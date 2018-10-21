using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    public enum GameState
    {
        MainMenu,
        Pause,
        GameplayLoop,
        Combat,
    }

    static class GameManager
    {
        public static GameState gameState;

        public static void Initialise()
        {
            gameState = GameState.MainMenu; //set default gamestate

        }
        public static void NewGame()
        {
            MapHandler.LoadMap("ObjectTest");
            Console.WriteLine("New Game started");
            Camera.camera.Position = new Vector2(0,0);
            gameState = GameState.GameplayLoop;
        }
    }
}
