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
        Timer damageTimer;
        int tickCount;
        
        public Spell(SPELL_TYPE type_, int spellLevel_, float x_, float y_)
        {
            type = type_;
            spellLevel = spellLevel_;
            Active = true;
            Width = 64;
            Height = 64;
            damage = 0;
            tickCount = 0;
            initialDamage = 0;

            X = x_ - Width / 2;
            Y = y_ - Height / 2;

            //Set spell to be 'active' - currently being used to control draw time on screen
            activeTime = 500;

            //Split into own functions later
            switch (type)
            {
                case SPELL_TYPE.FIREBALL:
                    initialDamage = 50 * (1 + spellLevel * 0.1f);
                    break;
                case SPELL_TYPE.ICELANCE:
                    initialDamage = 40 * (1 + spellLevel * 0.1f);
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
                    damagePerTick = initialDamage * 0.1f;
                    break;
                case SPELL_TYPE.DESPAIR:
                    damagePerTick = initialDamage * 0.1f;
                    break;
                case SPELL_TYPE.RAPTURE:
                    damagePerTick = initialDamage * 0.1f;
                    break;
                default:
                    damagePerTick = 0;
                    break;
            }

            switch (type)
            {
                case SPELL_TYPE.FIREBALL:
                    spellCooldown = 2000;                //Switch to milliseconds and incorporate timers 
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

            //Spell Timer
            activeTimer = new System.Timers.Timer(1000);  //1 second interval
            activeTimer.Elapsed += OnTimedEvent;
            activeTimer.Interval = activeTime;
            activeTimer.Enabled = true;
            activeTimer.Start();

            //Spell Damage Timer
            damageTimer = new System.Timers.Timer(1000);
            damageTimer.Elapsed += OnTimedEventDamage;
            damageTimer.Interval = 100; //tick length, 100 milliseconds
            damageTimer.Enabled = true; //Start enables... why keep putting this in?
            damageTimer.Start();
        }
                
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Active = false;
            damageTimer.Stop();
        }

        private void OnTimedEventDamage(object source, ElapsedEventArgs e)
        {
            if (tickCount == 0)
            {
                damage = initialDamage;
            }
            else
            {
                damage = damagePerTick;
            }
            tickCount++;
            //Debug.WriteLine("[Damage Timer On] BOOM SPELL CAST: " + type + ". SPELL DAMAGE: " + damage);            
            damageTimer.Stop();
            damageTimer.Start();
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

        public float Damage
        {
            get { return damage; }
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