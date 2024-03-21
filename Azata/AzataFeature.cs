using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.UnitLogic.ActivatableAbilities;
using MythicMagicMayhem.Angel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Azata
{
    internal class AzataFeature
    {
        private static readonly string FeatName = "FeatNewSuperp1";
        public static readonly string FeatGuid = "{2853F560-BB94-46F5-94D1-5824C476E346}";

        private static readonly string DisplayName = "MMMFeatNewSuperp1.Name";
        private static readonly string Description = "MMMFeatNewSuperp1.Description";

        public static void NewSuperp1Configure()
        {
            var icon = FeatureRefs.FascinateFeature.Reference.Get().Icon;

            FeatureConfigurator.New(FeatName, FeatGuid, Kingmaker.Blueprints.Classes.FeatureGroup.AzataSuperpowerAbilities)
                    .SetDisplayName(DisplayName)
                    .SetDescription(Description)
                    .SetIcon(icon)
                    .AddIncreaseActivatableAbilityGroupSize(ActivatableAbilityGroup.BardicPerformance)
                    .AddIncreaseActivatableAbilityGroupSize(ActivatableAbilityGroup.AzataMythicPerformance)
                    .Configure();
        }
    }
}
