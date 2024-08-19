using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Utility;
using MythicMagicMayhem.Aeon;
using MythicMagicMayhem.Components;
using MythicMagicMayhem.Demon;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace MythicMagicMayhem.Azata
{
    internal class AzataNewSpell
    {
        private const string DragonDanceAbility = "NewSpell.UseDragonDance";
        public static readonly string DragonDanceAbilityGuid = "{9942707F-4003-4846-B4DD-BFD46D1B67FD}";

        private const string DragonDanceAbility1 = "NewSpell.UseDragonDance1";
        public static readonly string DragonDanceAbilityGuid1 = "{2DFE0952-14DA-47DB-86E0-78520C593C69}";

        private const string DragonDanceBuff1 = "NewSpell.DragonDanceBuff1";
        private static readonly string DragonDanceBuffGuid1 = "{14130D2A-65EE-4367-B16D-635F704B6BF0}";

        private const string DragonDanceAbility2 = "NewSpell.UseDragonDance2";
        public static readonly string DragonDanceAbilityGuid2 = "{B4402763-C479-45B2-B149-87639EA2466A}";

        private const string DragonDanceBuff2 = "NewSpell.DragonDanceBuff2";
        private static readonly string DragonDanceBuffGuid2 = "{98966BAF-CA40-430F-A4E6-983C286C6FF4}";

        private const string DragonDanceAbility3 = "NewSpell.UseDragonDance3";
        public static readonly string DragonDanceAbilityGuid3 = "{7608AC8A-84E3-4A6F-ADF2-F5605F8F40AB}";

        private const string DragonDanceBuff3 = "NewSpell.DragonDanceBuff3";
        private static readonly string DragonDanceBuffGuid3 = "{68DB4241-4E7F-4029-B9C5-2E38CD8678D8}";

        private const string DragonDanceAbility4 = "NewSpell.UseDragonDance4";
        public static readonly string DragonDanceAbilityGuid4 = "{8F9BAAE7-A3CD-4438-860D-6CD5F5381553}";

        private const string DragonDanceBuff4 = "NewSpell.DragonDanceBuff4";
        private static readonly string DragonDanceBuffGuid4 = "{34DB17F5-507D-48F5-844A-A8E9B0A6D835}";

        private const string DragonDanceAbility5 = "NewSpell.UseDragonDance5";
        public static readonly string DragonDanceAbilityGuid5 = "{0C70A2EA-31C2-4EE1-8AF9-D19EEBCB5FC9}";

        private const string DragonDanceBuff5 = "NewSpell.DragonDanceBuff5";
        private static readonly string DragonDanceBuffGuid5 = "{4E695553-72E0-4E7B-BDC4-B8CBD9A32B4C}";

        internal const string DisplayName10 = "NewSpellDragonDance.Name";
        private const string Description10 = "NewSpellDragonDance.Description";

        internal const string DisplayName11 = "NewSpellDragonDance1.Name";
        private const string Description11 = "NewSpellDragonDance1.Description";

        internal const string DisplayName12 = "NewSpellDragonDance2.Name";
        private const string Description12 = "NewSpellDragonDance2.Description";

        internal const string DisplayName13 = "NewSpellDragonDance3.Name";
        private const string Description13 = "NewSpellDragonDance3.Description";

        internal const string DisplayName14 = "NewSpellDragonDance4.Name";
        private const string Description14 = "NewSpellDragonDance4.Description";

        internal const string DisplayName15 = "NewSpellDragonDance5.Name";
        private const string Description15 = "NewSpellDragonDance5.Description";
        public static BlueprintAbility DragonDanceConfigure()
        {
            var icon = AbilityRefs.DragonAzataBuffAlliesAbility.Reference.Get().Icon;

            var buff1 = BuffConfigurator.New(DragonDanceBuff1, DragonDanceBuffGuid1)
              .SetDisplayName(DisplayName11)
              .SetDescription(Description11)
              .SetIcon(icon)
              .AddEnergyVulnerability(Kingmaker.Enums.Damage.DamageEnergyType.Fire)
              .Configure();

            var ability1 = AbilityConfigurator.NewSpell(DragonDanceAbility1, DragonDanceAbilityGuid1, SpellSchool.Evocation, canSpecialize: true)
              .SetDisplayName(DisplayName11)
              .SetDescription(Description11)
              .SetIcon(AbilityRefs.ScorchingRay.Reference.Get().Icon)
              .AllowTargeting(false, true, false, false)
              .SetLocalizedDuration(Duration.OneMinute)
              .SetRange(AbilityRange.Long)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Empower, Metamagic.Quicken, Metamagic.Maximize)
              .AddAbilityDeliverChain(projectile: ProjectileRefs.ArrowFire00.ToString(), projectileFirst: ProjectileRefs.ArrowFire00.ToString(), radius: 30.Feet(), targetDead: false, targetsCount: ContextValues.Rank(), targetType: TargetType.Enemy, usedTargetsAgain: false, ignoreFirst: false)
              .AddContextRankConfig(ContextRankConfigs.CasterLevel())
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .DealDamage(value: ContextDice.Value(DiceType.D6, bonus: 0, diceCount: ContextValues.Rank()), damageType: DamageTypes.Energy(type: Kingmaker.Enums.Damage.DamageEnergyType.Fire), halfIfSaved: true)
                  .ApplyBuff(buff1, ContextDuration.Fixed(10))
                  .Build(), savingThrowType: Kingmaker.EntitySystem.Stats.SavingThrowType.Reflex)
              .Configure();

            var buff2 = BuffConfigurator.New(DragonDanceBuff2, DragonDanceBuffGuid2)
              .SetDisplayName(DisplayName12)
              .SetDescription(Description12)
              .SetIcon(icon)
              .AddEnergyVulnerability(Kingmaker.Enums.Damage.DamageEnergyType.Cold)
              .Configure();

            var ability2 = AbilityConfigurator.NewSpell(DragonDanceAbility2, DragonDanceAbilityGuid2, SpellSchool.Evocation, canSpecialize: true)
              .SetDisplayName(DisplayName12)
              .SetDescription(Description12)
              .SetIcon(AbilityRefs.ColdIceStrike.Reference.Get().Icon)
              .AllowTargeting(false, true, false, false)
              .SetLocalizedDuration(Duration.OneMinute)
              .SetRange(AbilityRange.Long)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Empower, Metamagic.Quicken, Metamagic.Maximize)
              .AddAbilityDeliverChain(projectile: ProjectileRefs.ColdCommonProjectile00.ToString(), projectileFirst: ProjectileRefs.ColdCommonProjectile00.ToString(), radius: 30.Feet(), targetDead: false, targetsCount: ContextValues.Rank(), targetType: TargetType.Enemy, usedTargetsAgain: false, ignoreFirst: false)
              .AddContextRankConfig(ContextRankConfigs.CasterLevel())
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .DealDamage(value: ContextDice.Value(DiceType.D6, bonus: 0, diceCount: ContextValues.Rank()), damageType: DamageTypes.Energy(type: Kingmaker.Enums.Damage.DamageEnergyType.Cold), halfIfSaved: true)
                  .ApplyBuff(buff2, ContextDuration.Fixed(10))
                  .Build(), savingThrowType: Kingmaker.EntitySystem.Stats.SavingThrowType.Reflex)
              .Configure();

            var buff3 = BuffConfigurator.New(DragonDanceBuff3, DragonDanceBuffGuid3)
              .SetDisplayName(DisplayName13)
              .SetDescription(Description13)
              .SetIcon(icon)
              .AddEnergyVulnerability(Kingmaker.Enums.Damage.DamageEnergyType.Electricity)
              .Configure();

            var ability3 = AbilityConfigurator.NewSpell(DragonDanceAbility3, DragonDanceAbilityGuid3, SpellSchool.Evocation, canSpecialize: true)
              .SetDisplayName(DisplayName13)
              .SetDescription(Description13)
              .SetIcon(AbilityRefs.ChainLightning.Reference.Get().Icon)
              .AllowTargeting(false, true, false, false)
              .SetLocalizedDuration(Duration.OneMinute)
              .SetRange(AbilityRange.Long)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Empower, Metamagic.Quicken, Metamagic.Maximize)
              .AddAbilityDeliverChain(projectile: ProjectileRefs.ElectroCommonProjectile00.ToString(), projectileFirst: ProjectileRefs.ElectroCommonProjectile00.ToString(), radius: 30.Feet(), targetDead: false, targetsCount: ContextValues.Rank(), targetType: TargetType.Enemy, usedTargetsAgain: false, ignoreFirst: false)
              .AddContextRankConfig(ContextRankConfigs.CasterLevel())
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .DealDamage(value: ContextDice.Value(DiceType.D6, bonus: 0, diceCount: ContextValues.Rank()), damageType: DamageTypes.Energy(type: Kingmaker.Enums.Damage.DamageEnergyType.Electricity), halfIfSaved: true)
                  .ApplyBuff(buff3, ContextDuration.Fixed(10))
                  .Build(), savingThrowType: Kingmaker.EntitySystem.Stats.SavingThrowType.Reflex)
              .Configure();

            var buff4 = BuffConfigurator.New(DragonDanceBuff4, DragonDanceBuffGuid4)
              .SetDisplayName(DisplayName14)
              .SetDescription(Description14)
              .SetIcon(icon)
              .AddEnergyVulnerability(Kingmaker.Enums.Damage.DamageEnergyType.Acid)
              .Configure();

            var ability4 = AbilityConfigurator.NewSpell(DragonDanceAbility4, DragonDanceAbilityGuid4, SpellSchool.Evocation, canSpecialize: true)
              .SetDisplayName(DisplayName14)
              .SetDescription(Description14)
              .SetIcon(AbilityRefs.AcidArrow.Reference.Get().Icon)
              .AllowTargeting(false, true, false, false)
              .SetLocalizedDuration(Duration.OneMinute)
              .SetRange(AbilityRange.Long)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Empower, Metamagic.Quicken, Metamagic.Maximize)
              .AddAbilityDeliverChain(projectile: ProjectileRefs.AcidArrow00.ToString(), projectileFirst: ProjectileRefs.AcidArrow00.ToString(), radius: 30.Feet(), targetDead: false, targetsCount: ContextValues.Rank(), targetType: TargetType.Enemy, usedTargetsAgain: false, ignoreFirst: false)
              .AddContextRankConfig(ContextRankConfigs.CasterLevel())
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .DealDamage(value: ContextDice.Value(DiceType.D6, bonus: 0, diceCount: ContextValues.Rank()), damageType: DamageTypes.Energy(type: Kingmaker.Enums.Damage.DamageEnergyType.Acid), halfIfSaved: true)
                  .ApplyBuff(buff4, ContextDuration.Fixed(10))
                  .Build(), savingThrowType: Kingmaker.EntitySystem.Stats.SavingThrowType.Reflex)
              .Configure();

            var buff5 = BuffConfigurator.New(DragonDanceBuff5, DragonDanceBuffGuid5)
              .SetDisplayName(DisplayName15)
              .SetDescription(Description15)
              .SetIcon(icon)
              .AddEnergyVulnerability(Kingmaker.Enums.Damage.DamageEnergyType.Sonic)
              .Configure();

            var ability5 = AbilityConfigurator.NewSpell(DragonDanceAbility5, DragonDanceAbilityGuid5, SpellSchool.Evocation, canSpecialize: true)
              .SetDisplayName(DisplayName15)
              .SetDescription(Description15)
              .SetIcon(AbilityRefs.ArcanistExploitSonicBlastAbility.Reference.Get().Icon)
              .AllowTargeting(false, true, false, false)
              .SetLocalizedDuration(Duration.OneMinute)
              .SetRange(AbilityRange.Long)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Empower, Metamagic.Quicken, Metamagic.Maximize)
              .AddAbilityDeliverChain(projectile: ProjectileRefs.SonicCommonRay00_Projectile.ToString(), projectileFirst: ProjectileRefs.SonicCommonRay00_Projectile.ToString(), radius: 30.Feet(), targetDead: false, targetsCount: ContextValues.Rank(), targetType: TargetType.Enemy, usedTargetsAgain: false, ignoreFirst: false)
              .AddContextRankConfig(ContextRankConfigs.CasterLevel())
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .DealDamage(value: ContextDice.Value(DiceType.D6, bonus: 0, diceCount: ContextValues.Rank()), damageType: DamageTypes.Energy(type: Kingmaker.Enums.Damage.DamageEnergyType.Sonic), halfIfSaved: true)
                  .ApplyBuff(buff5, ContextDuration.Fixed(10))
                  .Build(), savingThrowType: Kingmaker.EntitySystem.Stats.SavingThrowType.Reflex)
              .Configure();

            return AbilityConfigurator.NewSpell(DragonDanceAbility, DragonDanceAbilityGuid, SpellSchool.Evocation, canSpecialize: true)
              .SetDisplayName(DisplayName10)
              .SetDescription(Description10)
              .SetIcon(icon)
              .SetRange(AbilityRange.Long)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Empower, Metamagic.Quicken, Metamagic.Maximize)
              .AddAbilityVariants([ability1, ability2, ability3, ability4, ability5])
              .Configure();
        }

        private const string GroupHugAbility1 = "NewSpell.UseGroupHug1";
        public static readonly string GroupHugAbility1Guid = "{D12E2DFB-D841-4BA0-893B-2BE19C535DDC}";

        internal const string DisplayName = "NewSpellGroupHug.Name";
        private const string Description = "NewSpellGroupHug.Description";
        public static BlueprintAbility GroupHugConfigure()
        {
            var icon = AbilityRefs.FriendlyHug.Reference.Get().Icon;
            var fx = AbilityRefs.AuraOfGreaterCourage.Reference.Get().GetComponent<AbilitySpawnFx>();

            return AbilityConfigurator.NewSpell(
                GroupHugAbility1, GroupHugAbility1Guid, SpellSchool.Abjuration, canSpecialize: false)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .SetLocalizedDuration(Duration.MinutePerLevel)
              .SetRange(AbilityRange.Personal)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Quicken)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Ally, radius: 15.Feet(), spreadSpeed: 20.Feet())
              .AddComponent(fx)
              //.AddToSpellLists(level: 8, SpellList.AzataMythic)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .CastSpell(AbilityRefs.FriendlyHug.ToString(), overrideSpellbook: true)
                  .Build())
              .AddCraftInfoComponent(
                aOEType: CraftAOE.AOE,
                savingThrow: CraftSavingThrow.None,
                spellType: CraftSpellType.Buff)
              .Configure();
        }

        private const string ElysiumChoirAbility = "NewSpell.UseElysiumChoir";
        public static readonly string ElysiumChoirAbilityGuid = "{763C03D3-4DA2-42C7-96D6-376AEC5206B4}";

        private const string ElysiumChoirBuff = "NewSpell.ElysiumChoirBuff";
        private static readonly string ElysiumChoirBuffGuid = "{FED04615-8874-4C04-B039-B6144F8D88EC}";

        internal const string DisplayName2 = "NewSpellElysiumChoir.Name";
        private const string Description2 = "NewSpellElysiumChoir.Description";

        public static BlueprintAbility ElysiumChoirConfigure()
        {
            var icon = FeatureRefs.FascinateFeature.Reference.Get().Icon;

            var buff = BuffConfigurator.New(ElysiumChoirBuff, ElysiumChoirBuffGuid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddComponent<AddAreaEffectIfHasFact>(c => { 
                  c.RequiredFact = FeatureRefs.SongOfHeroicResolveFeature.Reference.Get(); 
                  c.m_AreaEffect = AbilityAreaEffectRefs.SongOfHeroicResolveArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>(); 
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SongOfBrokenChainsFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.SongOfBrokenChainsArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SongOfCourageousDefenderFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.SongOfCourageousDefenderArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SongOfDefianceFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.SongOfDefianceArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SongOfInspirationFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.SongOfInspirationArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SongOfStrength.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.SongOfStrengthArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SongOfTheFallen.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.SongOfTheFallenArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SongOfTheSensesFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.SongOfTheSensesArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.InspireCourageFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireCourageArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SenseiInspireCourageFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireCourageArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.MartyrInspireCourageFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireCourageArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.InspireCompetenceFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireCompetenceArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SenseiInspireCompetenceFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireCompetenceArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              }).AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.FascinateFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.FascinateArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.DirgeOfDoomFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.DirgeOfDoomArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.InspireGreatnessFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireGreatnessArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.SenseiInspireGreatnessFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireGreatnessArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.MartyrInspireGreatnessFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireGreatnessArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.FrighteningTuneFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.FrighteningTuneArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.InspireHeroicsFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireHeroicsArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.MartyrInspireHeroicsFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireHeroicsArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.InspiredRage.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspiredRageArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.DirgeOfDoom.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.DirgeOfDoomArea_0.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.StormCallFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.StormCallArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.FlameDancerPerformanceFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.FlameDancerPerformanceArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.InspireTranquilityFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InspireTranquilityArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.BeastTamerInspireFerocityFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.BeastTamerInspireFerocityArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.InsightfulContemplationSongFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.InsightfulContemplationSongArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.FakeInspireCourageFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.FakeInspireCourageArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.FakeInspireGreatnessFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.FakeInspireGreatnessArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = FeatureRefs.FakeInspireHeroicsFeature.Reference.Get();
                  c.m_AreaEffect = AbilityAreaEffectRefs.FakeInspireHeroicsArea.Reference.Get().ToReference<BlueprintAbilityAreaEffectReference>();
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = BlueprintTool.GetRef<BlueprintFeatureReference>("241ce86c60b54392a988c61a5d75293f");
                  c.m_AreaEffect = BlueprintTool.GetRef<BlueprintAbilityAreaEffectReference>("2fe3beace4484edb9a5e66de15e763cd");
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = BlueprintTool.GetRef<BlueprintFeatureReference>("2c575afe75a24905bde456308514b048");
                  c.m_AreaEffect = BlueprintTool.GetRef<BlueprintAbilityAreaEffectReference>("09e27ddbefb34cb9a398b0c8224687fe");
              })
              .AddComponent<AddAreaEffectIfHasFact>(c => {
                  c.RequiredFact = BlueprintTool.GetRef<BlueprintFeatureReference>("59077996944b4ae1bef4cdd1b872ef8d");
                  c.m_AreaEffect = BlueprintTool.GetRef<BlueprintAbilityAreaEffectReference>("141d633de63c477885463d4e836ae1a6");
              })
              .Configure();

            //var fx = AbilityRefs.SmiteEvilAbility.Reference.Get().GetComponent<AbilitySpawnFx>();

            return AbilityConfigurator.NewSpell(
                ElysiumChoirAbility, ElysiumChoirAbilityGuid, SpellSchool.Enchantment, canSpecialize: false)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .SetLocalizedDuration(Duration.OneMinute)
              //.AddComponent(fx)
              .AllowTargeting(false, true, true, true)
              .SetAnimation(CastAnimationStyle.Immediate)
              .SetIsFullRoundAction(true)
              .SetRange(AbilityRange.Long)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .ApplyBuff(buff, ContextDuration.Fixed(10), isFromSpell: true, toCaster: true)
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.DeadlyPerformanceFeature.ToString()).IsEnemy().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell(AbilityRefs.DeadlyPerformanceAbility.ToString()).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.ArchaeologistLuckFeature.ToString()).Build(),
                    ifTrue: ActionsBuilder.New().ApplyBuff(BuffRefs.ArchaeologistLuckBuffMythic.ToString(), ContextDuration.Fixed(10), isFromSpell: true, toCaster: true).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.ThunderCall.ToString()).IsEnemy().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell(AbilityRefs.ThundercallerSoundBurst.ToString()).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.DirgeBardDanceOfTheDeadFeature.ToString()).Build(),
                    ifTrue: ActionsBuilder.New().CastSpell(AbilityRefs.DirgeBardAnimateDead.ToString()).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.BattleSingerFirstVerseFeature.ToString()).IsEnemy().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell(AbilityRefs.BattleSingerFirstVerseAbility.ToString()).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.BattleSingerSecondVerseFeature.ToString()).IsEnemy().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell(AbilityRefs.BattleSingerSecondVerseAbility.ToString()).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.BattleSingerThirdVerseFeature.ToString()).IsEnemy().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell(AbilityRefs.BattleSingerThirdVerseAbility.ToString()).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.SongOfCourageousDefenderFeature.ToString()).IsAlly().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell(AbilityRefs.SongOfCourageousDefenderChoseCompanionAbility.ToString()).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.SoothingPerformanceFeature.ToString()).IsAlly().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell(AbilityRefs.SoothingPerformanceAbility.ToString()).Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact("f7dcab1cd75140488bacb73b04e29b5c").IsEnemy().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell("40e997669e6b4dbaaee8b9a093cfb506").Build())
                  .Conditional(ConditionsBuilder.New().CasterHasFact("6b600329edb14b8ca658a2e8e39b8610").IsEnemy().Build(),
                    ifTrue: ActionsBuilder.New().CastSpell("6cc1c799e163429dbf22f8ee97c6e377").Build())
                  .Build())
              .Configure();
        }
    }
}
