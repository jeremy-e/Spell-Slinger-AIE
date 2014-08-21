using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpellSlingerV1._0
{
    class Circle
    {
        Vector2 centre;
        double radius; 

        public Circle(Vector2 centre_, double radius_)
        {
            centre = centre_;
            radius = radius_;
        }

        public Vector2 GetPoint(double angle_)
        {
            Vector2 ret = new Vector2();
            float sine = (float) (Math.Sin(angle_) * radius);
            float cosine = (float)(Math.Cos(angle_) * radius);
            ret.X = centre.X + cosine;
            ret.Y = centre.Y + sine;                       
            return ret;
        }
    }
}
