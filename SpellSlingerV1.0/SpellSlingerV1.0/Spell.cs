using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{



    class Spell : Entity
    {
        SPELL_TYPE type;
        int spellLevel;

        public Spell(SPELL_TYPE type_, int spellLevel_, float x_, float y_)
        {
            type = type_;
            spellLevel = spellLevel_;

            Width = 128;
            Height = 128;

            X = x_ - Width/2;
            Y = y_ - Height/2;
        }

    }
}
