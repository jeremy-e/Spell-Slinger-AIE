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

            //Let's get centre of entity_b for a little more realism/accuracy
            float entity_b_X = entity_b.X + (entity_b.Width * 0.5f);
            float entity_b_Y = entity_b.Y + (entity_b.Height * 0.5f);

            if (entity_b_X >= entity_a_LH && entity_b_X <= entity_a_RH && entity_b_Y <= entity_a_B && entity_b_Y >= entity_a_T)
            {
                //Temporary for testing - set object to inactive for list clean up - Pass responsibility to enemy directly
                entity_b.Active = false;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
