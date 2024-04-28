using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.References;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Kingmaker.Visual.Particles.FxSpawnSystem;
using Kingmaker.Visual.Particles;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using static Kingmaker.Visual.Animation.IKController;

namespace MythicMagicMayhem.Demon
{
    internal class BreachUnitPart : OldStyleUnitPart
    {
        public Vector3 position;
        public IFxHandle hand;
    }

    internal class ContextActionBreachStart : ContextAction
    {
        public override string GetCaption()
        {
            return "Breach Start";
        }

        public override void RunAction()
        {
            if (Context.MaybeCaster == null) { return; }
            var prefab = AbilityRefs.DimensionalRetributionAbility.Reference.Get().GetComponent<AbilityCustomDimensionDoor>()?.PortalToPrefab?.Load(false, false);
            IFxHandle hand = null;
            if (prefab != null)
            {
                hand = FxHelper.SpawnFxOnPoint(prefab, Target.Point, false, Quaternion.identity);
            }
            var part = Context.MaybeCaster.Ensure<BreachUnitPart>();
            part.position = Target.Point;
            part.hand = hand;
        }
    }

    internal class ContextActionBreachEnd : ContextAction
    {
        public override string GetCaption()
        {
            return "Breach End";
        }

        public override void RunAction()
        {
            var part = Context.MaybeCaster?.Get<BreachUnitPart>();
            if (part == null) { return; }
            FxHelper.Destroy(part.hand);
            part.hand = null;
        }
    }
}
