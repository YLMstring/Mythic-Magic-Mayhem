using MythicMagicMayhem.Feats;
using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using UnityModManagerNet;
using ModMenu.Settings;
using MythicMagicMayhem.Menu;
using System.Text;
using MythicMagicMayhem.Mechanics;
using MythicMagicMayhem.Angel;
using MythicMagicMayhem.Lich;
using MythicMagicMayhem.Azata;
using MythicMagicMayhem.Aeon;
using MythicMagicMayhem.Demon;
using MythicMagicMayhem.Trickster;

namespace MythicMagicMayhem
{
  public static class Main
  {
    public static bool Enabled;
    public static readonly LogWrapper Logger = LogWrapper.Get("MythicMagicMayhem");
        private static readonly string RootKey = "mod-menu.mmm-settings";
        public static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }
        public static bool Load(UnityModManager.ModEntry modEntry)
    {
      try
      {
        modEntry.OnToggle = OnToggle;
        var harmony = new Harmony(modEntry.Info.Id);
        harmony.PatchAll();
        Logger.Info("Finished patching.");
      }
      catch (Exception e)
      {
        Logger.Error("Failed to patch", e);
      }
      return true;
    }

    public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
    {
      Enabled = value;
      return true;
    }
        private static void Onclick()
        {
            var log = new StringBuilder();
            log.AppendLine("Current settings: ");
            ///log.AppendLine($"-Toggle: {CheckToggle()}");
            log.AppendLine($"-Default Slider Float: {ModMenu.ModMenu.GetSettingValue<float>(GetKey("float-default"))}");
            log.AppendLine($"-Slider Float: {ModMenu.ModMenu.GetSettingValue<float>(GetKey("float"))}");
            log.AppendLine($"-Default Slider Int: {ModMenu.ModMenu.GetSettingValue<int>(GetKey("int-default"))}");
            log.AppendLine($"-Slider Int: {ModMenu.ModMenu.GetSettingValue<int>(GetKey("int"))}");
            Logger.Info(log.ToString());
        }

        [HarmonyPatch(typeof(BlueprintsCache))]
    static class BlueprintsCaches_Patch
    {
      private static bool Initialized = false;

      [HarmonyPriority(Priority.First)]
      [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
            static void Init()
            {
                ModMenu.ModMenu.AddSettings(
                SettingsBuilder.New(RootKey, Helpers.CreateString("title-mmm", "Mythic Magic Mayhem"))
                .AddDefaultButton().AddButton(Button.New(
            Helpers.CreateString("button-desc-mmm", "Restart the game to apply changes!"), Helpers.CreateString("button-text-mmm", "Do Not Turn Any Chosen Features Off"), Onclick))
          .AddToggle(
            Toggle.New(GetKey("tg1"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm1", "Lich Spell"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg2"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm2", "Lich Mount"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg3"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm3", "Lich Companion Feat"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg4"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm4", "Lich Fortitude"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg5"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm5", "Angel Spell"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg6"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm6", "Angel Halo"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg9"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm9", "Azata Spell"))
              .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg10"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm10", "Azata Superpower"))
              .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg11"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm11", "Aeon Spell"))
              .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg12"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm12", "Demon Spell"))
              .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg13"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm13", "Trickster Spell"))
              .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg7"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm7", "Mergable Spellbooks"))
              .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg8"), defaultValue: true, Helpers.CreateString("toggle-desc-mmm8", "Restore Owlcat Merger"))
              .ShowVisualConnection()));

                try
        {
          if (Initialized)
          {
            Logger.Info("Already configured blueprints.");
            return;
          }
          Initialized = true;

          Logger.Info("Configuring blueprints.");

          //MyFeat.Configure();
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg2"))) { LichFeature.UndeadMount1Configure(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg3"))) { LichFeature.UndeadMount2Configure(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg4"))) { LichFeature.UnholyFortitudeConfigure(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg6"))) { AngelFeature.NewHalo1Configure(); AngelFeature.NewHalo2Configure(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg10"))) { AzataFeature.NewSuperp1Configure(); }
                }
        catch (Exception e)
        {
          Logger.Error("Failed to configure blueprints.", e);
        }
      }
    }

    [HarmonyPatch(typeof(StartGameLoader))]
    static class StartGameLoader_Patch
    {
      private static bool Initialized = false;

      [HarmonyPatch(nameof(StartGameLoader.LoadPackTOC)), HarmonyPostfix]
      static void LoadPackTOC()
      {
        try
        {
          if (Initialized)
          {
            Logger.Info("Already configured delayed blueprints.");
            return;
          }
          Initialized = true;

                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg7"))) { MergableSpellbooks.Patch(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg1"))) { LichSpell.Patch(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg5"))) { AngelSpell.Patch(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg9"))) { AzataSpellTweak.Patch(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg11"))) { AeonSpellTweak.Patch(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg12"))) { DemonSpellTweak.Patch(); }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg13"))) { TricksterSpellTweak.Patch(); }
                    RootConfigurator.ConfigureDelayedBlueprints();
        }
        catch (Exception e)
        {
          Logger.Error("Failed to configure delayed blueprints.", e);
        }
      }
    }
  }
}
