using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SpellSlingerV1._0
{
    class ViewPort
    {
        int viewPortWidth;
        int viewPortHeight;
        int focusAreaX;
        int focusAreaY;
        SpriteBatch spriteBatch;
        Vector2 snapPosition;

        //PC: is the user currently holding W,A,S or D down?
        bool viewSnappedState;

        

        public ViewPort(SpriteBatch spriteBatch_, int viewPortWidth_, int viewPortHeight_)
        {
            spriteBatch = spriteBatch_;
            viewPortWidth = viewPortWidth_;
            viewPortHeight = viewPortHeight_;
            focusAreaX = 0;
            focusAreaY = 0;
            viewSnappedState = false;
            snapPosition = new Vector2(0.0f, 0.0f);
        }


        public void Draw(Entity entity_)
        {
            int xPos = (int)entity_.X + focusAreaX;
            int yPos = (int)entity_.Y + focusAreaY;
            Rectangle drawPos = new Rectangle(xPos, yPos, entity_.Width, entity_.Height);
            spriteBatch.Draw(entity_.Texture, drawPos, entity_.DrawColor);
        }

        public void SnapToX(float x_)
        {
            if (viewSnappedState == false)
            {
                focusAreaX -= (int)x_;
                snapPosition.X -= x_;
                viewSnappedState = true;
            }
        }

        public void SnapToY(float y_)
        {
            if (viewSnappedState == false)
            {
                focusAreaY -= (int)y_;
                snapPosition.Y -= y_;
                viewSnappedState = true;
            }
        }

        public void UnSnap()
        {
            if (viewSnappedState == true)
            {
                viewSnappedState = false;
                focusAreaX = 0;
                snapPosition.X = 0.0f;
            }
        }

        public void MoveX(int amount_)
        {
            focusAreaX += amount_;
        }
        
        public void MoveY(int amount_)
        {
            focusAreaY += amount_;
        }

        public int X
        {
            get { return focusAreaX; }
        }

        public int Y
        {
            get { return focusAreaY; }
        }

        public int ViewPortWidth
        {
            get { return viewPortWidth; }
            set { viewPortWidth = value; }
        }

        public int ViewPortHeight
        {
            get { return viewPortHeight; }
            set { viewPortHeight = value; }
        }
    }
}
