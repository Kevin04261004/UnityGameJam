using UnityEngine;

public abstract class PersistenceDataSO : ScriptableObject, IPersistenceData
{
    public abstract void SaveData();

    public abstract void LoadData();
}