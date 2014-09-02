﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Timers;
using Microsoft.Xna.Framework;

namespace SpellSlingerV1._0
{
    class Tower : Entity
    {
        int capacity;
        int maxCap = 15;
        int essence;
        List<int> spellLevel;
        Timer spellTimer;
        bool spellCast;                                                     //Has player cast a spell

        public Tower()
        {
            //Add initial spell level 1 to all spells.
            spellLevel = new List<int>();
            for (int i = 0; i < Enum.GetNames(typeof(SPELL_TYPE)).Length; i++)
            {
                spellLevel.Add(2);
            }

            this.Width = 64;
            this.Height = 64;
            X = Game1.SCREEN_WIDTH / 2 - Width / 2;
            Y = Game1.SCREEN_HEIGHT / 2 - Width / 2;
            Active = true;
            capacity = 0;

            spellTimer = new System.Timers.Timer();
            spellTimer.Elapsed += OnTimedEvent;
            spellTimer.Interval = 500;                          //Set player global cooldown
            spellCast = false;
        }

        public void Update(GameTime gameTime_)
        {
            if (capacity >= maxCap)
            {
                //Debug.WriteLine("You have been overwhelmed!");
            }

            if (spellCast && !spellTimer.Enabled)               //spell cast & timer not enabled
            {
                spellTimer.Start();                             //Start global cooldown
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            spellCast = false;
        }

        public int Essence
        {
            get { return essence; }
            set { essence = value; }
        }

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        public List<int> SpellLevel
        {
            get { return spellLevel; }
            set { spellLevel = value; }
        }

        public bool SpellCast
        {
            get { return spellCast; }
            set { spellCast = value; }
        }
    }
}
