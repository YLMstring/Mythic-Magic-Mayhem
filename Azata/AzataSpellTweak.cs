using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using MythicMagicMayhem.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Azata
{
    internal class AzataSpellTweak
    {
        public static void Patch()
        {
            var book = SpellbookRefs.AzataSpellbook.Reference.Get();
            book.m_SpellsPerDay = SpellbookRefs.AngelSpellbook.Reference.Get().m_SpellsPerDay;
            book.m_SpellsKnown = SpellbookRefs.OracleSpellbook.Reference.Get().m_SpellsKnown;
            SpellStuff.AddSpellLevel(AbilityRefs.Snowball.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 1);
            SpellStuff.AddSpellLevel(AbilityRefs.RemoveFear.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 1);
            SpellStuff.ChangeSpellLevel(AbilityRefs.OdeToMiraculousMagic.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 3, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.RejuvenatingPoem.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 3, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.FieldOfFlowers.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 4, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.VoiceOfRenewal.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 4, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.Rage.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 11, 3);
            SpellStuff.AddSpellLevel(AbilityRefs.Haste.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.NaturesGrasp.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 4, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.OptimisticSmile.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 4, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.RainbowDome.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 5, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.ChaoticHealing.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 5, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.DeadlyBeauty.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 5, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.Waterfall.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 6, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.FriendlyHug.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 6, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.SecondBreath.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 6, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.SuddenSquall.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 6, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EuphoricTranquilityCast.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 8, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.OverwhelmingPresence.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 9, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.JoyOfLife.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.ProtectionOfNature.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.SongsOfSteel.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.WindsOfTheFall.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.RainbowStarfall.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 8, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.PrismaticSpray.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.WaterTorrent.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 8, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.HeroNeverSurrender.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 8, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.RainbowArrows.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 9, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.Revolt.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 9, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.UnbreakableBond.Reference, SpellListRefs.AzataMythicSpellsSpelllist.Reference, 9, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.Seamantle.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.PolarRay.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.SummonElementalElderBase.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.Stormbolts.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AzataNewSpell.GroupHugConfigure(), SpellListRefs.AzataMythicSpellsSpelllist.Reference, 8);
            //SpellStuff.AddSpellLevel(AbilityRefs.DominateMonster.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.Tsunami.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.IcyPrisonMass.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 9);
            //SpellStuff.AddSpellLevel(AbilityRefs.HoldMonsterMass.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AzataNewSpell.ElysiumChoirConfigure(), SpellListRefs.AzataMythicSpellsSpelllist.Reference, 10);
            SpellStuff.ChangeSpellLevel(AbilityRefs.PowerWordStun.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 11, 8);
            SpellStuff.ChangeSpellLevel(AbilityRefs.HeroicInvocation.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 11, 9);
            SpellStuff.ChangeSpellLevel(AbilityRefs.PowerWordKill.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 11, 9);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EvilSuccubusDominate.Reference, SpellListRefs.AzataMythicSpelllist.Reference, 11, 9);
            SpellStuff.AddSpellLevel(AzataNewSpell.DragonDanceConfigure(), SpellListRefs.AzataMythicSpellsSpelllist.Reference, 10);
            var comp = FeatureRefs.BurstOfSonicEnergyFeature.Reference.Get()?.GetComponent<AddKnownSpell>();
            if (comp != null)
            {
                comp.SpellLevel = 4;
            }
        }
    }
}
