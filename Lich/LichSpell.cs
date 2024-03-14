using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using MythicMagicMayhem.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Lich
{
    internal class LichSpell
    {
        public static void Patch()
        {
            SpellStuff.AddSpellLevel(AbilityRefs.AnimateDeadLesser.Reference, SpellListRefs.LichWizardSpelllist.Reference, 3);
            //already in?
            //SpellStuff.AddSpellLevel(AbilityRefs.CreateUndeadGreaterBase.Reference, SpellListRefs.LichWizardSpelllist.Reference, 8);
            //"GloomblindBoltsAbility": "e28f4633-c0a2-425d-8895-adf20cb22f8f",
            //var bolt = BlueprintTool.GetRef<BlueprintAbilityReference>("e28f4633-c0a2-425d-8895-adf20cb22f8f");
            //SpellStuff.AddSpellLevel(bolt, SpellListRefs.LichWizardSpelllist.Reference, 3);
        }
    }
}
