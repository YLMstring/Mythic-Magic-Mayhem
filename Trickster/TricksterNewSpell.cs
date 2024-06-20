using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using MythicMagicMayhem.Demon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder.ContextEx;
using static Kingmaker.EntitySystem.Properties.BaseGetter.PropertyContextAccessor;
using MythicMagicMayhem.Aeon;
using MythicMagicMayhem.Azata;
using Kingmaker.UnitLogic.Abilities.Components;
using BlueprintCore.Utils;
using Kingmaker.Armies.Components;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.View;
using Kingmaker;
using System.Runtime.Remoting.Contexts;
using Kingmaker.UnitLogic;
using UnityEngine;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Components;
using HarmonyLib;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Groups;
using MythicMagicMayhem.Trickster;
using BlueprintCore.Conditions.Builder.ContextEx;

namespace MythicMagicMayhem.Trickster
{
    internal class TricksterNewSpell
    {
        private const string Metagamer = "TricksterNewSpell.Metagamer";
        private static readonly string MetagamerGuid = "{A494761A-E068-48B8-B6F4-34D107EB4862}";
        internal const string MetagamerDisplayName = "TricksterNewSpellMetagamer.Name";
        private const string MetagamerDescription = "TricksterNewSpellMetagamer.Description";

        private const string MetagamerAblity = "TricksterNewSpell.UseMetagamer";
        private static readonly string MetagamerAblityGuid = "{04CF702D-E286-4BCC-B11F-23D8444CB5B4}";

        private const string MetagamerBuff = "MetagamerBuff";
        public static readonly string MetagamerGuidBuff = "{47B70D83-13CD-4459-A6B3-0588D7DAD244}";

        private const string MetagamerAuraBuff = "MetagamerAuraBuff";
        private static readonly string MetagamerAuraGuidBuff = "{1938D8D6-F735-47E1-B9B4-4D56423AB76B}";

        private const string MetagamerAura = "MetagamerAura";
        private static readonly string MetagamerAuraGuid = "{25AFC47B-A276-4202-B343-095F5A2EECAD}";

        public static BlueprintAbility CreateMetagamer()
        {
            var icon = FeatureSelectionRefs.VivsectionistDiscoverySelection.Reference.Get().Icon;

            var Buff = BuffConfigurator.New(MetagamerBuff, MetagamerGuidBuff)
            .SetDisplayName(MetagamerDisplayName)
              .SetDescription(MetagamerDescription)
              .SetIcon(icon)
              .AddFacts([FeatureRefs.TricksterSneakyQuack.ToString()])
              .AddComponent<MetagamerComp>()
              .Configure();

            var comp = FeatureRefs.TricksterSneakyQuack.Reference.Get().GetComponent<ContextRankConfig>();
            comp.m_BaseValueType = ContextRankBaseValueType.BaseStat;
            comp.m_Stat = StatType.SneakAttack;

            var area = AbilityAreaEffectConfigurator.New(MetagamerAura, MetagamerAuraGuid)
                .SetAffectEnemies(false)
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(33.Feet())
                .AddAbilityAreaEffectBuff(buff: Buff, condition: ConditionsBuilder.New().IsAlly().Build())
                .Configure();

            var action = ActionsBuilder.New().ApplyBuff(BuffRefs.Exhausted.ToString(), ContextDuration.Fixed(10)).Build();
            var action2 = ActionsBuilder.New().Add<ContextActionOnAllAlly>(c => { c.Action = action; }).Build();

            var Aura = BuffConfigurator.New(MetagamerAuraBuff, MetagamerAuraGuidBuff)
              .SetDisplayName(MetagamerDisplayName)
              .SetDescription(MetagamerDescription)
              .SetIcon(icon)
              .AddAreaEffect(area)
              .AddBuffActions(deactivated: action2)
              .Configure();

            return AbilityConfigurator.NewSpell(MetagamerAblity, MetagamerAblityGuid, SpellSchool.Enchantment, canSpecialize: false)
              .SetDisplayName(MetagamerDisplayName)
              .SetDescription(MetagamerDescription)
              .SetIcon(icon)
              .SetRange(AbilityRange.Personal)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend)
              .SetLocalizedDuration(Duration.OneMinute)
              .AddAbilityEffectRunAction(ActionsBuilder.New()
                  .ApplyBuff(Aura, ContextDuration.Fixed(10), isFromSpell: true)
                  .Build())
              .Configure();
        }

        private const string HallMirrorsAbility = "NewSpell.UseHallMirrors";
        public static readonly string HallMirrorsAbilityGuid = "{18A1E6C0-0040-4EDB-8C18-6F764BD5E6F4}";

        private const string HallMirrorsBuff = "NewSpell.HallMirrorsBuff";
        public static readonly string HallMirrorsBuffGuid = "{A6201A04-84E3-436C-852F-6BED94B845E5}";

        internal const string DisplayName = "NewSpellHallMirrors.Name";
        private const string Description = "NewSpellHallMirrors.Description";
        public static BlueprintAbility HallMirrorsConfigure()
        {
            var icon = AbilityRefs.Banishment.Reference.Get().Icon;

            var mirror = ActionsBuilder.New()
                .ApplyBuff(BuffRefs.MirrorImageBuff.ToString(), ContextDuration.Fixed(1), isFromSpell: true)
                .Build();

            var buff = BuffConfigurator.New(HallMirrorsBuff, HallMirrorsBuffGuid)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .AddBuffActions(activated: mirror, newRound: mirror)
              .Configure();

            return AbilityConfigurator.NewSpell(HallMirrorsAbility, HallMirrorsAbilityGuid, SpellSchool.Illusion, canSpecialize: true)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Ally, radius: 30.Feet(), spreadSpeed: 20.Feet())
              .SetRange(AbilityRange.Personal)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend)
              .SetLocalizedDuration(Duration.OneMinute)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .ApplyBuff(buff, ContextDuration.Fixed(10), isFromSpell: true)
                  .Build())
              .Configure();
        }

        private const string RainHalberdiersAbility = "NewSpell.UseRainHalberdiers";
        public static readonly string RainHalberdiersAbilityGuid = "{F52AE91B-7C25-4F19-A40A-376AFE0337C4}";

        internal const string DisplayName2 = "NewSpellRainHalberdiers.Name";
        private const string Description2 = "NewSpellRainHalberdiers.Description";
        public static BlueprintAbility RainHalberdiersConfigure()
        {
            var icon = AbilityRefs.TricksterRainOfHalberds.Reference.Get().Icon;

            var action = ActionsBuilder.New()
                  .Add<ContextActionHalberdiersSummon>()
                  .Build();

            return AbilityConfigurator.NewSpell(RainHalberdiersAbility, RainHalberdiersAbilityGuid, SpellSchool.Conjuration, canSpecialize: true)
                .CopyFrom(AbilityRefs.TricksterRainOfHalberds,
                typeof(AbilityEffectRunAction),
                typeof(ContextRankConfig),
                typeof(AbilityTargetsAround),
                typeof(AbilitySpawnFx),
                typeof(AbilityDeliverDelay))
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AllowTargeting(true, true, true, true)
              .SetRange(AbilityRange.Custom)
              .SetCustomRange(40)
              //.SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend, Metamagic.Selective, Metamagic.Bolstered, Metamagic.Empower, Metamagic.Maximize)
              .SetSpellDescriptor(SpellDescriptor.Summoning)
              .SetLocalizedDuration(Duration.RoundPerLevel)
              .AddAbilityExecuteActionOnCast(action)
              .Configure();
        }

        private const string TrickDeveloperAbility = "NewSpell.UseTrickDeveloper";
        public static readonly string TrickDeveloperAbilityGuid = "{7858AA17-605A-4468-8680-CF932283C6BE}";

        internal const string DisplayName3 = "NewSpellTrickDeveloper.Name";
        private const string Description3 = "NewSpellTrickDeveloper.Description";
        public static BlueprintAbility TrickDeveloperConfigure()
        {
            var icon = AbilityRefs.HideousLaughter.Reference.Get().Icon;

            return AbilityConfigurator.NewSpell(TrickDeveloperAbility, TrickDeveloperAbilityGuid, SpellSchool.Abjuration, canSpecialize: false)
              .SetDisplayName(DisplayName3)
              .SetDescription(Description3)
              .SetIcon(icon)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal)
              .AddAbilityVariants(new() { AbilityRefs.AngelArmyOfHeaven.ToString(), AbilityRefs.AbsoluteDeath.ToString(), DemonNewSpell.AbyssalBreachAbilityGuid, AeonNewSpell.AbsoluteAuthorityAbility1Guid, AzataNewSpell.ElysiumChoirAbilityGuid })
              .Configure();
        }
    }

    internal class ContextActionHalberdiersSummon : ContextAction
    {
        public override string GetCaption()
        {
            return "Halberdiers Summon";
        }

        public override void RunAction()
        {
            var num = Context.Params.CasterLevel;
            while (true)
            {
                Summon(Context.MaybeCaster, Target.Point, Context.Params.CasterLevel * 6);
                num -= 4;
                if (num < 4) break;
            }
        }
        public static void Summon(UnitEntityData caster, Vector3 position, int second)
        {
            var unit = UnitRefs.CR11_GraveknightSummoned.Reference.Get();
            UnitEntityData maybeCaster = caster;
            Vector3 vector = position;
            vector = ObstacleAnalyzer.GetNearestNode(vector, null).position;
            UnitEntityView unitEntityView = unit.Prefab.Load(false, false);
            float radius = (unitEntityView != null) ? unitEntityView.Corpulence : 0.5f;
            FreePlaceSelector.PlaceSpawnPlaces(1, radius, vector);
            UnitEntityData unitEntityData = Game.Instance.EntityCreator.SpawnUnit(unit, vector, Quaternion.identity, maybeCaster.HoldingState, null);
            unitEntityData.Descriptor.SwitchFactions(caster.Faction, true);
            unitEntityData.GroupId = maybeCaster.GroupId;
            unitEntityData.UpdateGroup();
            unitEntityData.Descriptor.AddBuff(BlueprintTool.GetRef<BlueprintBuffReference>(DemonNewSpell.AbyssalBreachBuff2Guid), caster, new TimeSpan(0, 0, second), null);
        }
    }

    internal class ContextActionOnAllAlly : ContextAction
    {
        // Token: 0x0600D200 RID: 53760 RVA: 0x00369E20 File Offset: 0x00368020
        public override string GetCaption()
        {
            return "For each party member";
        }

        // Token: 0x0600D201 RID: 53761 RVA: 0x00369E28 File Offset: 0x00368028
        public override void RunAction()
        {
            UnitGroup group = Game.Instance.Player.MainCharacter.Value.Group;
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i] == Context.MaybeCaster) { continue; }
                using (base.Context.GetDataScope(group[i]))
                {
                    this.Action.Run();
                }
            }
        }
        // Token: 0x04008C44 RID: 35908
        public ActionList Action;
    }

    [HarmonyPatch(typeof(AddImmunityToPrecisionDamage))]
    [HarmonyPatch("OnEventAboutToTrigger")]
    [HarmonyPatch(new Type[] { typeof(RuleCalculateDamage) })]
    internal class MetagamerFix1
    {
        static bool Prefix(ref RuleCalculateDamage evt)
        {
            if (evt.Initiator.HasFact(Arcane))
            {
                return false;
            }
            return true;
        }
        private static BlueprintBuffReference Arcane = BlueprintTool.GetRef<BlueprintBuffReference>(TricksterNewSpell.MetagamerGuidBuff);
    }

    [HarmonyPatch(typeof(AddImmunityToPrecisionDamage))]
    [HarmonyPatch("OnEventAboutToTrigger")]
    [HarmonyPatch(new Type[] { typeof(RuleAttackRoll) })]
    internal class MetagamerFix2
    {
        static bool Prefix(ref RuleAttackRoll evt)
        {
            if (evt.Initiator.HasFact(Arcane))
            {
                return false;
            }
            return true;
        }
        private static BlueprintBuffReference Arcane = BlueprintTool.GetRef<BlueprintBuffReference>(TricksterNewSpell.MetagamerGuidBuff);
    }

    [HarmonyPatch(typeof(AddImmunityToCriticalHits))]
    [HarmonyPatch("OnEventAboutToTrigger")]
    [HarmonyPatch(new Type[] { typeof(RuleCalculateDamage) })]
    internal class MetagamerFix3
    {
        static bool Prefix(ref RuleCalculateDamage evt)
        {
            if (evt.Initiator.HasFact(Arcane))
            {
                return false;
            }
            return true;
        }
        private static BlueprintBuffReference Arcane = BlueprintTool.GetRef<BlueprintBuffReference>(TricksterNewSpell.MetagamerGuidBuff);
    }

    [HarmonyPatch(typeof(AddImmunityToCriticalHits))]
    [HarmonyPatch("OnEventAboutToTrigger")]
    [HarmonyPatch(new Type[] { typeof(RuleAttackRoll) })]
    internal class MetagamerFix4
    {
        static bool Prefix(ref RuleAttackRoll evt)
        {
            if (evt.Initiator.HasFact(Arcane))
            {
                return false;
            }
            return true;
        }
        private static BlueprintBuffReference Arcane = BlueprintTool.GetRef<BlueprintBuffReference>(TricksterNewSpell.MetagamerGuidBuff);
    }

    [HarmonyPatch(typeof(AddImmunityToCriticalHits))]
    [HarmonyPatch("OnEventAboutToTrigger")]
    [HarmonyPatch(new Type[] { typeof(RuleDealStatDamage) })]
    internal class MetagamerFix5
    {
        static bool Prefix(ref RuleDealStatDamage evt)
        {
            if (evt.Initiator.HasFact(Arcane))
            {
                return false;
            }
            return true;
        }
        private static BlueprintBuffReference Arcane = BlueprintTool.GetRef<BlueprintBuffReference>(TricksterNewSpell.MetagamerGuidBuff);
    }

    internal class MetagamerComp : UnitBuffComponentDelegate
    {
        // Token: 0x0600C307 RID: 49927 RVA: 0x0032F900 File Offset: 0x0032DB00
        public override void OnTurnOn()
        {
            int num = Buff.Context.MaybeCaster.Stats.SneakAttack - Owner.Stats.SneakAttack;
            if (num > 0)
            {
                base.Owner.Stats.SneakAttack.AddModifierUnique(num, base.Runtime);
            }
        }

        // Token: 0x0600C308 RID: 49928 RVA: 0x0032F9E4 File Offset: 0x0032DBE4
        public override void OnTurnOff()
        {
            base.OnTurnOff();
            base.Owner.Stats.SneakAttack.RemoveModifiersFrom(base.Runtime);
        }
    }
}

    

    

