namespace Core
{
    public interface ISavableDataClass
    {
        float CurrentFileVersion { get; }
        float SavedFileVersion { get; }
        object InstanceToSave { get; }

        bool CheckVersion();
        void HandleVersionChanged();
        void OnCreation();
        void OnLoadedFromDisk();
        void OnPreSave();
        void OnPostSave();
        void OnDelete();
        void OnDataSelected();
        void OnDataDeselected();
    }
}