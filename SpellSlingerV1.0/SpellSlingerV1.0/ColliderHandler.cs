using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SpellSlingerV1._0
{
    class ColliderHandler
    {

        public bool Collider(Entity entity_a, Entity entity_b)
        {
            float entity_a_LH = entity_a.X; 	                //LH
            float entity_a_RH = entity_a.X + entity_a.Width;	//RH
            float entity_a_T = entity_a.Y;	                    //Top
            float entity_a_B = entity_a.Y + entity_a.Height;	//Btm

            //Debug.WriteLine("T LH: " + entity_a_LH);
            //Debug.WriteLine("T RH: " + entity_a_RH);
            //Debug.WriteLine("T Top: " + entity_a_T);
            //Debug.WriteLine("T Btm: " + entity_a_B);

            //Debug.WriteLine("Enemy x pos: " + entity_b.X);
            //Debug.WriteLine("Enemy Y Pos: " + entity_b.Y);

            if (entity_b.X >= entity_a_LH && entity_b.X <= entity_a_RH && entity_b.Y <= entity_a_B && entity_b.Y >= entity_a_T)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
