using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class PlayerTransformData : ISavableDataClass
    {
        [SerializeField] private Vector3 position;
        [SerializeField] private float yaw;

        [SerializeField] private float savedFileVersion;

        public float CurrentFileVersion => 1.0f;
        public float SavedFileVersion => savedFileVersion;
        public object InstanceToSave => this;

        public Vector3 Position => position;
        public float Yaw => yaw;

        public void OnCreation()
        {
            savedFileVersion = CurrentFileVersion;
            position = Vector3.zero;
            yaw = 0f;
        }

        public void OnPreSave()
        {
            if (PlayerPositionRegistry.Active == null) return;
            position = PlayerPositionRegistry.Active.Position;
            yaw = PlayerPositionRegistry.Active.Yaw;
        }

        public void OnLoadedFromDisk() { }
        public void OnDataSelected() { }
        public void OnDataDeselected() { }
        public void OnPostSave() { }
        public void OnDelete() { }

        public bool CheckVersion() => CurrentFileVersion == SavedFileVersion;
        public void HandleVersionChanged() => savedFileVersion = CurrentFileVersion;
    }
}