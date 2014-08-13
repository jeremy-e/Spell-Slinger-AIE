using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SpellSlingerV1._0
{
    public enum TYPE
    {
        TOWER,
        ENEMY1,
        ENEMY2
    }

	
// CHANGE 2

    class Entity
    {
        private Texture2D texture;
        private float x;
        private float y;
        private int width;
        private int height;
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        //private bool active;
        
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

    }
}
