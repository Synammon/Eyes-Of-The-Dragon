using Psilibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Psilibrary.CharacterComponents
{
    public class PriestHeroDefinition : HeroDefinition
    {
        public PriestHeroDefinition(Game game)
            : base(game)
        {
            name = "Priest";

            isSpellcaster = true;

            attributeBonuses[CharacterAttributes.Intellect] = 2;
        }
    }
}
