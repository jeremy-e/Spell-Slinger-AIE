using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SpellSlingerV1._0
{
    
    public class Circle
    {
        Vector2 centre;
        public double radius;
        //Random random;
        private static Random random; 

        public Circle(Vector2 centre_, double radius_)
        {
            centre = centre_;
            radius = radius_;
            
            if (random == null)
            {
                int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                random = new Random(seed); 
            }

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

        public Vector2 RandomPoint()
        {
            //Math.PI * 2 * 1000 to increase accuracy. 
            int result = random.Next(0, (int)(Math.PI * 2 * 1000.0));
            double realResult = result / 1000.0;
            Debug.WriteLine("circle result: " + realResult);
            return GetPoint(realResult);
        }        
    }
}