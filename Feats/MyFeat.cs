using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;

namespace MythicMagicMayhem.Feats
{
  /// <summary>
  /// Creates a feat that does nothing but show up.
  /// </summary>
  public class MyFeat
  {
    private static readonly string FeatName = "MyFeat";
    private static readonly string FeatGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e20bb";

    private static readonly string DisplayName = "MyFeat.Name";
    private static readonly string Description = "MyFeat.Description";
    private static readonly string Icon = "assets/icons/quillen.jpg";

    public static void Configure()
    {
      FeatureConfigurator.New(FeatName, FeatGuid, FeatureGroup.Feat)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(Icon)
        .Configure(delayed: true);
    }
  }
}
