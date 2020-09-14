using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Psilibrary;

namespace Psilibrary.CharacterComponents
{
    public class WarriorHeroDefinition : HeroDefinition
    {
        public WarriorHeroDefinition(Game game)
            : base(game)
        {
            name = "Warrior";

            attributeBonuses[CharacterAttributes.Strength] = 3;
            attributeBonuses[CharacterAttributes.Endurance] = 2;
            attributeBonuses[CharacterAttributes.Agility] = 2;
        }
    }
}
