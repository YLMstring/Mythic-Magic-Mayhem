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
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Utils.Types;
using MythicMagicMayhem.Components;

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
            var icon = AbilityRefs.CastigateMass.Reference.Get().Icon;
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

        private const string TotalNullificationAbility = "NewSpell.UseTotalNullification";
        public static readonly string TotalNullificationAbilityGuid = "{51421F3C-A073-40F6-BF66-108944250DDA}";

        private const string TotalNullificationBuff = "NewSpell.TotalNullificationBuff";
        private static readonly string TotalNullificationBuffGuid = "{3A9E6E9C-7C6B-4169-B787-6AF4C5BD6536}";

        internal const string DisplayName2 = "NewSpellTotalNullification.Name";
        private const string Description2 = "NewSpellTotalNullification.Description";

        public static BlueprintAbility TotalNullificationConfigure()
        {
            var icon = AbilityRefs.HolyWord.Reference.Get().Icon;

            var buff = BuffConfigurator.New(TotalNullificationBuff, TotalNullificationBuffGuid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddComponent<ProtectionFromEverything>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(20))
              .Configure();

            var fx = AbilityRefs.CircleOfDeath.Reference.Get().GetComponent<AbilitySpawnFx>();

            return AbilityConfigurator.NewSpell(
                TotalNullificationAbility, TotalNullificationAbilityGuid, SpellSchool.Abjuration, canSpecialize: false)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .SetLocalizedDuration(Duration.TenMinutes)
              .AddComponent(fx)
              .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
              .SetRange(AbilityRange.Personal)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Ally, radius: 20.Feet(), spreadSpeed: 20.Feet())
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .ApplyBuff(buff, ContextDuration.Fixed(100), isFromSpell: true)
                  .Build())
              .Configure();
        }
    }
}
