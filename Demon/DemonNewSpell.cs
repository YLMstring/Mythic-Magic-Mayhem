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

namespace MythicMagicMayhem.Demon
{
    internal class DemonNewSpell
    {
        private const string BlindFuryAbility = "NewSpell.UseBlindFury";
        public static readonly string BlindFuryAbilityGuid = "{92C2E197-903E-4938-BF82-0C4724F01DAE}";

        private const string BlindFuryBuff = "NewSpell.BlindFuryBuff";
        public static readonly string BlindFuryBuffGuid = "{709F33B3-F95B-4BCF-8FC9-B503D8925A44}";

        internal const string DisplayName = "NewSpellBlindFury.Name";
        private const string Description = "NewSpellBlindFury.Description";

        public static void Configure()
        {
            var icon = AbilityRefs.BlindingWrathAbility.Reference.Get().Icon;
            var buff = BuffConfigurator.New(BlindFuryBuff, BlindFuryBuffGuid)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .AddSpellDescriptorComponent(SpellDescriptor.MindAffecting | SpellDescriptor.Emotion)
              .AddCondition(Kingmaker.UnitLogic.UnitCondition.Blindness)
              .AddFacts(new() { BuffRefs.RageSpellBuff.ToString() })
              .Configure();

            AbilityConfigurator.NewSpell(BlindFuryAbility, BlindFuryAbilityGuid, SpellSchool.Enchantment, canSpecialize: false)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Any, radius: 20.Feet(), spreadSpeed: 30.Feet())
              .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
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

        [HarmonyPatch(typeof(RerollConcealment), nameof(UnitPartConcealment.Calculate))]
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
}
