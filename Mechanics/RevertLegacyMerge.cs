using HarmonyLib;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker;

namespace MythicMagicMayhem.Mechanics
{
    internal class RevertLegacyMerge
    {
        [HarmonyBefore(new string[] { "SpellbookMerge" })]
        [HarmonyPatch(typeof(ApplySpellbook), nameof(ApplySpellbook.Apply), new Type[] { typeof(LevelUpState), typeof(UnitDescriptor) })]
        private static class ApplySpellbookApplyPatch
        {
            private static bool Prefix(LevelUpState state, UnitDescriptor unit)
            {
                if (!ModMenu.ModMenu.GetSettingValue<bool>(Main.GetKey("tg8"))) return true;
                Apply(state, unit);
                return false;
            }

            public static void Apply(LevelUpState state, UnitDescriptor unit)
            {
                Main.Logger.Info("Start Owlcat Merger...");
                if (state.SelectedClass == null)
                {
                    return;
                }
                SkipLevelsForSpellProgression component = state.SelectedClass.GetComponent<SkipLevelsForSpellProgression>();
                if (component != null && component.Levels.Contains(state.NextClassLevel))
                {
                    return;
                }
                ClassData classData = unit.Progression.GetClassData(state.SelectedClass);
                if (classData == null)
                {
                    return;
                }
                if (classData.Spellbook != null)
                {
                    Spellbook spellbook = unit.DemandSpellbook(classData.Spellbook);
                    if (state.SelectedClass.Spellbook && state.SelectedClass.Spellbook != classData.Spellbook)
                    {
                        Spellbook spellbook2 = unit.Spellbooks.FirstOrDefault((Spellbook s) => s.Blueprint == state.SelectedClass.Spellbook);
                        if (spellbook2 != null)
                        {
                            foreach (AbilityData abilityData in spellbook2.GetAllKnownSpells())
                            {
                                spellbook.AddKnown(abilityData.SpellLevel, abilityData.Blueprint, true);
                            }
                            unit.DeleteSpellbook(state.SelectedClass.Spellbook);
                        }
                    }
                    int num = classData.CharacterClass.IsMythic ? spellbook.CasterLevel : spellbook.BaseLevel;
                    spellbook.AddLevelFromClass(classData.CharacterClass);
                    int classLevel = classData.CharacterClass.IsMythic ? spellbook.CasterLevel : spellbook.BaseLevel;
                    bool flag = num == 0;
                    SpellSelectionData spellSelectionData = state.DemandSpellSelection(spellbook.Blueprint, spellbook.Blueprint.SpellList);
                    if (spellbook.Blueprint.SpellsKnown != null)
                    {
                        BlueprintSpellsTable blueprintSpellsTable = spellbook.Blueprint.SpellsKnown;
                        if (classData.CharacterClass.IsMythic)
                        {
                            UnitFact unitFact = unit.Facts.Get<UnitFact>((UnitFact x) => x.Blueprint is BlueprintFeatureSelectMythicSpellbook);
                            BlueprintFeatureSelectMythicSpellbook blueprintFeatureSelectMythicSpellbook = ((unitFact != null) ? unitFact.Blueprint : null) as BlueprintFeatureSelectMythicSpellbook;
                            if (blueprintFeatureSelectMythicSpellbook != null)
                            {
                                blueprintSpellsTable = blueprintFeatureSelectMythicSpellbook.SpellKnownForSpontaneous;
                                if (blueprintSpellsTable != null)
                                {
                                    num = spellbook.MythicLevel - 1;
                                    classLevel = spellbook.MythicLevel;
                                }
                                else
                                {
                                    PFLog.Default.Error(string.Format("Mythic Spellbook {0} doesn't contains SpellKnownForSpontaneous table!", blueprintFeatureSelectMythicSpellbook), Array.Empty<object>());
                                }
                            }
                        }
                        for (int i = 0; i <= 10; i++)
                        {
                            int num2 = Math.Max(0, blueprintSpellsTable.GetCount(num, i));
                            int num3 = Math.Max(0, blueprintSpellsTable.GetCount(classLevel, i));
                            spellSelectionData.SetLevelSpells(i, Math.Max(0, num3 - num2));
                        }
                    }
                    int maxSpellLevel = spellbook.MaxSpellLevel;
                    if (spellbook.Blueprint.SpellsPerLevel > 0)
                    {
                        if (flag)
                        {
                            spellSelectionData.SetExtraSpells(0, maxSpellLevel);
                            spellSelectionData.ExtraByStat = true;
                            spellSelectionData.UpdateMaxLevelSpells(unit);
                        }
                        else
                        {
                            spellSelectionData.SetExtraSpells(spellbook.Blueprint.SpellsPerLevel, maxSpellLevel);
                        }
                    }
                    foreach (AddCustomSpells customSpells in spellbook.Blueprint.GetComponents<AddCustomSpells>())
                    {
                        ApplySpellbook.TryApplyCustomSpells(spellbook, customSpells, state, unit);
                    }
                }
            }
        }
    }
}