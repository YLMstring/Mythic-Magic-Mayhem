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
using Kingmaker.Designers.EventConditionActionSystem.ContextData;
using Kingmaker.View;
using Kingmaker.Utility;
using Kingmaker.Armies.Components;

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

    internal class ContextActionBreachSummon : ContextAction
    {
        public override string GetCaption()
        {
            return "Breach Summon";
        }

        public override void RunAction()
        {
            var part = Context.MaybeCaster?.Get<BreachUnitPart>();
            if (part == null) { return; }
            int cr = UnityEngine.Random.Range(13, 26);
            Summon(Context.MaybeCaster, part.position, GetUnit(cr));
            var prefab = AbilityRefs.DimensionalRetributionAbility.Reference.Get().GetComponent<AbilityCustomDimensionDoor>()?.PortalToPrefab?.Load(false, false);
            if (prefab != null)
            {
                FxHelper.SpawnFxOnPoint(prefab, part.position, false, Quaternion.identity);
            }
        }

        public static void Summon(UnitEntityData caster, Vector3 position, BlueprintUnit unit)
        {
            UnitEntityData maybeCaster = caster;
            Vector3 vector = position;
            vector = ObstacleAnalyzer.GetNearestNode(vector, null).position;
            UnitEntityView unitEntityView = unit.Prefab.Load(false, false);
            float radius = (unitEntityView != null) ? unitEntityView.Corpulence : 0.5f;
            FreePlaceSelector.PlaceSpawnPlaces(1, radius, vector);
            UnitEntityData unitEntityData = Game.Instance.EntityCreator.SpawnUnit(unit, vector, Quaternion.identity, maybeCaster.HoldingState, null);
            unitEntityData.GroupId = maybeCaster.GroupId;
            unitEntityData.UpdateGroup();
            unitEntityData.Descriptor.AddBuff(Game.Instance.BlueprintRoot.SystemMechanics.SummonedUnitBuff, caster, new TimeSpan(0, 1, 0), null);
        }

        public BlueprintUnit GetUnit(int cr)
        {
            if (demons.Count > 0)
            {
                var list = demons.FindAll(delegate(BlueprintUnit u)
                {
                    return u.CR == cr;
                });
                if (list?.Count > 0)
                {
                    return list.Random();
                }
                return GetUnit(cr + 1);
            }
            foreach (var unit in UnitRefs.All)
            {
                var demon = unit.Reference.Get();
                if (demon.CR <= 25 && demon.CR >= 13 && demon.m_AddFacts?.Contains(FeatureRefs.SubtypeDemon.Reference.Get().ToReference<BlueprintUnitFactReference>()) == true && demon.GetComponent<ArmyUnitComponent>() == null && demon.Faction?.m_AttackFactions?.Contains(FactionRefs.Player.Reference.Get()) == true)
                {
                    demons.Add(unit.Reference);
                }  
            }
            return GetUnit(cr);
        }

        private List<BlueprintUnit> demons = new() { };
    }
}
