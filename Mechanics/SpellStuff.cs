using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Epic.OnlineServices.Logging;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Mechanics
{
    internal class SpellStuff
    {
        public static void ChangeSpellLevel(BlueprintAbility spell, BlueprintSpellList list, int newlevel, int oldlevel)
        {
            if (spell == null || list == null) return;
            foreach (var levelentry in list.SpellsByLevel)
            {
                if (levelentry.SpellLevel == oldlevel)
                {
                    levelentry.m_Spells.Remove(spell.ToReference<BlueprintAbilityReference>());
                }
                if (levelentry.SpellLevel == newlevel)
                {
                    levelentry.m_Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            if (newlevel > 10) return;
            var Lists = spell.GetComponents<SpellListComponent>();
            if (Lists == null) return;
            foreach (var List in Lists)
            {
                if (List.SpellList == list)
                {
                    List.SpellLevel = newlevel;
                }
            }
        }

        public static void AddSpellLevel(BlueprintAbility spell, BlueprintSpellList list, int newlevel)
        {
            if (spell == null || list == null) return;
            foreach (var levelentry in list.SpellsByLevel)
            {
                if (levelentry.SpellLevel == newlevel)
                {
                    levelentry.m_Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            AbilityConfigurator.For(spell)
                .AddSpellListComponent(newlevel, list)
                .Configure();
        }
    }
}
