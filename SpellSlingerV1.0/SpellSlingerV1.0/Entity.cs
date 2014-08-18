using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpellSlingerV1._0
{

    class Entity
    {

        private Texture2D texture;
        //private float x;
        //private float y;
        protected Vector2 pos;
        private int width;
        private int height;
        //private int type;

       //private bool active;

        public Vector2 Pos
        {
            get { return pos; }
        }
        
        public float X
        {
            get { return pos.X; }
            set { pos.X = value; }
        }
        
        public float Y
        {
            get { return pos.Y; }
            set { pos.Y = value; }
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


        //public int Type
        //{
        //    get { return type; }
        //    set { type = value; }
        //}

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
    }
}
