using Psilibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Psilibrary.CharacterComponents
{
    public class RogueHeroDefinition : HeroDefinition
    {
        public RogueHeroDefinition(Game game)
            : base(game)
        {
            name = "Rogue";

            attributeBonuses[CharacterAttributes.Agility] = 3;
        }
    }
}
