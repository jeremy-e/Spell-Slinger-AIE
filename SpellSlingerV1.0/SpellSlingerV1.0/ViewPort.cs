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

        public ViewPort(SpriteBatch spriteBatch_, int viewPortWidth_, int viewPortHeight_)
        {
            spriteBatch = spriteBatch_;
            viewPortWidth = viewPortWidth_;
            viewPortHeight = viewPortHeight_;
            focusAreaX = 0;
            focusAreaY = 0;
        }

        public void Draw(Entity entity_)
        {
            int xPos = (int)entity_.X + focusAreaX;
            int yPos = (int)entity_.Y + focusAreaY;
            Rectangle drawPos = new Rectangle(xPos, yPos, entity_.Width, entity_.Height);
            spriteBatch.Draw(entity_.Texture, drawPos, Color.White);
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
    }
}
