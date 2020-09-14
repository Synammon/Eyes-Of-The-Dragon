using Psilibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Psilibrary.CharacterComponents
{
    public abstract class HeroDefinition
    {
        protected string name;
        protected Dictionary<CharacterAttributes, int> attributeBonuses = new Dictionary<CharacterAttributes, int>();
        protected bool isSpellcaster;
        protected int startingHealth;
        protected int startingMana;
        protected int healthLevelBonus;
        protected int manaLevelBonus;

        public string ClassName
        {
            get { return name; }
        }

        public bool IsSpellcaster
        {
            get { return isSpellcaster; }
        }

        public Dictionary<CharacterAttributes, int> AttributeBonuses
        {
            get { return attributeBonuses; }
        }

        public int StartingHealth
        {
            get { return startingHealth; }
        }

        public int HealthLevelBonus
        {
            get { return healthLevelBonus; }
        }

        public int StartingMana
        {
            get { return startingMana; }
        }

        public int ManaLevelBonus
        {
            get { return manaLevelBonus; }
        }

        private HeroDefinition()
        {
            isSpellcaster = false;

            startingHealth = 25;
            startingMana = 25;
            healthLevelBonus = 10;
            manaLevelBonus = 10;

            foreach (CharacterAttributes attr in Enum.GetValues(typeof(CharacterAttributes)))
                attributeBonuses.Add(attr, 0);
        }

        protected HeroDefinition(Game game)
            : this()
        {
        }
    }
}
