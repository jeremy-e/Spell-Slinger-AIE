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
        int damage;
        Timer activeTimer;
        int activeTime;
        int spellCooldown;
                
        public Spell(SPELL_TYPE type_, int spellLevel_, float x_, float y_)
        {
            type = type_;
            spellLevel = spellLevel_;
            Active = true;
            Width = 64;
            Height = 64;
            damage = 0;

            X = x_ - Width / 2;
            Y = y_ - Height / 2;

            //Set spell to be 'active' - currently being used to control draw time on screen
            activeTime = 500;

            //Split into own functions later
            switch (type)
            {
                case SPELL_TYPE.FIREBALL:
                    damage = 50;
                    break;
                case SPELL_TYPE.ICELANCE:
                    damage = 40;
                    break;
                case SPELL_TYPE.LIGHTNING:
                    damage = 30;
                    break;
                case SPELL_TYPE.DESPAIR:
                    damage = 20;
                    break;
                case SPELL_TYPE.RAPTURE:
                    damage = 10;
                    break;
                default:
                    damage = 0;
                    break;
            }

            switch (type)
            {
                case SPELL_TYPE.FIREBALL:
                    spellCooldown = 2000;                //Switch to milliseconds and incorprate timers 
                    break;
                case SPELL_TYPE.ICELANCE:
                    spellCooldown = 400;
                    break;
                case SPELL_TYPE.LIGHTNING:
                    spellCooldown = 300;
                    break;
                case SPELL_TYPE.DESPAIR:
                    spellCooldown = 200;
                    break;
                case SPELL_TYPE.RAPTURE:
                    spellCooldown = 100;
                    break;
                default:
                    spellCooldown = 10;
                    break;
            }

            activeTimer = new System.Timers.Timer(1000);  //1 second interval
            activeTimer.Elapsed += OnTimedEvent;
            activeTimer.Interval = activeTime;
            activeTimer.Enabled = true;
            activeTimer.Start();

            Debug.WriteLine("BOOM SPELL CAST: " + type + ". SPELL LEVEL: " + spellLevel + ". SPELL DAMAGE: " + damage);
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
            get { return damage; }
            set { damage = value; }
        }

        public int SpellCooldown
        {
            get { return spellCooldown; }
            set { spellCooldown = value; }
        }

    }
}