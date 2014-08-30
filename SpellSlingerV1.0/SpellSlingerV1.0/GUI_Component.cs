using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    class GUI_Component : Entity
    {
        float offsetX;
        float offsetY;

        public GUI_Component(float x_, float y_, int width_, int height_, bool active_)
        {
            Width = width_;
            Height = height_;
            X = x_;
            Y = y_;
            offsetX = X;
            offsetY = Y;
            Active = active_;
        }

        public void Update(float x_, float y_)
        {
            X = offsetX - x_;
            Y = offsetY - y_;
        }

    }
}
