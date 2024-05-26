using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Utility;
using MythicMagicMayhem.Components;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace MythicMagicMayhem.Azata
{
    internal class AzataNewSpell
    {
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
                  .Build())
              .Configure();
        }
    }
}
