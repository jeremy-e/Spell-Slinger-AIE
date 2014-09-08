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
        bool viewXSnappedState;
        bool viewYSnappedState;

        

        public ViewPort(SpriteBatch spriteBatch_, int viewPortWidth_, int viewPortHeight_)
        {
            spriteBatch = spriteBatch_;
            viewPortWidth = viewPortWidth_;
            viewPortHeight = viewPortHeight_;
            focusAreaX = 0;
            focusAreaY = 0;
            viewXSnappedState = false;
            viewYSnappedState = false;
            snapPosition = new Vector2(0.0f, 0.0f);
        }


        public void Draw(Entity entity_)
        {
            int xPos = (int)entity_.X + focusAreaX;
            int yPos = (int)entity_.Y + focusAreaY;
            Rectangle drawPos = new Rectangle(xPos, yPos, entity_.Width, entity_.Height);

            if ( entity_ is Enemy )
            {
                Enemy enemy = (Enemy)entity_;
                spriteBatch.Draw(entity_.Texture, null, drawPos, null, entity_.Pos2, enemy.Rotation, null, entity_.DrawColor, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(entity_.Texture, drawPos, entity_.DrawColor );
            }
        }

        public void SnapToX(float x_)
        {
            if (viewXSnappedState == false)
            {
                focusAreaX -= (int)x_;
                snapPosition.X -= x_;
                viewXSnappedState = true;
            }
        }

        public void SnapToY(float y_)
        {
            if (viewYSnappedState == false)
            {
                focusAreaY -= (int)y_;
                snapPosition.Y -= y_;
                viewYSnappedState = true;
            }
        }

        public void UnSnapX()
        {
            if (viewXSnappedState == true)
            {
                viewXSnappedState = false;
                focusAreaX = 0;
            }
        }
        
        public void UnSnapY()
        {
            if (viewYSnappedState == true)
            {
                viewYSnappedState = false;
                focusAreaY = 0;
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
