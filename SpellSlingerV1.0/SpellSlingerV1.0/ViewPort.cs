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

        int aimAreaX;
        int aimAreaY;

        int focusAreaX;
        int focusAreaY;

        const int VIEW_PORT_SPEED = 30;

        SpriteBatch spriteBatch;
        Vector2 snapPosition;
        Rectangle drawPos;

        //PC: is the user currently holding W,A,S or D down?
        bool viewLeftSnappedState;
        bool viewRightSnappedState;
        bool viewUpSnappedState;
        bool viewDownSnappedState;

        

        public ViewPort(SpriteBatch spriteBatch_, int viewPortWidth_, int viewPortHeight_)
        {
            spriteBatch = spriteBatch_;
            viewPortWidth = viewPortWidth_;
            viewPortHeight = viewPortHeight_;
            focusAreaX = 0;
            focusAreaY = 0;
            aimAreaX = 0;
            aimAreaY = 0;

            viewLeftSnappedState = false;
            viewRightSnappedState = false;
            viewUpSnappedState = false;
            viewDownSnappedState = false;

            snapPosition = new Vector2(0.0f, 0.0f);
            drawPos = new Rectangle(0, 0, 0, 0);
        }


        public void Draw(Entity entity_)
        {
            int xPos = (int)entity_.X + focusAreaX;
            int yPos = (int)entity_.Y + focusAreaY;

            //dont create a new rectangle each time, thanks JK
            drawPos.Width = entity_.Width;
            drawPos.Height = entity_.Height;
            drawPos.X = xPos;
            drawPos.Y = yPos;
                
            spriteBatch.Draw(entity_.Texture, null, drawPos, null, entity_.Origin, entity_.Rotation, null, entity_.DrawColor, SpriteEffects.None, 0f);
            entity_.ResetDrawColour();
        }

        public void SnapToLeft(float x_)
        {
            if (viewLeftSnappedState == false)
            {
                aimAreaX += (int)x_;
                //snapPosition.X += x_;
                viewLeftSnappedState = true;
            }
        }

        public void SnapToRight(float x_)
        {
            if (viewRightSnappedState == false)
            {
                aimAreaX -= (int)x_;
                //snapPosition.X -= x_;
                viewRightSnappedState = true;
            }
        }

        public void SnapToUp(float y_)
        {
            if (viewUpSnappedState == false)
            {
                aimAreaY -= (int)y_;
                //snapPosition.Y -= y_;
                viewUpSnappedState = true;
            }
        }

        public void SnapToDown(float y_)
        {
            if (viewDownSnappedState == false)
            {
                aimAreaY -= (int)y_;
                //snapPosition.Y += y_;
                viewDownSnappedState = true;
            }
        }

        public void UnSnapLeft()
        {
            if (viewLeftSnappedState == true)
            {
                viewLeftSnappedState = false;
                aimAreaX = 0;
            }
        }
        
        public void UnSnapRight()
        {
            if (viewRightSnappedState == true)
            {
                viewRightSnappedState = false;
                aimAreaX = 0;
            }
        }
        public void UnSnapUp()
        {
            if (viewUpSnappedState == true)
            {
                viewUpSnappedState = false;
                aimAreaY = 0;
            }
        }

        public void UnSnapDown()
        {
            if (viewDownSnappedState == true)
            {
                viewDownSnappedState = false;
                aimAreaY = 0;
            }
        }

        public void Update()
        {
            if (focusAreaX < aimAreaX)
                focusAreaX += VIEW_PORT_SPEED;
            if (focusAreaX > aimAreaX)
                focusAreaX -= VIEW_PORT_SPEED;
            if (focusAreaY < aimAreaY)
                focusAreaY += VIEW_PORT_SPEED;
            if (focusAreaY > aimAreaY)
                focusAreaY -= VIEW_PORT_SPEED;
        }



        private void MoveX(int amount_)
        {
            focusAreaX += amount_;
        }
        
        private void MoveY(int amount_)
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
