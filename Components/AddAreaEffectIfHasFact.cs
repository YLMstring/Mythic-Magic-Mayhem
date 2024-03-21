using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Kingmaker.UnitLogic.Class.LevelUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythicMagicMayhem.Components
{
    internal class AddAreaEffectIfHasFact : AddAreaEffect
    {
        public BlueprintFeatureReference RequiredFact;
    }


    [HarmonyPatch(typeof(AddAreaEffect), nameof(AddAreaEffect.IsOwnerAvatarValid))]
    internal static class AreaEffectIfHasFactPatch
    {
        private static void Postfix(ref AddAreaEffect __instance, ref bool __result)
        {
            if (__instance is AddAreaEffectIfHasFact eff && !__instance.Owner.HasFact(eff.RequiredFact))
            {
                __result = false;
            }
        }
    }
}
