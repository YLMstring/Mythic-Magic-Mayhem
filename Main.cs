using MythicMagicMayhem.Feats;
using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using UnityModManagerNet;
using Epic.OnlineServices;
using ModMenu.Settings;
using System.Globalization;
using MythicMagicMayhem.Menu;
using System.Text;
using MythicMagicMayhem.Mechanics;
using MythicMagicMayhem.Angel;
using MythicMagicMayhem.Lich;

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
        private static void onclick()
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
                SettingsBuilder.New(RootKey, Helpers.CreateString("title", "Mythic Magic Mayhem"))
                .AddDefaultButton().AddButton(Button.New(
            Helpers.CreateString("button-desc", "Restart the game to apply changes!"), Helpers.CreateString("button-text", "Do Not Turn Any Chosen Features Off"), onclick))
          .AddToggle(
            Toggle.New(GetKey("tg1"), defaultValue: true, Helpers.CreateString("toggle-desc1", "Lich Spell"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg2"), defaultValue: true, Helpers.CreateString("toggle-desc2", "Lich Mount"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg3"), defaultValue: true, Helpers.CreateString("toggle-desc3", "Lich Companion Feat"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg4"), defaultValue: true, Helpers.CreateString("toggle-desc4", "Lich Fortitude"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg5"), defaultValue: true, Helpers.CreateString("toggle-desc5", "Angel Spell"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg6"), defaultValue: true, Helpers.CreateString("toggle-desc6", "Angel Halo"))
                .ShowVisualConnection())
          .AddToggle(
            Toggle.New(GetKey("tg7"), defaultValue: true, Helpers.CreateString("toggle-desc7", "Mergable Spellbooks"))
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
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg2"))) {  }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg3"))) {  }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg4"))) { }
                    if (ModMenu.ModMenu.GetSettingValue<bool>(GetKey("tg6"))) { }
                    

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
