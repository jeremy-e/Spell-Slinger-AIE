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
        Timer activeTimer;
        
        public Spell(SPELL_TYPE type_, int spellLevel_, float x_, float y_)
        {
            type = type_;
            spellLevel = spellLevel_;
            Active = true;
            Width = 64;
            Height = 64;

            X = x_ - Width / 2;
            Y = y_ - Height / 2;
                        
            activeTimer = new System.Timers.Timer(1000);  //1 second interval
            activeTimer.Elapsed += OnTimedEvent;
            activeTimer.Interval = 500;
            activeTimer.Enabled = true;
            activeTimer.Start();

            Debug.WriteLine("BOOM SPELL CAST: " + type + ". SPELL LEVEL: " + spellLevel);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Active = false;
        }

        public int SpellLevel
        {
            get { return spellLevel; }
            set { spellLevel = value; }
        }

        internal SPELL_TYPE Type
        {
            get { return type; }
            set { type = value; }
        }


        public int Damage
        {
            get
            {
                switch (type)
                {
                    case SPELL_TYPE.FIREBALL:
                        return 50;
                    case SPELL_TYPE.ICELANCE:
                        return 40;
                    case SPELL_TYPE.LIGHTNING:
                        return 30;
                    case SPELL_TYPE.DESPAIR:
                        return 20;
                    case SPELL_TYPE.RAPTURE:
                        return 10;
                    default:
                        return 0;
                }
            }
        }
    }
}