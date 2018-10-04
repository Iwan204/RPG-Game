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
    public static class GameStateManager
    {
        static private int StateID;

        static public int stateID
        {
            get { return StateID; }
            set { StateID = stateID; }
        }

        public static void Initialize()
        {
            stateID = 0;
        }

        public static void Update()
        {

        }

    }
}
