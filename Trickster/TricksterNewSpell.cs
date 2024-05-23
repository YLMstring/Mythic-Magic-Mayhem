using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using MythicMagicMayhem.Demon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder.ContextEx;
using static Kingmaker.EntitySystem.Properties.BaseGetter.PropertyContextAccessor;
using MythicMagicMayhem.Aeon;
using MythicMagicMayhem.Azata;
using Kingmaker.UnitLogic.Abilities.Components;

namespace MythicMagicMayhem.Trickster
{
    internal class TricksterNewSpell
    {
        private const string HallMirrorsAbility = "NewSpell.UseHallMirrors";
        public static readonly string HallMirrorsAbilityGuid = "{18A1E6C0-0040-4EDB-8C18-6F764BD5E6F4}";

        private const string HallMirrorsBuff = "NewSpell.HallMirrorsBuff";
        public static readonly string HallMirrorsBuffGuid = "{A6201A04-84E3-436C-852F-6BED94B845E5}";

        internal const string DisplayName = "NewSpellHallMirrors.Name";
        private const string Description = "NewSpellHallMirrors.Description";
        public static BlueprintAbility HallMirrorsConfigure()
        {
            var icon = AbilityRefs.DLC3_ThePromissingMirrorAbility.Reference.Get().Icon;

            var mirror = ActionsBuilder.New()
                .ApplyBuff(BuffRefs.MirrorImageBuff.ToString(), ContextDuration.Fixed(1), isFromSpell: true)
                .Build();

            var buff = BuffConfigurator.New(HallMirrorsBuff, HallMirrorsBuffGuid)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .AddBuffActions(activated: mirror, newRound: mirror)
              .Configure();

            return AbilityConfigurator.NewSpell(HallMirrorsAbility, HallMirrorsAbilityGuid, SpellSchool.Illusion, canSpecialize: true)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              .SetIcon(icon)
              .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Ally, radius: 30.Feet(), spreadSpeed: 20.Feet())
              .SetRange(AbilityRange.Personal)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend)
              .SetLocalizedDuration(Duration.OneMinute)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .ApplyBuff(buff, ContextDuration.Fixed(10), isFromSpell: true)
                  .Build())
              .Configure();
        }

        private const string RainHalberdiersAbility = "NewSpell.UseRainHalberdiers";
        public static readonly string RainHalberdiersAbilityGuid = "{0680AD26-35F1-4875-9AA6-C792A4081193}";

        private const string RainHalberdiersBuff = "NewSpell.RainHalberdiersBuff";
        public static readonly string RainHalberdiersBuffGuid = "{16CB55C7-836A-4E7C-BED7-C2DC8F8063A3}";

        private const string RainHalberdiersBuff2 = "NewSpell.RainHalberdiersBuff2";
        public static readonly string RainHalberdiersBuff2Guid = "{0A6F618A-8898-40D6-AA47-7E902FCD4470}";

        internal const string DisplayName2 = "NewSpellRainHalberdiers.Name";
        private const string Description2 = "NewSpellRainHalberdiers.Description";
        public static BlueprintAbility RainHalberdiersConfigure()
        {
            var icon = AbilityRefs.DemonTeleport.Reference.Get().Icon;

            var end = ActionsBuilder.New()
                .Add<ContextActionBreachEnd>()
                .Build();

            var summon = ActionsBuilder.New()
                .Add<ContextActionBreachSummon>()
                .Build();

            var buff = BuffConfigurator.New(RainHalberdiersBuff, RainHalberdiersBuffGuid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddBuffActions(deactivated: end, newRound: summon)
              .AddToFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.StayOnDeath)
              .Configure();

            BuffConfigurator.New(RainHalberdiersBuff2, RainHalberdiersBuff2Guid)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AddComponent<MMMDestroyOnDeactivate>()
              .Configure();

            return AbilityConfigurator.NewSpell(RainHalberdiersAbility, RainHalberdiersAbilityGuid, SpellSchool.Conjuration, canSpecialize: false)
              .SetDisplayName(DisplayName2)
              .SetDescription(Description2)
              .SetIcon(icon)
              .AllowTargeting(true, false, false, false)
              .SetRange(AbilityRange.Close)
              .SetType(AbilityType.Spell)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal, Metamagic.Heighten, Metamagic.Extend)
              .SetSpellDescriptor(SpellDescriptor.Summoning)
              .SetLocalizedDuration(Duration.OneMinute)
              .AddAbilityCasterHasNoFacts(new() { buff })
              .SetIsFullRoundAction(true)
              .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                  .ApplyBuff(buff, ContextDuration.Fixed(10), toCaster: true)
                  .Add<ContextActionBreachStart>()
                  .Build())
              .Configure();
        }

        private const string TrickDeveloperAbility = "NewSpell.UseTrickDeveloper";
        public static readonly string TrickDeveloperAbilityGuid = "{7858AA17-605A-4468-8680-CF932283C6BE}";

        internal const string DisplayName3 = "NewSpellTrickDeveloper.Name";
        private const string Description3 = "NewSpellTrickDeveloper.Description";
        public static BlueprintAbility TrickDeveloperConfigure()
        {
            var icon = AbilityRefs.TrickeryBlessingMajorAbility.Reference.Get().Icon;

            return AbilityConfigurator.NewSpell(TrickDeveloperAbility, TrickDeveloperAbilityGuid, SpellSchool.Abjuration, canSpecialize: false)
              .SetDisplayName(DisplayName3)
              .SetDescription(Description3)
              .SetIcon(icon)
              .SetAvailableMetamagic(Metamagic.CompletelyNormal)
              .AddAbilityVariants(new() { AbilityRefs.AngelArmyOfHeaven.ToString(), AbilityRefs.AbsoluteDeath.ToString(), DemonNewSpell.AbyssalBreachAbilityGuid, AeonNewSpell.AbsoluteAuthorityAbility1Guid, AzataNewSpell.ElysiumChoirAbilityGuid })
              .Configure();
        }
    }
}
