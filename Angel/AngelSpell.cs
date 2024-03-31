using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
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
            SpellStuff.ChangeSpellLevel(AbilityRefs.AngelWrathOfTheRighteous.Reference, SpellListRefs.AngelMythicSpelllist.Reference, 8, 9);
            var cond = AbilityRefs.AngelWrathOfTheRighteous.Reference.Get().GetComponent<AbilityTargetsAround>()?.m_Condition;
            if (cond != null)
            {
                var newcond = ConditionsBuilder.New()
                    .Alignment(Kingmaker.Enums.AlignmentComponent.Evil, false, false)
                    .HasFact(FeatureRefs.SubtypeDemon.ToString())
                    .Build();
                cond.Conditions = newcond.Conditions;
            }
        }
    }
}
