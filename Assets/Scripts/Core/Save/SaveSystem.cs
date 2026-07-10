using System.IO;
using UnityEngine;

namespace Core
{
    public static class SaveSystem
    {
        public static SettingsSavedData SettingsData { get; private set; }
        public static GameSavedData ActiveGameData { get; private set; }
        public static GameSavedData[] AllSlots { get; private set; }

        private static int currentSlotIndex = -1;


        public static void Initialize()
        {
            Directory.CreateDirectory(SaveSystemConfiguration.SettingsFolderPath);
            Directory.CreateDirectory(SaveSystemConfiguration.GameDataFolderPath);

            LoadSettings();
            LoadAllSlots();

            EventBus.Subscribe<LoadGameRequestedEvent>(OnLoadGameRequested);
            EventBus.Subscribe<SaveGameEvent>(OnSaveGame);
        }

        #region Settings
        public static void LoadSettings() =>
            SettingsData = LoadFromFile<SettingsSavedData>(SaveSystemConfiguration.SettingsPath);

        public static void SaveSettings() =>
            SaveToFile(SaveSystemConfiguration.SettingsPath, SettingsData);
        #endregion

        #region Slot
        public static void LoadAllSlots()
        {
            AllSlots = new GameSavedData[SaveSystemConfiguration.GameDataSlotsNumber];
            for (int i = 0; i < AllSlots.Length; i++)
            {
                string path = SaveSystemConfiguration.GetGameDataPath(i);
                AllSlots[i] = File.Exists(path) ? LoadFromFile<GameSavedData>(path) : null;
            }
        }

        public static bool SlotExists(int slotIndex) =>
            File.Exists(SaveSystemConfiguration.GetGameDataPath(slotIndex));

        public static void CreateSlot(int slotIndex)
        {
            var data = new GameSavedData();
            data.OnCreation();
            AllSlots[slotIndex] = data;
            SaveToFile(SaveSystemConfiguration.GetGameDataPath(slotIndex), data);
        }

        public static void DeleteSlot(int slotIndex)
        {
            AllSlots[slotIndex]?.OnDelete();

            string path = SaveSystemConfiguration.GetGameDataPath(slotIndex);
            if (File.Exists(path)) File.Delete(path);

            AllSlots[slotIndex] = null;
        }

        public static void SetActiveSlot(int slotIndex)
        {
            ActiveGameData?.OnDataDeselected();

            if (slotIndex < 0)
            {
                ActiveGameData = null;
                currentSlotIndex = -1;
                return;
            }

            if (AllSlots[slotIndex] == null)
                CreateSlot(slotIndex); //new slot game creation

            ActiveGameData = AllSlots[slotIndex];
            ActiveGameData.OnDataSelected();
            currentSlotIndex = slotIndex;
        }

        public static void SaveActiveSlot()
        {
            if (ActiveGameData == null || currentSlotIndex < 0) return;
            SaveToFile(SaveSystemConfiguration.GetGameDataPath(currentSlotIndex), ActiveGameData);
        }

        public static System.DateTime? GetSlotLastWriteTime(int slotIndex)
        {
            string path = SaveSystemConfiguration.GetGameDataPath(slotIndex);
            return File.Exists(path) ? File.GetLastWriteTime(path) : (System.DateTime?)null;
        }
        #endregion

        #region Eventi
        private static void OnLoadGameRequested(LoadGameRequestedEvent e) => SetActiveSlot(e.SlotIndex);
        private static void OnSaveGame(SaveGameEvent e) => SaveActiveSlot();
        #endregion

        #region JSON
        private static void SaveToFile(string path, ISavableDataClass data)
        {
            data.OnPreSave();
            string json = JsonUtility.ToJson(data.InstanceToSave, prettyPrint: true);
            File.WriteAllText(path, json);
            data.OnPostSave();
        }

        private static T LoadFromFile<T>(string path) where T : class, ISavableDataClass, new()
        {
            if (!File.Exists(path))
                return CreateFresh<T>();

            string json = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(json))
                return CreateFresh<T>();

            T data = JsonUtility.FromJson<T>(json);
            if (data == null)
                return CreateFresh<T>();

            if (!data.CheckVersion())
                data.HandleVersionChanged();

            data.OnLoadedFromDisk();
            return data;
        }

        private static T CreateFresh<T>() where T : class, ISavableDataClass, new()
        {
            var data = new T();
            data.OnCreation();
            return data;
        }
        #endregion
    }
}