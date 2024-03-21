using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using BlueprintCore.Actions.Builder.ContextEx;

namespace MythicMagicMayhem.Azata
{
    internal class AzataNewSpell
    {
        private const string GroupHugAbility1 = "NewSpell.UseGroupHug1";
        public static readonly string GroupHugAbility1Guid = "{D12E2DFB-D841-4BA0-893B-2BE19C535DDC}";

        internal const string DisplayName = "NewSpellGroupHug.Name";
        private const string Description = "NewSpellGroupHug.Description";
        public static void GroupHugConfigure()
        {
            var icon = AbilityRefs.FriendlyHug.Reference.Get().Icon;
            var fx = AbilityRefs.FriendlyHug.Reference.Get().GetComponent<AbilitySpawnFx>();

            AbilityConfigurator.NewSpell(
                GroupHugAbility1, GroupHugAbility1Guid, SpellSchool.Abjuration, canSpecialize: false)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .SetRange(AbilityRange.Personal)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Quicken)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Ally, radius: 15.Feet(), spreadSpeed: 20.Feet())
              .AddComponent(fx)
              .AddToSpellLists(level: 8, SpellList.AzataMythic)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .CastSpell(AbilityRefs.FriendlyHug.ToString())
                  .Build())
              .AddCraftInfoComponent(
                aOEType: CraftAOE.AOE,
                savingThrow: CraftSavingThrow.None,
                spellType: CraftSpellType.Buff)
              .Configure();
        }
    }
}
