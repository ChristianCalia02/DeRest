using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class SettingsSavedData : ISavableDataClass
    {
        public float masterVolume = 1f;
        public bool vibrationEnabled = true;
        public bool notificationsEnabled = true;

        [SerializeField] private float savedFileVersion;

        public float CurrentFileVersion => 1.0f;
        public float SavedFileVersion => savedFileVersion;
        public object InstanceToSave => this;

        public void OnCreation()
        {
            savedFileVersion = CurrentFileVersion;
            masterVolume = 1f;
            vibrationEnabled = true;
            notificationsEnabled = true;
        }

        public void OnDataSelected() => Apply();
        public void OnPreSave() { }
        public void OnLoadedFromDisk() { }
        public void OnPostSave() { }
        public void OnDelete() { }
        public void OnDataDeselected() { }

        public bool CheckVersion() => CurrentFileVersion == SavedFileVersion;
        public void HandleVersionChanged() => savedFileVersion = CurrentFileVersion;

        public void Apply()
        {
            AudioListener.volume = masterVolume;
        }
    }
}