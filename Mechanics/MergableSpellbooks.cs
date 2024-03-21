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

            var trickster = FeatureSelectMythicSpellbookConfigurator.For("c4ef6975-167d-4cf5-acbf-d66b60e63f9c")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();
            trickster.m_AllowedSpellbooks = books;

            var demon = FeatureSelectMythicSpellbookConfigurator.For("f3ff8515-355e-4738-b128-c3d01483f1ca")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();
            demon.m_AllowedSpellbooks = books;

            var azata = FeatureSelectMythicSpellbookConfigurator.For("83385d9f-4d71-4e4e-9461-8703be762a20")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();
            azata.m_AllowedSpellbooks = books;

            var aeon = FeatureSelectMythicSpellbookConfigurator.For("2b7027ee-76cb-4c58-b2cf-f0475bc69fbb")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .Configure();
            aeon.m_AllowedSpellbooks = books;
        }

        private static BlueprintSpellbookReference[] Get9thBooks()
        {
            var books = new List<BlueprintSpellbookReference>() { };
            var clazzs = RootRefs.BlueprintRoot.Reference.Get().Progression.AvailableCharacterClasses;
            foreach (var clazz in clazzs)
            {
                if (GetMaxLevel(clazz.Spellbook) > 2)
                {
                    books.Add(clazz.Spellbook.ToReference<BlueprintSpellbookReference>());
                    Main.Logger.Info("Make " + clazz.Spellbook.NameSafe() + " mergable");
                }
                if (!clazz.Archetypes.Any()) continue;
                foreach (var archetype in clazz.Archetypes)
                {
                    if (GetMaxLevel(archetype.ReplaceSpellbook) > 2)
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
