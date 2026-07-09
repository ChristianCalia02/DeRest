using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GameSavedData : ISavableDataClass
    {
        [SerializeReference]
        private List<ISavableDataClass> savableDatas = new();

        [SerializeField] private float savedFileVersion;

        public float CurrentFileVersion => 1.0f;
        public float SavedFileVersion => savedFileVersion;
        public object InstanceToSave => this;

        public T GetData<T>() where T : class, ISavableDataClass, new()
        {
            foreach (var d in savableDatas)
                if (d is T typed) return typed;

            var created = new T();
            created.OnCreation();
            savableDatas.Add(created);
            return created;
        }

        public void OnCreation()
        {
            savableDatas = new List<ISavableDataClass>();
            savedFileVersion = CurrentFileVersion;
        }

        public void OnLoadedFromDisk()
        {
            savableDatas ??= new List<ISavableDataClass>();
            foreach (var data in savableDatas)
                data.OnLoadedFromDisk();
        }

        public void OnDataSelected()
        {
            foreach (var data in savableDatas)
                data.OnDataSelected();
        }

        public void OnDataDeselected()
        {
            foreach (var data in savableDatas)
                data.OnDataDeselected();
        }

        public void OnPreSave()
        {
            foreach (var data in savableDatas)
                data.OnPreSave();
        }

        public void OnPostSave()
        {
            foreach (var data in savableDatas)
                data.OnPostSave();
        }

        public void OnDelete()
        {
            foreach (var data in savableDatas)
                data.OnDelete();
        }

        public bool CheckVersion() => CurrentFileVersion == SavedFileVersion;
        public void HandleVersionChanged() => savedFileVersion = CurrentFileVersion;
    }
}