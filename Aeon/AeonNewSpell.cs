using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using BlueprintCore.Actions.Builder.ContextEx;

namespace MythicMagicMayhem.Aeon
{
    internal class AeonNewSpell
    {
        private const string AbsoluteAuthorityAbility1 = "NewSpell.UseAbsoluteAuthority1";
        public static readonly string AbsoluteAuthorityAbility1Guid = "{E87E92DF-09A2-4B42-8082-8C7420D5CF67}";

        internal const string DisplayName = "NewSpellAbsoluteAuthority.Name";
        private const string Description = "NewSpellAbsoluteAuthority.Description";
        public static BlueprintAbility AbsoluteAuthorityConfigure()
        {
            var icon = AbilityRefs.OverwhelmingPresence.Reference.Get().Icon;
            var fx = AbilityRefs.OverwhelmingPresence.Reference.Get().GetComponent<AbilitySpawnFx>();

            return AbilityConfigurator.NewSpell(
                AbsoluteAuthorityAbility1, AbsoluteAuthorityAbility1Guid, SpellSchool.Abjuration, canSpecialize: false)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .SetRange(AbilityRange.Personal)
              .SetType(AbilityType.Spell)
              .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Enemy, radius: 30.Feet(), spreadSpeed: 20.Feet())
              .AddComponent(fx)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .CastSpell(AbilityRefs.EdictOfRetaliation.ToString(), overrideSpellbook: true, overrideSpellLevel: 10)
                  .CastSpell(AbilityRefs.EdictOfNonresistance.ToString(), overrideSpellbook: true, overrideSpellLevel: 10)
                  .Build())
              .Configure();
        }
    }
}
