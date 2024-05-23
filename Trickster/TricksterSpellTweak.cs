using BlueprintCore.Blueprints.References;
using MythicMagicMayhem.Azata;
using MythicMagicMayhem.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Trickster
{
    internal class TricksterSpellTweak
    {
        public static void Patch()
        {
            var book = SpellbookRefs.TricksterSpellbook.Reference.Get();
            book.m_SpellsPerDay = SpellbookRefs.AngelSpellbook.Reference.Get().m_SpellsPerDay;
            book.m_SpellsKnown = SpellbookRefs.OracleSpellbook.Reference.Get().m_SpellsKnown;
            SpellStuff.AddSpellLevel(AbilityRefs.Glitterdust.Reference, SpellListRefs.TricksterSpelllist.Reference, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterFishMissile.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 3, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterBreathOfMoney.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 4, 3);
            SpellStuff.AddSpellLevel(AbilityRefs.ConfusionSpell.Reference, SpellListRefs.TricksterSpelllist.Reference, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterSummonGreaslyBear.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 5, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterMicroscopicProportions.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 6, 5);
            //SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterRainOfHalberds.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 8, 5);
            SpellStuff.AddSpellLevel(AbilityRefs.CloakofDreams.Reference, SpellListRefs.TricksterSpelllist.Reference, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.Serenity.Reference, SpellListRefs.TricksterSpelllist.Reference, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterSummonPerpetuallyAnnoyedWizard.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterRecreationalPit.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 7, 6);
            //SpellStuff.AddSpellLevel(AzataNewSpell.GroupHugConfigure(), SpellListRefs.TricksterSpelllistMythic.Reference, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.HoldPersonMass.Reference, SpellListRefs.TricksterSpelllist.Reference, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.UmbralStrike.Reference, SpellListRefs.TricksterSpelllist.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterRayOfHalberds.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 8, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterSummonHogOfDesolation.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 8, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TricksterTrickFate.Reference, SpellListRefs.TricksterSpelllistMythic.Reference, 9, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.EuphoricTranquilityCast.Reference, SpellListRefs.TricksterSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.PredictionOfFailure.Reference, SpellListRefs.TricksterSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.MindBlank.Reference, SpellListRefs.TricksterSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.ScintillatingPattern.Reference, SpellListRefs.TricksterSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.DominateMonster.Reference, SpellListRefs.TricksterSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.HoldMonsterMass.Reference, SpellListRefs.TricksterSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.MindBlankCommunal.Reference, SpellListRefs.TricksterSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.Shades.Reference, SpellListRefs.TricksterSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.Weird.Reference, SpellListRefs.TricksterSpelllist.Reference, 9);
            //SpellStuff.AddSpellLevel(AzataNewSpell.ElysiumChoirConfigure(), SpellListRefs.TricksterSpelllistMythic.Reference, 9);
            //SpellStuff.AddSpellLevel(AzataNewSpell.ElysiumChoirConfigure(), SpellListRefs.TricksterSpelllistMythic.Reference, 10);
        }
    }
}
