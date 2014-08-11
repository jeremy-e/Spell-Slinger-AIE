using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{

    class Factory
    {
        public Factory()                                        //Default Constructor
        {

        }

        public Entity CreateObject(string objectName)
        {
            if (objectName == "tower")
            {
                Tower tower = new Tower();
                return tower;
            }

            if (objectName == "enemy")
            {
                Enemy enemy = new Enemy();
                return enemy;
            }

            return null;
        }

    }
}
