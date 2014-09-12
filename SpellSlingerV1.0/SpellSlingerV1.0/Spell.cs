using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace SpellSlingerV1._0
{



    class Spell : Entity
    {
        SPELL_TYPE type;
        int spellLevel;
        float damage;
        float damagePerTick;
        Timer activeTimer;
        int activeTime;
        int spellCooldown;

        float initialDamage;
        //Timer damageTimer;

        public Spell(SPELL_TYPE type_, int spellLevel_, float x_, float y_)
        {
            type = type_;
            spellLevel = spellLevel_;
            Active = true;
            Width = 64;
            Height = 64;
            initialDamage = 0;

            X = x_;
            Y = y_;

//            Debug.WriteLine("spellX" + X + "spellY" + Y);

            //Set spell to be 'active' - currently being used to control draw time on screen
            activeTime = 500;

            //Split into own functions later
            switch (type)
            {
                case SPELL_TYPE.FIREBALL:
                    initialDamage = 100 * (1 + spellLevel * 0.1f);
                    break;
                case SPELL_TYPE.ICELANCE:
                    initialDamage = 75 * (1 + spellLevel * 0.1f);
                    break;
                case SPELL_TYPE.LIGHTNING:
                    initialDamage = 30 * (1 + spellLevel * 0.1f);
                    break;
                case SPELL_TYPE.DESPAIR:
                    initialDamage = 20 * (1 + spellLevel * 0.1f);
                    break;
                case SPELL_TYPE.RAPTURE:
                    initialDamage = 10 * (1 + spellLevel * 0.1f);
                    break;
                default:
                    initialDamage = 0;
                    break;
            }

            switch (type)
            {
                case SPELL_TYPE.FIREBALL:
                    damagePerTick = 0;
                    break;
                case SPELL_TYPE.ICELANCE:
                    damagePerTick = 0;
                    break;
                case SPELL_TYPE.LIGHTNING:
                    damagePerTick = initialDamage * 0.03f;
                    break;
                case SPELL_TYPE.DESPAIR:
                    damagePerTick = initialDamage * 0.03f;
                    break;
                case SPELL_TYPE.RAPTURE:
                    damagePerTick = initialDamage * 0.03f;
                    break;
                default:
                    damagePerTick = 0;
                    break;
            }

            switch (type)
            {
                case SPELL_TYPE.FIREBALL:
                    spellCooldown = 2000;
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

            damage = initialDamage;

            //Spell Timer
            activeTimer = new System.Timers.Timer(1000);  //1 second interval
            activeTimer.Elapsed += OnTimedEvent;
            activeTimer.Interval = activeTime;
            activeTimer.Enabled = true;
            activeTimer.Start();

            ////Spell Damage Timer
            //damageTimer = new System.Timers.Timer(1000);
            //damageTimer.Elapsed += OnTimedEventDamage;
            //damageTimer.Interval = 100; //tick length, 100 milliseconds
            //damageTimer.Enabled = true; //Start enables... why keep putting this in?
            //damageTimer.Start();
        }

        public void InitialHitFinished()
        {
            damage = damagePerTick;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Active = false;
        }

        //private void OnTimedEventDamage(object source, ElapsedEventArgs e)
        //{
        //    damage = damagePerTick;
        //}

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

        public float Damage
        {
            //make sure the first time we get initial damage. 
            //every time after, damagePerTick
            get 
            {                
                return damage; 
            }
            set { damage = value; }
        }

        

        public int SpellCooldown
        {
            get { return spellCooldown; }
            set { spellCooldown = value; }
        }

        public float DamagePerTick
        {
            get { return damagePerTick; }
            set { damagePerTick = value; }
        }

    }
}