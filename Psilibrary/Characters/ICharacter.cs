using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Psilibrary.TileEngine;

namespace Psilibrary.CharacterComponents
{
    public enum CharacterAttributes
    {
        Strength,
        Perception,
        Endurance,
        Charisma,
        Intellect,
        Agility,
        Luck
    }

    public struct Modifier
    {
        public int Duration;
        public int Amount;
        public bool Permanent;

        public Modifier(int duration, int amount, bool permanent = false)
        {
            Duration = duration;
            Amount = amount;
            Permanent = permanent;
        }

        public static Modifier Zero
        {
            get { return new Modifier() { Amount = 0, Duration = 0 }; }
        }
    }

    public interface ICharacter
    {
        string Name { get; }
        string SpriteName { get; }
        AnimatedSprite Sprite { get; }
        string PortraitName { get; }
        bool IsRecruited { get; }
        bool InParty { get; }
        bool Gender { get; set; }
        bool IsMerchant { get; }
        bool HasQuest { get; }
        bool IsSpellcaster { get; }
        long Gold { get; }
        long Experience { get; }

        Dictionary<CharacterAttributes, int> Attributes { get; }
        Dictionary<CharacterAttributes, List<Modifier>> AttributeModifiers { get; }

        int CurrentHealth { get; }
        int MaximumHealth { get; }
        int CurrentMana { get; }
        int MaximumMana { get; }

        void UpdateGold(int amount);
        void UpdateExperience(int amount);
        void UpdateHealth(int amount);
        void UpdateMana(int amount);
        void AddToParty();
        void RemoveFromParty();
        void Recruit();

        Color ManaColor();
    }
}
