using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder.ContextEx;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;
using Kingmaker.Blueprints;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic;
using Kingmaker;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Enums;
using Kingmaker.EntitySystem.Entities;
using BlueprintCore.Actions.Builder.AVEx;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using MythicMagicMayhem.Trickster;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.RuleSystem.Rules.Abilities;
using UnityEngine.Serialization;
using UnityEngine;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace MythicMagicMayhem.Demon
{
    internal class DemonNewSpell
    {
        private const string DemonicFormVAbility = "NewSpell.UseDemonicFormV";
        public static readonly string DemonicFormVAbilityGuid = "{2F0B7663-1BF3-49DC-806A-76560897704D}";

        private const string DemonicFormVBuff = "NewSpell.DemonicFormVBuff";
        public static readonly string DemonicFormVBuffGuid = "{49C018D7-1AEE-4B2A-9102-B45F45D9D34F}";

        internal const string DisplayName0 = "NewSpellDemonicFormV.Name";
        private const string Description0 = "NewSpellDemonicFormV.Description";
        public static BlueprintAbility DemonicFormVConfigure()
        {
            var icon = FeatureSelectionRefs.DemonLordAspectSelection.Reference.Get().Icon;

            var monster = UnitRefs.CR23_AreshkagalRetriever.Reference.Get();
            var balor = BuffRefs.DemonicFormIVBalorBuff.Reference.Get().GetComponent<Polymorph>();

            var buff = BuffConfigurator.New(DemonicFormVBuff, DemonicFormVBuffGuid)
                .CopyFrom(
                BuffRefs.DemonicFormIVBalorBuff,
                typeof(ReplaceSourceBone),
                typeof(ReplaceCastSource))
              .SetDisplayName(DisplayName0)
              .SetDescription(Description0)
              .SetIcon(icon)
              .AddSpellDescriptorComponent(SpellDescriptor.Polymorph)
              .AddReplaceAsksList(monster.Visual.Barks)
              .AddMechanicsFeature(Kingmaker.UnitLogic.FactLogic.AddMechanicsFeature.MechanicsFeatureType.NaturalSpell)
              .AddPolymorph([ItemWeaponRefs.ClawHuge3d6.ToString(), ItemWeaponRefs.ClawHuge3d6.ToString(), ItemWeaponRefs.BiteHuge2d8.ToString()], false, 10, 8,
                balor.m_EnterTransition, balor.m_ExitTransition, [AbilityRefs.TurnBackAbilityStandart.ToString(), FeatureRefs.DemonicFormWeaponEnchantFact.ToString(), FeatureRefs.ShifterGrabTiger.ToString(), AbilityRefs.Disintegrate.ToString()], 
                false, ItemWeaponRefs.ClawHuge3d6.ToString(), null, BlueprintCore.Blueprints.CustomConfigurators.ComponentMerge.Fail, 10, ItemWeaponRefs.ClawHuge3d6.ToString(),
                monster.PortraitSafe, monster.Prefab, monster.Prefab, null, null, null, true, Size.Huge, SpecialDollType.None, 12, balor.m_TransitionExternal, true)
              .AddAutoMetamagic([AbilityRefs.Disintegrate.ToString()], AutoMetamagic.AllowedType.Any, metamagic: Metamagic.Quicken)
              .AddComponent<ReplaceCasterLevelForAbility>(c => { c.spell = AbilityRefs.Disintegrate.Reference; })
              .Configure();

            var mirror = ActionsBuilder.New()
                .RemoveBuffsByDescriptor(SpellDescriptor.Polymorph, false)
                .ApplyBuff(buff, ContextDuration.Variable(ContextValues.Rank(), Kingmaker.UnitLogic.Mechanics.DurationRate.Minutes), isFromSpell: true)
                .Build();

            return AbilityConfigurator.NewSpell(DemonicFormVAbility, DemonicFormVAbilityGuid, SpellSchool.Transmutation, canSpecialize: false)
              .SetDisplayName(DisplayName0)
              .SetDescription(Description0)
              .SetIcon(icon)
              .SetRange(AbilityRange.Personal)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Extend)
              .SetLocalizedDuration(Duration.MinutePerLevel)
              .AddContextRankConfig(ContextRankConfigs.CasterLevel())
              .AddAbilityEffectRunAction(mirror)
              .AddSpellDescriptorComponent(SpellDescriptor.Polymorph)
              .Configure();
        }

        private const string BlindFuryAbility = "NewSpell.UseBlindFury";
        public static readonly string BlindFuryAbilityGuid = "{92C2E197-903E-4938-BF82-0C4724F01DAE}";

        private const string BlindFuryBuff = "NewSpell.BlindFuryBuff";
        public static readonly string BlindFuryBuffGuid = "{709F33B3-F95B-4BCF-8FC9-B503D8925A44}";

        internal const string DisplayName = "NewSpellBlindFury.Name";
        private const string Description = "NewSpellBlindFury.Description";
        public static BlueprintAbility BlindFuryConfigure()
        {
            var icon = AbilityRefs.BansheeBlast.Reference.Get().Icon;
            var buff = BuffConfigurator.New(BlindFuryBuff, BlindFuryBuffGuid)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .AddSpellDescriptorComponent(SpellDescriptor.MindAffecting | SpellDescriptor.Emotion)
              .AddCondition(Kingmaker.UnitLogic.UnitCondition.Blindness)
              .AddFacts(new() { BuffRefs.RageSpellBuff.ToString() })
              .Configure();

            return AbilityConfigurator.NewSpell(BlindFuryAbility, BlindFuryAbilityGuid, SpellSchool.Enchantment, canSpecialize: false)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Any, radius: 20.Feet(), spreadSpeed: 30.Feet())
              .AllowTargeting(true, true, true, true)
              //.SetAnimation(CastAnimationStyle.Directional) 
              .SetRange(AbilityRange.Custom)
              .SetCustomRange(60)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Persistent, Metamagic.Extend, Metamagic.Selective)
              .SetSpellDescriptor(SpellDescriptor.MindAffecting | SpellDescriptor.Emotion)
              .SetSpellResistance()
              .SetLocalizedDuration(Duration.RoundPerLevel)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .ConditionalSaved(failed: ActionsBuilder.New()
                        .ApplyBuff(buff, ContextDuration.Variable(ContextValues.Rank()))
                        .Build())
                  .Build(), savingThrowType: SavingThrowType.Will)
              .Configure();
        }

        private const string AbyssalBreachAbility = "NewSpell.UseAbyssalBreach";
        public static readonly string AbyssalBreachAbilityGuid = "{0680AD26-35F1-4875-9AA6-C792A4081193}";

        private const string AbyssalBreachBuff = "NewSpell.AbyssalBreachBuff";
        public static readonly string AbyssalBreachBuffGuid = "{16CB55C7-836A-4E7C-BED7-C2DC8F8063A3}";

        private const string AbyssalBreachBuff2 = "NewSpell.AbyssalBreachBuff2";
        public static readonly string AbyssalBreachBuff2Guid = "{0A6F618A-8898-40D6-AA47-7E902FCD4470}";

        internal const string DisplayName2 = "NewSpellAbyssalBreach.Name";
        private const string Description2 = "NewSpellAbyssalBreach.Description";
        public static BlueprintAbility AbyssalBreachConfigure()
        {
            var icon = AbilityRefs.DemonTeleport.Reference.Get().Icon;

            var end = ActionsBuilder.New()
                .Add<ContextActionBreachEnd>()
                .Build();

            var summon = ActionsBuilder.New()
                .Add<ContextActionBreachSummon>()
                .Build();

            var buff = BuffConfigurator.New(AbyssalBreachBuff, AbyssalBreachBuffGuid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddBuffActions(deactivated: end, newRound: summon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.StayOnDeath)
              .Configure();

            BuffConfigurator.New(AbyssalBreachBuff2, AbyssalBreachBuff2Guid)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
              .AddComponent<MMMDestroyOnDeactivate>()
              .Configure();

            return AbilityConfigurator.NewSpell(AbyssalBreachAbility, AbyssalBreachAbilityGuid, SpellSchool.Conjuration, canSpecialize: false)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AllowTargeting(true, false, false, false)
              .SetRange(AbilityRange.Close)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend)
              .SetSpellDescriptor(SpellDescriptor.Summoning)
              .SetLocalizedDuration(Duration.OneMinute)
              .AddAbilityCasterHasNoFacts(new() { buff })
              .SetIsFullRoundAction(true)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .ApplyBuff(buff, ContextDuration.Fixed(10), toCaster: true)
                  .Add<ContextActionBreachStart>()
                  .Build())
              .Configure();
        }
    }
    internal class BlindFuryFix
    {
        [HarmonyPatch(typeof(RerollConcealment), nameof(RerollConcealment.ShouldReroll))]
        private static class BlindFuryFixReRoll
        {
            private static void Postfix(ref bool __result, ref RuleConcealmentCheck evt)
            {
                if (evt.Initiator.HasFact(Buff))
                {
                    __result = false;
                }
            }
        }

        [HarmonyPatch(typeof(UnitPartConcealment), nameof(UnitPartConcealment.Calculate))]
        private static class BlindFuryFixCalc
        {
            private static void Postfix(ref Concealment __result, ref UnitEntityData initiator)
            {
                if (initiator.HasFact(Buff) && initiator.State.HasCondition(UnitCondition.Blindness))
                {
                    __result = Concealment.Total;
                }
            }
        }

        private static BlueprintBuffReference Buff = BlueprintTool.GetRef<BlueprintBuffReference>(DemonNewSpell.BlindFuryBuffGuid);
    }

    public class MMMDestroyOnDeactivate : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleDealDamage>, IRulebookHandler<RuleDealDamage>, ISubscriber, ITargetRulebookSubscriber, IUnitSubscriber
    {
        // Token: 0x0600C378 RID: 50040 RVA: 0x00330564 File Offset: 0x0032E764
        public override void OnActivate()
        {

        }

        // Token: 0x0600C379 RID: 50041 RVA: 0x003305D0 File Offset: 0x0032E7D0
        public override void OnDeactivate()
        {
            Owner.IsInGame = false;
        }

        void IRulebookHandler<RuleDealDamage>.OnEventAboutToTrigger(RuleDealDamage evt)
        {
            if (evt.Reason.Ability?.Blueprint == BlueprintTool.GetRef<BlueprintAbilityReference>(TricksterNewSpell.RainHalberdiersAbilityGuid).Get())
            {
                evt.Remove((BaseDamage _) => true);
            }
        }

        void IRulebookHandler<RuleDealDamage>.OnEventDidTrigger(RuleDealDamage evt)
        {
            
        }
    }

    public class ReplaceCasterLevelForAbility : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        // Token: 0x170025AA RID: 9642
        // (get) Token: 0x0600E6AD RID: 59053 RVA: 0x003B7517 File Offset: 0x003B5717
        public BlueprintAbility spell;
        // Token: 0x0600E6AE RID: 59054 RVA: 0x003B752A File Offset: 0x003B572A
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            var num = Buff?.MaybeContext?.Params?.CasterLevel;
            if (evt.Spell == this.spell && evt.Spellbook == null && num > 0)
            {
                evt.ReplaceCasterLevel = num;
            }
        }

        // Token: 0x0600E6AF RID: 59055 RVA: 0x003B754C File Offset: 0x003B574C
        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
    }
}
