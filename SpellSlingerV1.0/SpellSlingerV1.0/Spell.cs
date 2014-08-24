using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace SpellSlingerV1._0
{



    class Spell : Entity
    {
        SPELL_TYPE type;
        int spellLevel;
        Timer timer;

        public Spell(SPELL_TYPE type_, int spellLevel_, float x_, float y_)
        {
            type = type_;
            spellLevel = spellLevel_;
            Active = true;
            Width = 64;
            Height = 64;

            X = x_ - Width/2;
            Y = y_ - Height/2;

            timer = new System.Timers.Timer(1000);  //1 second interval
            timer.Elapsed += OnTimedEvent;
            timer.Interval = 500;
            timer.Enabled = true;
            timer.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Active = false;
        }
    }
}
