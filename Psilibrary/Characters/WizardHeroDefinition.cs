using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Psilibrary;

namespace Psilibrary.CharacterComponents
{
    public class WizardHeroDefinition : HeroDefinition
    {
        public WizardHeroDefinition(Game game) 
            : base(game)
        {
            name = "Wizard";
            
            isSpellcaster = true;

            attributeBonuses[CharacterAttributes.Intellect] = 3;
        }
    }
}
