using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Lich
{
    internal class LichFeature
    {
        private static readonly string FeatName = "FeatUndeadMount1";
        public static readonly string FeatGuid = "{E2D99CFC-DDDF-4F10-AA04-E518E92C0A14}";

        private static readonly string FeatShowName = "FeatShowUndeadMount1";
        public static readonly string FeatShowGuid = "{5C6DB2C4-5950-465D-8936-4DC7569B2B8E}";

        private static readonly string DisplayName = "FeatUndeadMount1.Name";
        private static readonly string Description = "FeatUndeadMount1.Description";

        public static void UndeadMount1Configure()
        {
            var icon = FeatureRefs.UndeadMountFeature.Reference.Get().Icon;

            var feat = FeatureConfigurator.New(FeatName, FeatGuid)
                    .SetDisplayName(DisplayName)
                    .SetDescription(Description)
                    .SetIcon(icon)
                    .AddContextStatBonus(StatType.Charisma, ContextValues.Rank(), ModifierDescriptor.Profane)
                    .AddContextRankConfig(ContextRankConfigs.MythicLevel(true))
                    .Configure();

            var feat2 = FeatureConfigurator.New(FeatShowName, FeatShowGuid)
                    .SetDisplayName(DisplayName)
                    .SetDescription(Description)
                    .SetIcon(icon)
                    .AddFeatureToPet(feat, PetType.AnimalCompanion)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

            FeatureConfigurator.For(FeatureRefs.UndeadMountFeature.Reference)
                .AddFacts(new() { feat2 })
                .Configure();
        }

        private static readonly string Feat2Name = "FeatUndeadMount2";
        public static readonly string Feat2Guid = "{6C5E8925-103B-442C-B4C6-916056109AEE}";

        private static readonly string DisplayName2 = "FeatUndeadMount2.Name";
        private static readonly string Description2 = "FeatUndeadMount2.Description";

        public static void UndeadMount2Configure()
        {
            var icon = FeatureRefs.UndeadMountFeature.Reference.Get().Icon;

            FeatureConfigurator.New(Feat2Name, Feat2Guid, Kingmaker.Blueprints.Classes.FeatureGroup.MythicAbility)
                    .SetDisplayName(DisplayName2)
                    .SetDescription(Description2)
                    .SetIcon(icon)
                    .AddFeatureToPet(FeatureRefs.UndeadType.ToString(), PetType.AnimalCompanion)
                    .AddPrerequisitePlayerHasFeature(FeatureSelectionRefs.LichUniqueAbilitiesSelection.ToString())
                    .SetHideNotAvailibleInUI(true)
                    .Configure();
        }

        private static readonly string Feat3Name = "FeatUnholyFortitude";
        public static readonly string Feat3Guid = "{73212779-14FC-4983-A817-4FF1643EBCB9}";

        private static readonly string DisplayName3 = "FeatUnholyFortitude.Name";
        private static readonly string Description3 = "FeatUnholyFortitude.Description";

        public static void UnholyFortitudeConfigure()
        {
            var icon = FeatureRefs.BardLoreMaster.Reference.Get().Icon;

            var feat = FeatureConfigurator.New(Feat3Name, Feat3Guid)
                    .SetDisplayName(DisplayName3)
                    .SetDescription(Description3)
                    .SetIcon(icon)
                    .AddComponent<UnholyFortitudeLogic>()
                    .Configure();

            ProgressionConfigurator.For(ProgressionRefs.LichProgression.Reference)
                .AddToLevelEntry(3, feat)
                .Configure();
        }
    }

    internal class UnholyFortitudeLogic : UnitFactComponentDelegate, IUnitSubscriber, ISubscriber, IInitiatorRulebookHandler<RuleSavingThrow>, IRulebookHandler<RuleSavingThrow>, IInitiatorRulebookSubscriber, IUnitGainFactHandler, IUnitLostFactHandler, IUnitNewCombatRoundHandler
    {
        public override void OnTurnOn()
        {
            Apply();
        }

        // Token: 0x0600E570 RID: 58736 RVA: 0x003AB2CE File Offset: 0x003A94CE
        public override void OnTurnOff()
        {
            base.Owner.Stats.HitPoints.BaseStat = HitPointsBaseStat.Constitution;
        }

        void IRulebookHandler<RuleSavingThrow>.OnEventAboutToTrigger(RuleSavingThrow evt)
        {
            if (Owner.HasFact(Undead)) return; 
            int num = Owner.Stats.Charisma.Bonus - Owner.Stats.Constitution.Bonus;
            if (num <= 0) return;
            evt.AddTemporaryModifier(evt.Initiator.Stats.SaveFortitude.AddModifier(num, base.Runtime));
        }

        void IRulebookHandler<RuleSavingThrow>.OnEventDidTrigger(RuleSavingThrow evt)
        {
            
        }

        void IUnitGainFactHandler.HandleUnitGainFact(EntityFact fact)
        {
            Apply();
        }
        void IUnitLostFactHandler.HandleUnitLostFact(EntityFact fact)
        {
            Apply();
        }

        void IUnitNewCombatRoundHandler.HandleNewCombatRound(UnitEntityData unit)
        {
            Apply();
        }

        private void Apply()
        {
            int con = Owner.Descriptor.Stats.Constitution;
            int cha = Owner.Descriptor.Stats.Charisma;
            if (con >= cha)
            {
                base.Owner.Stats.HitPoints.BaseStat = HitPointsBaseStat.Constitution;
            }
            else
            {
                base.Owner.Stats.HitPoints.BaseStat = HitPointsBaseStat.Charisma;
            }
        }

        private static BlueprintFeatureReference Undead = BlueprintTool.GetRef<BlueprintFeatureReference>(FeatureRefs.UndeadImmunities.ToString());
    }
}
