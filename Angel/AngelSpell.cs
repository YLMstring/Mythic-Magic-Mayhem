using BlueprintCore.Blueprints.References;
using MythicMagicMayhem.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Angel
{
    internal class AngelSpell
    {
        public static void Patch()
        {
            SpellStuff.ChangeSpellLevel(AbilityRefs.AngelBoltOfJusticeAbility.Reference, SpellListRefs.AngelMythicSpelllist.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.AngelStormOfJusticeAbility.Reference, SpellListRefs.AngelMythicSpelllist.Reference, 9, 8);
        }
    }
}
