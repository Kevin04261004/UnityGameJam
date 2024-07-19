using System;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance;
    private List<IPersistenceData> persistenceDataList = new List<IPersistenceData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        FindScripatbleObjectPersistenceData();
    }

    private void Start()
    {
        LoadAllDatas();
    }

    private void LoadAllDatas()
    {
        foreach (var persistenceData in persistenceDataList)
        {
            LoadData(persistenceData);
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var persistenceData in persistenceDataList)
        {
            SaveData(persistenceData);
        }
    }
    private void LoadData(IPersistenceData data)
    {
        data.LoadData();
    }

    private void SaveData(IPersistenceData data)
    {
        data.SaveData();
    }

    private void FindScripatbleObjectPersistenceData()
    {
        var scriptableObjects = Resources.FindObjectsOfTypeAll<PersistenceDataSO>();

        foreach (var so in scriptableObjects)
        {
            if (so is IPersistenceData persistenceData && !persistenceDataList.Contains(so))
            {
                persistenceDataList.Add(so);
            }
        }
    }
}
