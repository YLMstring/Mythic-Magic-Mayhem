using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Armies.Components;
using Kingmaker.Blueprints;
using Kingmaker.Utility;
using MythicMagicMayhem.Aeon;
using MythicMagicMayhem.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Demon
{
    internal class DemonSpellTweak
    {
        public static void Patch()
        {
            var book = SpellbookRefs.DemonSpellbook.Reference.Get();
            book.m_SpellsPerDay = SpellbookRefs.AngelSpellbook.Reference.Get().m_SpellsPerDay;
            book.m_SpellsKnown = SpellbookRefs.OracleSpellbook.Reference.Get().m_SpellsKnown;
            //"LongArmAbility": "fde4d2da-3043-45bc-a11a-73eb8f06b755",
            SpellStuff.AddSpellLevel(BlueprintTool.GetRef<BlueprintAbilityReference>("fde4d2da-3043-45bc-a11a-73eb8f06b755"), SpellListRefs.DemonUsualSpelllist.Reference, 1);
            SpellStuff.ChangeSpellLevel(AbilityRefs.DemonicFormI.Reference, SpellListRefs.DemonSpelllist.Reference, 3, 2);
            SpellStuff.ChangeSpellLevel(AbilityRefs.FlamesOfTheAbyss.Reference, SpellListRefs.DemonSpelllist.Reference, 4, 2);
            SpellStuff.AddSpellLevel(AbilityRefs.Slow.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.DemonicFormII.Reference, SpellListRefs.DemonSpelllist.Reference, 5, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.MorbidRestoration.Reference, SpellListRefs.DemonSpelllist.Reference, 4, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.ProfaneHymn.Reference, SpellListRefs.DemonSpelllist.Reference, 4, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TelekineticStrike.Reference, SpellListRefs.DemonSpelllist.Reference, 5, 3);
            SpellStuff.ChangeSpellLevel(AbilityRefs.AbyssalSkin.Reference, SpellListRefs.DemonSpelllist.Reference, 6, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.AbyssalChains.Reference, SpellListRefs.DemonSpelllist.Reference, 5, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.DemonTeleport.Reference, SpellListRefs.DemonSpelllist.Reference, 6, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.ChannelRageCommunal.Reference, SpellListRefs.DemonSpelllist.Reference, 6, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.DemonicFormIII.Reference, SpellListRefs.DemonSpelllist.Reference, 7, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.InfectiousRage.Reference, SpellListRefs.DemonSpelllist.Reference, 8, 4);
            SpellStuff.ChangeSpellLevel(AbilityRefs.AbyssalStorm.Reference, SpellListRefs.DemonSpelllist.Reference, 7, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.DemonicFormIV.Reference, SpellListRefs.DemonSpelllist.Reference, 9, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.DevourCast.Reference, SpellListRefs.DemonSpelllist.Reference, 9, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.Lifebane.Reference, SpellListRefs.DemonSpelllist.Reference, 7, 5);
            SpellStuff.ChangeSpellLevel(AbilityRefs.TelekineticBurst.Reference, SpellListRefs.DemonSpelllist.Reference, 8, 5);
            SpellStuff.AddSpellLevel(AbilityRefs.BearsEnduranceMass.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.BullsStrengthMass.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.ChainLightning.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.FoxsCunningMass.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.Sirocco.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.Transformation.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 6);
            SpellStuff.AddSpellLevel(AbilityRefs.CausticEruption.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.KiShout_0.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.LegendaryProportions.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.TrueSeeingCommunal.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.WalkThroughSpace.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.WavesOfExhaustion.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 7);
            SpellStuff.AddSpellLevel(AbilityRefs.FrightfulAspect.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.PolarRay.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.ProtectionFromSpells.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.RiftOfRuin.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.ShoutGreater.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.Stormbolts.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(DemonNewSpell.BlindFuryConfigure(), SpellListRefs.DemonSpelllist.Reference, 8);
            SpellStuff.AddSpellLevel(AbilityRefs.DominateMonster.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.EnergyDrain.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.FieryBody.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.HoldMonsterMass.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(AbilityRefs.MindBlankCommunal.Reference, SpellListRefs.DemonUsualSpelllist.Reference, 9);
            SpellStuff.AddSpellLevel(DemonNewSpell.AbyssalBreachConfigure(), SpellListRefs.DemonSpelllist.Reference, 10);
            SpellStuff.AddSpellLevel(DemonNewSpell.DemonicFormVConfigure(), SpellListRefs.DemonSpelllist.Reference, 10);
            if (ContextActionBreachSummon.demons.Count() > 0) { return; }
            foreach (var unit in UnitRefs.All)
            {
                var demon = unit.Reference.Get();
                if (demon.CR <= 25 && demon.CR >= 13 && demon.m_AddFacts?.Contains(FeatureRefs.SubtypeDemon.Reference.Get().ToReference<BlueprintUnitFactReference>()) == true && demon.GetComponent<ArmyUnitComponent>() == null && demon.Faction?.m_AttackFactions?.Contains(FactionRefs.Player.Reference.Get()) == true)
                {
                    ContextActionBreachSummon.demons.Add(unit.Reference);
                }
            }
        }
    }
}
