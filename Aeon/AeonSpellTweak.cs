using BlueprintCore.Blueprints.References;
using MythicMagicMayhem.Azata;
using MythicMagicMayhem.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Aeon
{
    internal class AeonSpellTweak
    {
        public static void Patch()
        {
            var book = SpellbookRefs.AeonSpellbook.Reference.Get();
            book.m_SpellsPerDay = SpellbookRefs.AngelSpellbook.Reference.Get().m_SpellsPerDay;
            book.m_SpellsKnown = SpellbookRefs.OracleSpellbook.Reference.Get().m_SpellsKnown;
            SpellStuff.ChangeSpellLevel(AbilityRefs.CrystalMind.Reference, SpellListRefs.AeonSpellMythicList.Reference, 3, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EdictOfRetaliation.Reference, SpellListRefs.AeonSpellMythicList.Reference, 3, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EqualForce.Reference, SpellListRefs.AeonSpellMythicList.Reference, 3, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EdictOfNonresistance.Reference, SpellListRefs.AeonSpellMythicList.Reference, 4, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.Relativity.Reference, SpellListRefs.AeonSpellMythicList.Reference, 4, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.Starlight.Reference, SpellListRefs.AeonSpellMythicList.Reference, 4, 3);
            SpellStuff.AddSpellLevel(AbilityRefs.InflictCriticalWoundsCast.Reference, SpellListRefs.AeonSpellList.Reference, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.ChainsOfLight.Reference, SpellListRefs.AeonSpellList.Reference, 6, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EdictOfPredetermination.Reference, SpellListRefs.AeonSpellMythicList.Reference, 5, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TrueForm.Reference, SpellListRefs.AeonSpellMythicList.Reference, 5, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.UncertanityPrinciple.Reference, SpellListRefs.AeonSpellMythicList.Reference, 6, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.AbsoluteOrder.Reference, SpellListRefs.AeonSpellMythicList.Reference, 7, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EdictOfPerseverance.Reference, SpellListRefs.AeonSpellMythicList.Reference, 6, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.Supernova.Reference, SpellListRefs.AeonSpellMythicList.Reference, 6, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.ZeroState.Reference, SpellListRefs.AeonSpellMythicList.Reference, 7, 5);
            SpellStuff.AddSpellLevel(AbilityRefs.HarmCast.Reference, SpellListRefs.AeonSpellList.Reference, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EdictOfImpenetrableFortress.Reference, SpellListRefs.AeonSpellMythicList.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EmbodimentOfOrder.Reference, SpellListRefs.AeonSpellMythicList.Reference, 7, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.FreezingNothingness.Reference, SpellListRefs.AeonSpellMythicList.Reference, 8, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.ZoneOfPredetermination.Reference, SpellListRefs.AeonSpellMythicList.Reference, 8, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.HoldPersonMass.Reference, SpellListRefs.AeonSpellList.Reference, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.ResonatingWord.Reference, SpellListRefs.AeonSpellList.Reference, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.EdictOfInvulnerability.Reference, SpellListRefs.AeonSpellMythicList.Reference, 8, 7);
            SpellStuff.ChangeSpellLevel(AbilityRefs.ShoutGreater.Reference, SpellListRefs.AeonSpellList.Reference, 8, 6);
            SpellStuff.ChangeSpellLevel(AbilityRefs.OverwhelmingPresence.Reference, SpellListRefs.AeonSpellList.Reference, 9, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.ProtectionFromSpells.Reference, SpellListRefs.AeonSpellList.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.MindBlank.Reference, SpellListRefs.AeonSpellList.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.PowerWordStun.Reference, SpellListRefs.AeonSpellList.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.PredictionOfFailure.Reference, SpellListRefs.AeonSpellList.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.Sunburst.Reference, SpellListRefs.AeonSpellList.Reference, 8);
            SpellStuff.AddSpellLevel(AeonNewSpell.TotalNullificationConfigure(), SpellListRefs.AeonSpellMythicList.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.DominateMonster.Reference, SpellListRefs.AeonSpellList.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.Foresight.Reference, SpellListRefs.AeonSpellList.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.HeroicInvocation.Reference, SpellListRefs.AeonSpellList.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.HoldMonsterMass.Reference, SpellListRefs.AeonSpellList.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.MindBlankCommunal.Reference, SpellListRefs.AeonSpellList.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.PowerWordKill.Reference, SpellListRefs.AeonSpellList.Reference, 9);
            SpellStuff.AddSpellLevel(AeonNewSpell.AbsoluteAuthorityConfigure(), SpellListRefs.AeonSpellMythicList.Reference, 10);
        }
    }
}
