using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Psilibrary.TileEngine;
using Psilibrary;
using System.IO;

namespace Psilibrary.CharacterComponents
{
    public class Character : ICharacter
    {
        #region Constant

        public const float SpeakingRadius = 96f;
        public const float ActivateRadius = 96f;
        public const int MaximumActions = 4;

        #endregion

        #region Field Region

        private static List<Texture2D> actionIconTextures = new List<Texture2D>();
        private AnimatedSprite sprite;
        private HeroDefinition definition;
        private string name;
        private TimeSpan elapsedTime;
        private TimeSpan totalTime;
        private string className;
        private string spriteName;
        private string portraitName;
        private string conversation;
        private Texture2D portrait;
        private static Game gameRef;
        private bool isMerchant;
        private bool hasQuest;
        private bool isSpellcaster;

        private bool gender;
        private bool isRecruited;
        private bool inParty;
        private long gold;
        private long experience;
        private int level;
        private Dictionary<CharacterAttributes, int> attributes = new Dictionary<CharacterAttributes, int>();
        private Dictionary<CharacterAttributes, List<Modifier>> modifiers = new Dictionary<CharacterAttributes, List<Modifier>>();
        private int currentHealth, maximumHealth;
        private int currentMana, maximumMana;

        #endregion

        #region Property Region

        public string Name
        {
            get { return name; }
        }

        public string ClassName
        {
            get { return className; }
        }

        public AnimatedSprite Sprite
        {
            get { return sprite; }
        }

        public string Conversation
        {
            get { return conversation; }            
        }

        public Texture2D Portrait
        {
            get { return portrait; }
        }

        public string SpriteName
        {
            get { return spriteName; }
        }

        public string PortraitName
        {
            get { return portraitName; }
        }

        public bool Gender
        {
            get { return gender; }
            set { gender = value;  }
        }

        public bool IsMerchant
        {
            get { return isMerchant; }
        }

        public bool IsSpellcaster
        {
            get { return isSpellcaster; }
        }

        public bool IsRecruited
        {
            get { return isRecruited; }
        }

        public bool InParty
        {
            get { return inParty; }
        }

        public Dictionary<CharacterAttributes, int> Attributes
        {
            get { return attributes; }
        }

        public Dictionary<CharacterAttributes, List<Modifier>> AttributeModifiers
        {
            get { return modifiers; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
        }

        public int MaximumHealth
        {
            get { return maximumHealth; }
        }

        public int CurrentMana
        {
            get { return currentMana; }
        }

        public int MaximumMana
        {
            get { return maximumMana; }
        }

        public bool HasQuest
        {
            get { return hasQuest; }
        }

        public long Gold
        {
            get { return gold; }
        }

        public int Level
        {
            get { return level; }
        }

        public long Experience
        {
            get { return experience; }
        }

        public TimeSpan ElapasedTime
        {
            get { return elapsedTime; }
        }

        public TimeSpan TotalTime
        {
            get { return totalTime; }
        }

        #endregion

        #region Constructor Region

        private Character()
        {
            level = 1;

            foreach (CharacterAttributes attr in Enum.GetValues(typeof(CharacterAttributes)))
            {
                attributes.Add(attr, 10);
            }
        }

        #endregion

        #region Method Region

        private static void BuildAnimations()
        {
        }

        public static Character FromString(Game game, string characterString)
        {

            Character character = new Character();
             return character;
        }

        public static Character FromDefinitioin(HeroDefinition definition, string data)
        {
            Character c = new Character();

            c.definition = definition;
            c.className = definition.ClassName;
            c.currentHealth = definition.StartingHealth;
            c.currentMana = definition.StartingMana;
            c.isSpellcaster = definition.IsSpellcaster;

            foreach (var a in definition.AttributeBonuses.Keys)
            {
                c.Attributes[a] += definition.AttributeBonuses[a];
            }

            c.maximumHealth = c.currentHealth;
            c.maximumMana = c.currentMana;
            return c;
        }

        public void SetConversation(string newConversation)
        {
            this.conversation = newConversation;
        }

        public static void Save(string characterName)
        {

        }
        

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
            elapsedTime = gameTime.ElapsedGameTime;
            totalTime += elapsedTime;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch);
        }

        #endregion

        public void AddToParty()
        {
            if (!inParty)
                inParty = true;
        }

        public void RemoveFromParty()
        {
            if (inParty)
                inParty = false;
        }

        public void Recruit()
        {
            if (!isRecruited)
                isRecruited = true;
        }

        public void UpdateGold(int amount)
        {
            gold += amount;
        }

        public void UpdateExperience(int amount)
        {
            experience += amount;
        }

        public void UpdateHealth(int amount)
        {
            currentHealth += amount;

            if (currentHealth > maximumHealth)
                currentHealth = maximumHealth;
        }

        public void UpdateMana(int amount)
        {
            currentMana += amount;

            if (currentMana > maximumMana)
                currentMana = maximumMana;
        }

        public Color ManaColor()
        {
            return IsSpellcaster ? Color.Blue : Color.Green;
        }
        
        public void ResetTime()
        {
            elapsedTime = TimeSpan.Zero;
            totalTime = TimeSpan.Zero;
        }

    }
}
