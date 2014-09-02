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
        protected Texture2D texture;
        protected Vector2 pos;
        protected Vector2 pos2; //Position for centre of tower
        private int width;
        private int height;
        private float rotation;

        protected Color drawColour = Color.White;


        //Will allow for iteration through lists and if false object will be removed - ie dead enemies, spells cast
        private bool active;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Vector2 Pos
        {
            get { return pos; }
        }

        public Vector2 Pos2
        {
            get { return pos2; }
        }

        public float X
        {
            get { return pos.X; }
            set
            {
                pos.X = value;
                pos2.X = value + Width*0.5f;
            }

        }

        public float Y
        {
            get { return pos.Y; }
            set
            {
                pos.Y = value;
                pos2.Y = value + Height*0.5f;
            }
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

        public Color DrawColor
        {
            get { return drawColour; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
    }
}
