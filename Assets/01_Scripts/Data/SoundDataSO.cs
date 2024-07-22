using System;
using System.Collections.Generic;
using DYLib;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SoundDataSO", menuName = "Scriptable Object/Sound Data SO", order = 1)]
public class SoundDataSO : PersistenceDataSO
{
    [Serializable]
    public class SoundVolumeData
    {
        public string GroupName = String.Empty;
        public float volume = 1; // 0 ~ 1
    }

    [SerializeField] private List<SoundVolumeData> soundVolumeDataList = new List<SoundVolumeData>(3);
    public void SetVolume(string groupName, float volume)
    {
        if (TryGetSoundVolumeData(groupName, out SoundVolumeData soundVolumeData))
        {
            soundVolumeData.volume = volume;
        }
    }

    public float GetVolume(string groupName)
    {
        if (TryGetSoundVolumeData(groupName, out SoundVolumeData soundVolumeData))
        {
            return soundVolumeData.volume;
        }
        else
        {
            return 1;
        }
    }

    private bool TryGetSoundVolumeData(string groupName, out SoundVolumeData soundVolumeData)
    {
        soundVolumeData = null;
        
        foreach (var data in soundVolumeDataList)
        {
            if (data.GroupName == groupName)
            {
                soundVolumeData = data;
                return true;
            }
        }
        
        return false;
    }
    
    #region Data I/O using Json

    [Serializable]
    private class SoundVolumeDataListWrapper // List를 감싸기 위한 클래스
    {
        public List<SoundVolumeData> list;
    }
    
    private static readonly string SOUND_DATA_JSON_RELATIVE_PATH = "soundData.json";
    
    [ContextMenu("SaveSoundDataToJson")]
    public override void SaveData()
    {
        SoundVolumeDataListWrapper wrapper = new SoundVolumeDataListWrapper();
        wrapper.list = soundVolumeDataList;
        string jsonData = JsonUtility.ToJson(wrapper);
        Debugger.Log("JsonData", $"{jsonData}");
        if (FileHandler.TryWrite(SOUND_DATA_JSON_RELATIVE_PATH, jsonData.SerializeToBytes()))
        {
            Debugger.Log("Save Input Data", "Sound Data를 Json에 적는데 성공했습니다.", EColor.green);
        }
        else
        {
            Debugger.Log("Save Input Data", "Sound Data를 Json에 적는데 실패했습니다.", EColor.red);
        }
        OnSaveDataSuccess?.Invoke();
    }
    [ContextMenu("LoadSoundDataFromJson")]
    public override void LoadData()
    {
        if (FileHandler.TryRead(SOUND_DATA_JSON_RELATIVE_PATH, out byte[] data))
        {
            try
            {
                string jsonData = data.DeserializeToString();
                Debugger.Log("JsonData", $"{jsonData}");
                SoundVolumeDataListWrapper wrapper = JsonUtility.FromJson<SoundVolumeDataListWrapper>(jsonData);
                foreach (var soundData in wrapper.list)
                {
                    SetVolume(soundData.GroupName, soundData.volume);
                }
            }
            catch (Exception ex)
            {
                Debugger.LogError($"{ex.Message}");
                return;
            }   
            Debugger.Log("Load Input Data", "Sound Data를 Json으로 부터 읽어들이는데 성공했습니다.", EColor.green);
        }
        else
        {
            Debugger.Log("Load Input Data", "SoundData가 존재하지 않습니다. 기본 세팅으로 진행합니다.", EColor.purple);
        }
        OnLoadDataSuccess?.Invoke();
    }
    #endregion
}