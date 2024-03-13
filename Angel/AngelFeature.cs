using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic;
using Owlcat.Runtime.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace MythicMagicMayhem.Angel
{
    internal class AngelFeature
    {
        private static readonly string FeatName = "FeatNewHalo1";
        public static readonly string FeatGuid = "{1477586B-DC59-41AC-ABE7-DBF2227ABF64}";

        private static readonly string DisplayName = "MMMFeatNewHalo1.Name";
        private static readonly string Description = "MMMFeatNewHalo1.Description";

        public static void NewHalo1Configure()
        {
            var icon = AbilityRefs.BestowGraceOfTheChampionCast.Reference.Get().Icon;

            FeatureConfigurator.New(FeatName, FeatGuid, Kingmaker.Blueprints.Classes.FeatureGroup.AngelMythcHalo)
                    .SetDisplayName(DisplayName)
                    .SetDescription(Description)
                    .SetIcon(icon)
                    .AddComponent<ChangeEnergyDamageType>(c => { c.halo = BuffRefs.AngelHaloBuff.Reference; c.Type = DamageTypes.Energy(DamageEnergyType.Holy); })
                    .Configure();
        }

        private static readonly string Feat2Name = "FeatNewHalo2";
        public static readonly string Feat2Guid = "{202E74CA-528C-4410-B369-2A7EAD7DD893}";

        private static readonly string DisplayName2 = "MMMFeatNewHalo2.Name";
        private static readonly string Description2 = "MMMFeatNewHalo2.Description";

        public static void NewHalo2Configure()
        {
            var icon = AbilityRefs.FlameStrike.Reference.Get().Icon;

            FeatureConfigurator.New(Feat2Name, Feat2Guid, Kingmaker.Blueprints.Classes.FeatureGroup.AngelMythcHalo)
                    .SetDisplayName(DisplayName2)
                    .SetDescription(Description2)
                    .SetIcon(icon)
                    .AddComponent<ChangeEnergyDamageType>(c => { c.halo = BuffRefs.AngelHaloBuff.Reference; c.Type = DamageTypes.Energy(DamageEnergyType.Fire); })
                    .Configure();
        }
    }

    public class ChangeEnergyDamageType : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleDealDamage>, IRulebookHandler<RuleDealDamage>, ISubscriber, IInitiatorRulebookSubscriber, RuleDealDamage.IOutgoingDamageHandler, IUnitSubscriber
    {
        // Token: 0x0600C331 RID: 49969 RVA: 0x0032F750 File Offset: 0x0032D950
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            if (!Owner.HasFact(halo)) { return; }
            BaseDamage first = evt.DamageBundle.First;
            bool flag;
            if (first == null)
            {
                flag = false;
            }
            else
            {
                EntityFact sourceFact = first.SourceFact;
                if (sourceFact == null)
                {
                    flag = false;
                }
                else
                {
                    MechanicsContext maybeContext = sourceFact.MaybeContext;
                    flag = (maybeContext?.ParentContext) != null;
                }
            }
            if (!flag && evt.DamageBundle.Weapon == null)
            {
                return;
            }
            List<BaseDamage> list = TempList.Get<BaseDamage>();
            foreach (BaseDamage damage in evt.DamageBundle)
            {
                list.Add(this.ChangeType(damage));
            }
            evt.Remove((BaseDamage _) => true);
            foreach (BaseDamage damage2 in list)
            {
                evt.Add(damage2);
            }
        }

        // Token: 0x0600C332 RID: 49970 RVA: 0x0032F850 File Offset: 0x0032DA50
        public void OnEventDidTrigger(RuleDealDamage evt)
        {
        }

        // Token: 0x0600C333 RID: 49971 RVA: 0x0032F852 File Offset: 0x0032DA52
        public void HandleOutgoingDamageWillBeAdded(BaseDamage damage, out BaseDamage damageToAdd)
        {
            damageToAdd = this.ChangeType(damage);
        }

        // Token: 0x0600C334 RID: 49972 RVA: 0x0032F860 File Offset: 0x0032DA60
        private BaseDamage ChangeType(BaseDamage damage)
        {
            if (damage.Type != DamageType.Energy) { return damage; }
            if (damage.Type == this.Type.Type)
            {
                DamageType type = damage.Type;
                if (type != DamageType.Energy)
                {
                    if (type - DamageType.Force > 2)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    return damage;
                }
                else
                {
                    DamageEnergyType? damageEnergyType = (damage is EnergyDamage energyDamage) ? new DamageEnergyType?(energyDamage.EnergyType) : null;
                    DamageEnergyType energy = this.Type.Energy;
                    if (damageEnergyType.GetValueOrDefault() == energy & damageEnergyType != null)
                    {
                        return damage;
                    }
                }
            }
            BaseDamage baseDamage = this.Type.CreateDamage(new ModifiableDiceFormula(damage.Dice), damage.Bonus);
            baseDamage.CopyFrom(damage);
            return baseDamage;
        }

        // Token: 0x040083CD RID: 33741
        public DamageTypeDescription Type;
        public BlueprintBuff halo;
    }
}
