using UnityEngine;

public abstract class PersistenceDataSO : ScriptableObject, IPersistenceData
{
    public delegate void SaveDataSuccessEvent();
    public SaveDataSuccessEvent OnSaveDataSuccess { get; set; }
    public delegate void LoadDataSuccessEvent();
    public LoadDataSuccessEvent OnLoadDataSuccess { get; set; }
    public abstract void SaveData();

    public abstract void LoadData();
}