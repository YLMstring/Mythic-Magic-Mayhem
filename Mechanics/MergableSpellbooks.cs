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
            var books = GetBooks();

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

            // String, where your guid file :sus_emoji:
            var gd = FeatureSelectMythicSpellbookConfigurator.New("gdspellbook", "2cd312fb-390d-4123-804e-c6f5b93383f1")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetSpellKnownForSpontaneous(SpellsTableRefs.MythicSpontaneousSpellsKnownTable.Reference.Get())
                .SetMythicSpellList(SpellListRefs.GoldDragonSpellList.Reference.Get())
                .Configure();
            gd.m_AllowedSpellbooks = books;
            // add the spell book configurator to the GD progression
            ProgressionConfigurator.For(ProgressionRefs.GoldenDragonProgression)
                .AddToLevelEntry(1, gd)
                .Configure();

            var hacker = SpellbookRefs.MagicDeceiverSpellbook.Reference.Get();
            if (hacker?.GetComponent<MagicHackSpellbookComponent>() != null)
            {
                hacker.GetComponent<MagicHackSpellbookComponent>().m_MaxDamageDicesPerAction = [5, 7, 10, 15, 20, 100, 100, 100, 100, 100];
            }
        }

        private static BlueprintSpellbookReference[] GetBooks()
        {
            var books = new List<BlueprintSpellbookReference>() { 
                SpellbookRefs.ThassilonianAbjurationSpellbook.Reference.Get().ToReference<BlueprintSpellbookReference>(),
                SpellbookRefs.ThassilonianConjurationSpellbook.Reference.Get().ToReference<BlueprintSpellbookReference>(),
                SpellbookRefs.ThassilonianEnchantmentSpellbook.Reference.Get().ToReference<BlueprintSpellbookReference>(),
                SpellbookRefs.ThassilonianEvocationSpellbook.Reference.Get().ToReference<BlueprintSpellbookReference>(),
                SpellbookRefs.ThassilonianIllusionSpellbook.Reference.Get().ToReference<BlueprintSpellbookReference>(),
                SpellbookRefs.ThassilonianNecromancySpellbook.Reference.Get().ToReference<BlueprintSpellbookReference>(),
                SpellbookRefs.ThassilonianTransmutationSpellbook.Reference.Get().ToReference<BlueprintSpellbookReference>()
            };
            var clazzs = RootRefs.BlueprintRoot.Reference.Get().Progression.AvailableCharacterClasses;
            var fours = new HashSet<BlueprintSpellsTable>() { };
            var sixs = new HashSet<BlueprintSpellsTable>() { };
            foreach (var clazz in clazzs)
            {
                if (clazz.Archetypes.Any()) 
                {
                    foreach (var archetype in clazz.Archetypes)
                    {
                        if (GetMaxLevel(archetype.ReplaceSpellbook) == 0) { continue; }
                        books.Add(archetype.ReplaceSpellbook.ToReference<BlueprintSpellbookReference>());
                        Main.Logger.Info("Make " + archetype.ReplaceSpellbook.NameSafe() + " mergable");
                        if (GetMaxLevel(archetype.ReplaceSpellbook) == 5)
                        {
                            fours.Add(archetype.ReplaceSpellbook.SpellsPerDay);
                        }
                        else if (GetMaxLevel(archetype.ReplaceSpellbook) == 7)
                        {
                            sixs.Add(archetype.ReplaceSpellbook.SpellsPerDay);
                        }
                    }
                }
                if (GetMaxLevel(clazz.Spellbook) == 0) { continue; }
                books.Add(clazz.Spellbook.ToReference<BlueprintSpellbookReference>());
                Main.Logger.Info("Make " + clazz.Spellbook.NameSafe() + " mergable");
                if (GetMaxLevel(clazz.Spellbook) == 5)
                {
                    fours.Add(clazz.Spellbook.SpellsPerDay);
                }
                else if (GetMaxLevel(clazz.Spellbook) == 7)
                {
                    sixs.Add(clazz.Spellbook.SpellsPerDay);
                }
            }
            foreach (var table in fours)
            {
                Convert4th(table);
            }
            foreach (var table in sixs)
            {
                Convert6th(table);
            }
            return books.ToArray();
        }

        private static int GetMaxLevel(BlueprintSpellbook book)
        {
            var levels = book?.m_SpellsPerDay?.Get()?.Levels;
            if (levels == null) return 0;
            return levels[17].Count.Length;
        }

        private static void Convert4th(BlueprintSpellsTable table)
        {
            var list = new List<SpellsLevelEntry>();
            SpellsLevelEntry entry = null;
            foreach (var level in table.Levels)
            {
                if (list.Count >= 18) { break; }
                list.Add(level);
                entry = level;
            }
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 0 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 1 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 1, 0 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 1, 1 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 2, 1, 0 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 2, 1, 1 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 2, 2, 1, 0 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 3, 2, 1, 1 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 3, 2, 2, 1, 0 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 3, 3, 2, 1, 1 })
            });
            table.Levels = list.ToArray();
            Main.Logger.Info("Converting 4th Table " + table.AssetGuidThreadSafe);
        }
        private static void Convert6th(BlueprintSpellsTable table)
        {
            var list = new List<SpellsLevelEntry>();
            SpellsLevelEntry entry = null;
            foreach (var level in table.Levels)
            {
                if (list.Count >= 21) { break; }
                list.Add(level);
                entry = level;
            }
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 1 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 2 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 2, 1 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 2, 2 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 3, 2, 1 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 3, 2, 2 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 3, 3, 2 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 4, 3, 2, 1 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 4, 3, 3, 2 })
            });
            list.Add(new SpellsLevelEntry()
            {
                Count = MergeCount(entry.Count, new int[] { 4, 4, 3, 2 })
            });
            table.Levels = list.ToArray();
            Main.Logger.Info("Converting 6th Table " + table.AssetGuidThreadSafe);
        }

        private static int[] MergeCount(int[] oldcount, int[] newcount)
        {
            var list = new List<int>();
            foreach (var num in oldcount)
            {
                list.Add(num);
            }
            foreach (var num in newcount)
            {
                list.Add(num);
            }
            return list.ToArray();
        }
    }
}
