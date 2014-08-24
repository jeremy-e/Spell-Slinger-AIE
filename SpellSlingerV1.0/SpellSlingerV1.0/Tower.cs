using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SpellSlingerV1._0
{
    class Tower : Entity
    {
        int capacity;
        int maxCap = 15;
        float recoveryTime = 1.0f;  //Seconds

        public Tower()
        {
            this.Width = 64;
            this.Height = 64;
            X = Game1.SCREEN_WIDTH / 2 - Width / 2;
            Y = Game1.SCREEN_HEIGHT / 2 - Width / 2;
            Active = true;
            capacity = 0;
        }

        public void Update()
        {
            if (capacity >= maxCap)
            {
                Debug.WriteLine("You have been overwhelmed!");
            }
        }


        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

    }
}
