using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
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

            FeatureConfigurator.For(FeatureRefs.UndeadMountFeature.Reference)
                .AddFeatureToPet(feat, PetType.AnimalCompanion)
                .SetReapplyOnLevelUp(true)
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
                    .AddPrerequisitePlayerHasFeature(ProgressionRefs.LichProgression.ToString())
                    .SetHideNotAvailibleInUI(true)
                    .Configure();
        }
    }
}
