using System;
using DYLib;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "Scriptable Object/Stage Data SO", order = 1)]
public class StageDataSO : PersistenceDataSO
{
    [field:SerializeField] public string curStage { get; set; }

    private static readonly string STAGE_DATA_JSON_RELATIVE_PATH = "stageData.json";

    [ContextMenu("SaveStageDataToJson")]
    public override void SaveData()
    {
        string str = curStage;
        Debugger.Log("Data", $"{str}");
        if (FileHandler.TryWrite(STAGE_DATA_JSON_RELATIVE_PATH, str.SerializeToBytes()))
        {
            Debugger.Log("Save Stage Data", "Stage Data를 바이너리로 적는데 성공했습니다.", EColor.green);
        }
        else
        {
            Debugger.Log("Save Stage Data", "Stage Data를 바이너리로 적는데 실패했습니다.", EColor.red);
        }
        OnSaveDataSuccess?.Invoke();
    }
    [ContextMenu("LoadStageDataFromJson")]
    public override void LoadData()
    {
        if (FileHandler.TryRead(STAGE_DATA_JSON_RELATIVE_PATH, out byte[] data))
        {
            try
            {
                string strData = data.DeserializeToString();
                Debugger.Log("JsonData", $"{strData}");
                curStage = strData;
            }
            catch (Exception ex)
            {
                Debugger.LogError($"{ex.Message}");
                return;
            }   
            Debugger.Log("Load Stage Data", "Stage Data를 바이너리 파일로 부터 읽어들이는데 성공했습니다.", EColor.green);
        }
        else
        {
            Debugger.Log("Load Stage Data", "StageData가 존재하지 않습니다. 기본 세팅으로 진행합니다.", EColor.purple);
        }   
        OnLoadDataSuccess?.Invoke();
    }
}
