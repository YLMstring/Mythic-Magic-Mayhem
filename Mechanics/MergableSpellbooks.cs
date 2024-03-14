using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Spells;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Mechanics
{
    internal class MergableSpellbooks
    {
        private static readonly string TricksterFeatName = "MergableSpellbooksTrickster";
        private static readonly string TricksterFeatGuid = "{40E8CAAE-4F2C-43A2-AFDA-3B218D116BE2}";

        private static readonly string DemonFeatName = "MergableSpellbooksDemon";
        private static readonly string DemonFeatGuid = "{1FB17BDF-B9CA-4430-B895-BE0B64400512}";

        private static readonly string AzataFeatName = "MergableSpellbooksAzata";
        private static readonly string AzataFeatGuid = "{9310036B-9916-4CAE-BF4D-7E8C29D97A13}";

        private static readonly string AeonFeatName = "MergableSpellbooksAeon";
        private static readonly string AeonFeatGuid = "{6ACCBFCA-6F6D-4A2D-8FF0-16AC48FD3D5F}";

        private static readonly string DisplayName = "MergableSpellbooks.Name";
        private static readonly string Description = "MergableSpellbooks.Description";
        public static void Patch()
        {
            var books = Get9thBooks();

            var angel = FeatureSelectMythicSpellbookRefs.AngelIncorporateSpellbook.Reference.Get();
            angel.m_AllowedSpellbooks = books;
            FeatureSelectMythicSpellbookConfigurator.For(FeatureSelectMythicSpellbookRefs.AngelIncorporateSpellbook)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();

            var lich = FeatureSelectMythicSpellbookRefs.LichIncorporateSpellbookFeature.Reference.Get();
            lich.m_AllowedSpellbooks = books;
            FeatureSelectMythicSpellbookConfigurator.For(FeatureSelectMythicSpellbookRefs.LichIncorporateSpellbookFeature)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideInUI(false)
                .Configure();

            var trickster = FeatureSelectMythicSpellbookConfigurator.New(TricksterFeatName, TricksterFeatGuid)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIsClassFeature(true)
                .SetMythicSpellList(SpellListRefs.TricksterSpelllistMythic.ToString())
                .SetSpellKnownForSpontaneous(SpellsTableRefs.MythicSpontaneousSpellsKnownTable.ToString())
                .Configure();
            trickster.m_AllowedSpellbooks = books;
            ProgressionConfigurator.For(ProgressionRefs.TricksterProgression)
                .AddToLevelEntries(1, trickster)
                .Configure();

            var demon = FeatureSelectMythicSpellbookConfigurator.New(DemonFeatName, DemonFeatGuid)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIsClassFeature(true)
                .SetMythicSpellList(SpellListRefs.DemonSpelllist.ToString())
                .SetSpellKnownForSpontaneous(SpellsTableRefs.MythicSpontaneousSpellsKnownTable.ToString())
                .Configure();
            demon.m_AllowedSpellbooks = books;
            ProgressionConfigurator.For(ProgressionRefs.DemonProgression)
                .AddToLevelEntries(1, demon)
                .Configure();

            var azata = FeatureSelectMythicSpellbookConfigurator.New(AzataFeatName, AzataFeatGuid)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIsClassFeature(true)
                .SetMythicSpellList(SpellListRefs.AzataMythicSpellsSpelllist.ToString())
                .SetSpellKnownForSpontaneous(SpellsTableRefs.MythicSpontaneousSpellsKnownTable.ToString())
                .Configure();
            azata.m_AllowedSpellbooks = books;
            ProgressionConfigurator.For(ProgressionRefs.AzataProgression)
                .AddToLevelEntries(1, azata)
                .Configure();

            var aeon = FeatureSelectMythicSpellbookConfigurator.New(AeonFeatName, AeonFeatGuid)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIsClassFeature(true)
                .SetMythicSpellList(SpellListRefs.AeonSpellMythicList.ToString())
                .SetSpellKnownForSpontaneous(SpellsTableRefs.MythicSpontaneousSpellsKnownTable.ToString())
                .Configure();
            aeon.m_AllowedSpellbooks = books;
            ProgressionConfigurator.For(ProgressionRefs.AeonProgression)
                .AddToLevelEntries(1, aeon)
                .Configure();
        }

        private static BlueprintSpellbookReference[] Get9thBooks()
        {
            var books = new List<BlueprintSpellbookReference>() { };
            var clazzs = RootRefs.BlueprintRoot.Reference.Get().Progression.AvailableCharacterClasses;
            foreach (var clazz in clazzs)
            {
                if (GetMaxLevel(clazz.Spellbook) > 9)
                {
                    books.Add(clazz.Spellbook.ToReference<BlueprintSpellbookReference>());
                    Main.Logger.Info("Make " + clazz.Spellbook.NameSafe() + " mergable");
                }
                if (!clazz.Archetypes.Any()) continue;
                foreach (var archetype in clazz.Archetypes)
                {
                    if (GetMaxLevel(archetype.ReplaceSpellbook) > 9)
                    {
                        books.Add(archetype.ReplaceSpellbook.ToReference<BlueprintSpellbookReference>());
                        Main.Logger.Info("Make " + archetype.ReplaceSpellbook.NameSafe() + " mergable");
                    }
                }
            }
            return books.ToArray();
        }

        private static int GetMaxLevel(BlueprintSpellbook book)
        {
            var levels = book?.m_SpellsPerDay?.Get()?.Levels;
            if (levels == null) return 0;
            return levels.Last().Count.Length;
        }
    }
}
