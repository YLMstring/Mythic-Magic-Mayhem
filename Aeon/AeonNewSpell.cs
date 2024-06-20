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
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Mechanics.Components;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.UnitLogic;
using Kingmaker.Designers;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Buffs.Components;

namespace MythicMagicMayhem.Aeon
{
    internal class AeonNewSpell
    {
        private const string AbsoluteAuthorityAbility1 = "NewSpell.UseAbsoluteAuthority1";
        public static readonly string AbsoluteAuthorityAbility1Guid = "{E87E92DF-09A2-4B42-8082-8C7420D5CF67}";

        private const string AbsoluteAuthorityAbility2 = "NewSpell.UseAbsoluteAuthority2";
        public static readonly string AbsoluteAuthorityAbility2Guid = "{B1840175-0400-47D9-B2B5-F362D44B7075}";

        internal const string DisplayName = "NewSpellAbsoluteAuthority.Name";
        private const string Description = "NewSpellAbsoluteAuthority.Description";
        public static BlueprintAbility AbsoluteAuthorityConfigure()
        {
            var icon = AbilityRefs.CastigateMass.Reference.Get().Icon;

            var ability = AbilityConfigurator.NewSpell(
                AbsoluteAuthorityAbility2, AbsoluteAuthorityAbility2Guid, SpellSchool.Enchantment, canSpecialize: false)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .SetRange(AbilityRange.Personal)
              .SetType(AbilityType.Spell)
              .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Enemy, radius: 30.Feet(), spreadSpeed: 20.Feet())
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .CastSpell(AbilityRefs.EdictOfRetaliation.ToString(), overrideSpellbook: true, overrideSpellLevel: 10, markAsChild: true)
                  .Build())
              .Configure();

            return AbilityConfigurator.NewSpell(
                AbsoluteAuthorityAbility1, AbsoluteAuthorityAbility1Guid, SpellSchool.Enchantment, canSpecialize: false)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .SetRange(AbilityRange.Personal)
              .SetType(AbilityType.Spell)
              .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Enemy, radius: 30.Feet(), spreadSpeed: 20.Feet())
              .AddComponent(AbilityRefs.EdictOfNonresistance.Reference.Get().GetComponent<AbilityEffectRunAction>())
              .AddComponent(AbilityRefs.EdictOfNonresistance.Reference.Get().GetComponent<ContextRankConfig>())
              .AddAbilityExecuteActionOnCast(
                actions: ActionsBuilder.New()
                  .CastSpell(ability, overrideSpellbook: true, overrideSpellLevel: 10, markAsChild: true)
                  .Build())
              .Configure();
        }

        private const string TotalNullificationAbility = "NewSpell.UseTotalNullification";
        public static readonly string TotalNullificationAbilityGuid = "{51421F3C-A073-40F6-BF66-108944250DDA}";

        private const string TotalNullificationBuff = "NewSpell.TotalNullificationBuff";
        private static readonly string TotalNullificationBuffGuid = "{3A9E6E9C-7C6B-4169-B787-6AF4C5BD6536}";

        private const string TotalNullificationBuff1 = "NewSpell.TotalNullificationBuff1";
        private static readonly string TotalNullificationBuff1Guid = "{3A95893D-BE61-46E7-9B9D-D5EFC7F63279}";

        private const string TotalNullificationBuff2 = "NewSpell.TotalNullificationBuff2";
        private static readonly string TotalNullificationBuff2Guid = "{797A9AF2-123B-4F40-8F01-A8785B4C2E2B}";

        private const string TotalNullificationBuff3 = "NewSpell.TotalNullificationBuff3";
        private static readonly string TotalNullificationBuff3Guid = "{6910608E-4F6F-4E54-843C-751640589646}";

        private const string TotalNullificationBuff4 = "NewSpell.TotalNullificationBuff4";
        private static readonly string TotalNullificationBuff4Guid = "{93163DD3-6954-407E-8221-B30E40AE71ED}";

        private const string TotalNullificationBuff5 = "NewSpell.TotalNullificationBuff5";
        private static readonly string TotalNullificationBuff5Guid = "{29A10405-79DB-42AA-B94E-DAEB049569B4}";

        private const string TotalNullificationBuff6 = "NewSpell.TotalNullificationBuff6";
        private static readonly string TotalNullificationBuff6Guid = "{A17C4E27-32DB-4F98-9D8C-93A6084B9097}";

        private const string TotalNullificationBuff7 = "NewSpell.TotalNullificationBuff7";
        private static readonly string TotalNullificationBuff7Guid = "{9DC9CDD3-8A03-413C-A5DC-944BE075C225}";

        private const string TotalNullificationBuff8 = "NewSpell.TotalNullificationBuff8";
        private static readonly string TotalNullificationBuff8Guid = "{E984B7B7-F01A-4072-B2D5-11BCE58D0A88}";

        private const string TotalNullificationBuff9 = "NewSpell.TotalNullificationBuff9";
        private static readonly string TotalNullificationBuff9Guid = "{9074C403-2ACF-43E1-A9C0-B70E4A564455}";

        private const string TotalNullificationBuff10 = "NewSpell.TotalNullificationBuff10";
        private static readonly string TotalNullificationBuff10Guid = "{38E0D2F6-39BF-4E51-8D8F-BCF309ACDD85}";

        internal const string DisplayName2 = "NewSpellTotalNullification.Name";
        private const string Description2 = "NewSpellTotalNullification.Description";

        public static BlueprintAbility TotalNullificationConfigure()
        {
            var icon = AbilityRefs.HolyWord.Reference.Get().Icon;

            var buff = BuffConfigurator.New(TotalNullificationBuff, TotalNullificationBuffGuid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.Divine; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff1 = BuffConfigurator.New(TotalNullificationBuff1, TotalNullificationBuff1Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddDamageResistancePhysical(usePool: true, pool: ContextValues.Rank(), value: ContextValues.Rank())
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff2 = BuffConfigurator.New(TotalNullificationBuff2, TotalNullificationBuff2Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.Fire; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff3 = BuffConfigurator.New(TotalNullificationBuff3, TotalNullificationBuff3Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.Cold; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff4 = BuffConfigurator.New(TotalNullificationBuff4, TotalNullificationBuff4Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.Electricity; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff5 = BuffConfigurator.New(TotalNullificationBuff5, TotalNullificationBuff5Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.Acid; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff6 = BuffConfigurator.New(TotalNullificationBuff6, TotalNullificationBuff6Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.Holy; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff7 = BuffConfigurator.New(TotalNullificationBuff7, TotalNullificationBuff7Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.Unholy; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff8 = BuffConfigurator.New(TotalNullificationBuff8, TotalNullificationBuff8Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.Sonic; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff9 = BuffConfigurator.New(TotalNullificationBuff9, TotalNullificationBuff9Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.PositiveEnergy; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
              .Configure();

            var buff10 = BuffConfigurator.New(TotalNullificationBuff10, TotalNullificationBuff10Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<ProtectionFromEnergy>(c => { c.UsePool = true; c.Pool = ContextValues.Rank(); c.Type = DamageEnergyType.NegativeEnergy; })
              .AddContextRankConfig(ContextRankConfigs.CasterLevel().WithMultiplyByModifierProgression(10))
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
                  .ApplyBuff(buff1, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff2, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff3, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff4, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff5, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff6, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff7, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff8, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff9, ContextDuration.Fixed(100), isFromSpell: true)
                  .ApplyBuff(buff10, ContextDuration.Fixed(100), isFromSpell: true)
                  .Build())
              .Configure();
        }

        private const string TemporalInterdictionAbility = "NewSpell.UseTemporalInterdiction";
        public static readonly string TemporalInterdictionAbilityGuid = "{665D3138-3130-40DD-8E6C-D6253F82CE6D}";

        private const string TemporalInterdictionBuff = "NewSpell.TemporalInterdictionBuff";
        public static readonly string TemporalInterdictionBuffGuid = "{10B7F9F7-62B5-45C3-8C99-6C3F3922488A}";

        internal const string DisplayName3 = "NewSpellTemporalInterdiction.Name";
        private const string Description3 = "NewSpellTemporalInterdiction.Description";

        public static BlueprintAbility TemporalInterdictionConfigure()
        {
            var icon = AbilityRefs.TimeStop.Reference.Get().Icon;

            var buff = BuffConfigurator.New(TemporalInterdictionBuff, TemporalInterdictionBuffGuid)
              .SetDisplayName(DisplayName3)
              .SetDescription(Description3)
              .SetIcon(icon)
              .AddComponent<SynthesisComponent>()
              .SetStacking(Kingmaker.UnitLogic.Buffs.Blueprints.StackingType.Rank)
              .SetRanks(3)
              .Configure();

            var fx = AbilityRefs.OverwhelmingPresence.Reference.Get().GetComponent<AbilitySpawnFx>();

            return AbilityConfigurator.NewSpell(
                TemporalInterdictionAbility, TemporalInterdictionAbilityGuid, SpellSchool.Abjuration, canSpecialize: false)
              .SetDisplayName(DisplayName3)
              .SetDescription(Description3)
              .SetIcon(icon)
              .SetLocalizedDuration(Duration.OneRound)
              .AddComponent(fx)
              .SetRange(AbilityRange.Personal)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .ApplyBuff(buff, ContextDuration.Fixed(1), isFromSpell: true)
                  .ApplyBuff(buff, ContextDuration.Fixed(1), isFromSpell: true)
                  .ApplyBuff(buff, ContextDuration.Fixed(1), isFromSpell: true)
                  .Build())
              .Configure();
        }
    }

    [HarmonyPatch(typeof(AbilityData), "get_ActionType")]
    internal class SynthesisFix
    {
        static void Postfix(ref UnitCommand.CommandType __result, ref AbilityData __instance)
        {
            try
            {
                if (__instance.Caster == null || __instance.Spellbook == null)
                {
                    return;
                }
                if (__instance.Caster.HasFact(Arcane))
                {
                    __result = UnitCommand.CommandType.Free;
                }   
            }
            catch (Exception ex) { Logger.Error("Failed to synthesis.", ex); }
        }
        private static readonly LogWrapper Logger = LogWrapper.Get("MMMmod");
        private static BlueprintBuffReference Arcane = BlueprintTool.GetRef<BlueprintBuffReference>(AeonNewSpell.TemporalInterdictionBuffGuid);
    }
    internal class SynthesisComponent : UnitBuffComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber, IInitiatorRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell>
    {
        void IRulebookHandler<RuleCastSpell>.OnEventAboutToTrigger(RuleCastSpell evt)
        {

        }

        void IRulebookHandler<RuleCastSpell>.OnEventDidTrigger(RuleCastSpell evt)
        {
            try
            {
                if (evt.Spell?.Spellbook != null)
                {
                    if (Buff.Rank > 1)
                    {
                        Buff.Rank--;
                    }
                    else { Buff.Remove(); }
                }
            }
            catch (Exception ex) { Logger.Error("Failed to synthesis.", ex); }
        }
        private static readonly LogWrapper Logger = LogWrapper.Get("MMMmod");
    }
}
