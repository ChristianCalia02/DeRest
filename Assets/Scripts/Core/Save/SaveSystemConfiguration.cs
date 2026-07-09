using System.IO;
using UnityEngine;

namespace Core
{
    public static class SaveSystemConfiguration
    {
        private const string SettingsFolder = "Settings";
        private const string GameDataFolder = "Data";
        private const string SettingsFileName = "Settings.json";
        private const string GameDataPrefix = "GameData_";
        private const string GameDataExtension = ".json";
        private const int GameDataSlotsNumberValue = 3;

        public static string RootPath => Application.persistentDataPath;
        public static string SettingsFolderPath => Path.Combine(RootPath, SettingsFolder);
        public static string SettingsPath => Path.Combine(SettingsFolderPath, SettingsFileName);
        public static string GameDataFolderPath => Path.Combine(RootPath, GameDataFolder);
        public static int GameDataSlotsNumber => GameDataSlotsNumberValue;

        public static string GetGameDataPath(int slot) =>
            Path.Combine(GameDataFolderPath, $"{GameDataPrefix}{slot}{GameDataExtension}");
    }
}