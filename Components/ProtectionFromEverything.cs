using HarmonyLib;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Components
{
    internal class ProtectionFromEverything : ProtectionFromEnergy
    {
    }

    [HarmonyPatch(typeof(AddDamageResistanceEnergy), nameof(AddDamageResistanceEnergy.Bypassed))]
    internal static class ProtectionFromEverythingPatch
    {
        private static void Postfix(ref AddDamageResistanceEnergy __instance, ref bool __result, ref BaseDamage damage)
        {
            Main.Logger.Info("ProtectionFromEverything11");
            if (__instance is ProtectionFromEverything)
            {
                Main.Logger.Info("ProtectionFromEverything");
                if (damage is EnergyDamage energy)
                {
                    __result = energy.EnergyType == Kingmaker.Enums.Damage.DamageEnergyType.Magic;
                }
                else if (damage is PhysicalDamage)
                {
                    __result = false;
                }
                else
                {
                    __result = true;
                }
            }
        }
    }
}
