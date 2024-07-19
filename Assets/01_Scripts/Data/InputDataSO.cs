
using System;
using System.Collections.Generic;
using DYLib;
using UnityEngine;

[CreateAssetMenu(fileName = "InputDataSO", menuName = "Scriptable Object/Input Data SO", order = 1)]
public class InputDataSO : PersistenceDataSO
{
        [field: SerializeField] public SerializableDictionary<EKeyType, KeyCode> _inputDictionary { get; private set; }
        public InputDataSO()
        {
            _inputDictionary = new SerializableDictionary<EKeyType, KeyCode>();
        }

        #region Indexer

        public KeyCode this[EKeyType keyType]
        {
            get
            {
                Debug.Assert(keyType != EKeyType.Size);
                Debug.Assert(_inputDictionary.Contains(keyType), "_inputDictionary.Contains(keyType)");
                return _inputDictionary[keyType];
            }
        }

        #endregion
        
        // OnEnable()과 OnDisable()이 계속 디버깅모드, 즉 에디터 모드에서도 계속해서 실행이 되는 이상한 버그가 있어서,
        // IDataPersistence 인터페이스로 빼놓아, 모든 IDataPersistence들을 시작 및 종료에 부르고 저장해주는 DataPersistenceManager를 제작함.
        private void OnEnable()
        {
            Debugger.Assert(_inputDictionary.Count == (int)EKeyType.Size, "Input Data SO에서 모든 Key를 설정해주세요.");
        }
        public void SetKeyCode(EKeyType keyType, KeyCode keyCode)
        {
            Debugger.Assert(_inputDictionary.Contains(keyType), "InputData SO 세팅이 필요합니다.");
            _inputDictionary[keyType] = keyCode;
        }

        #region Data I/O using Json

        private static readonly string INPUT_DATA_JSON_RELATIVE_PATH = "inputData.json";
        
        [Serializable]
        private class KeyValueListWrapper // List를 감싸기 위한 클래스
        {
            public List<KeyValueItem> keyValuePairs;
        }
        [Serializable]
        private class KeyValueItem // KeyValuePair 대체 클래스
        {
            public EKeyType Key;
            public KeyCode Value;
        }
        [ContextMenu("SaveInputDataToJson")]
        public override void SaveData()
        {
            List<KeyValueItem> KeyTypeCodePairList = new List<KeyValueItem>();

            foreach (var kvp in _inputDictionary)
            {
                KeyTypeCodePairList.Add(new KeyValueItem { Key = kvp.Key, Value = kvp.Value });
                Debugger.Log($"{kvp.Key}, {kvp.Value}");
            }

            string jsonData = JsonUtility.ToJson(new KeyValueListWrapper { keyValuePairs = KeyTypeCodePairList });
            Debugger.Log("JsonData", $"{jsonData}");
            if (FileHandler.TryWrite(INPUT_DATA_JSON_RELATIVE_PATH, jsonData.SerializeToBytes()))
            {
                Debugger.Log("Save Input Data", "Input Data를 Json에 적는데 성공했습니다.", EColor.green);
                OnSaveDataSuccess?.Invoke();
            }
            else
            {
                Debugger.Log("Save Input Data", "Input Data를 Json에 적는데 실패했습니다.", EColor.red);
            }
        }
        [ContextMenu("LoadInputDataFromJson")]
        public override void LoadData()
        {
            if (FileHandler.TryRead(INPUT_DATA_JSON_RELATIVE_PATH, out byte[] data))
            {
                try
                {
                    string jsonData = data.DeserializeToString();
                    Debugger.Log("JsonData", $"{jsonData}");
                    KeyValueListWrapper wrapper = JsonUtility.FromJson<KeyValueListWrapper>(jsonData);
                    foreach (var kvp in wrapper.keyValuePairs)
                    {
                        SetKeyCode(kvp.Key, kvp.Value);   
                    }
                }
                catch (Exception ex)
                {
                    Debugger.LogError($"{ex.Message}");
                    return;
                }   
                Debugger.Log("Load Input Data", "Input Data를 Json으로 부터 읽어들이는데 성공했습니다.", EColor.green);
                OnLoadDataSuccess?.Invoke();
            }
            else
            {
                Debugger.Log("Load Input Data", "InputData가 존재하지 않습니다. 기본 세팅으로 진행합니다.", EColor.purple);
            }
        }
        #endregion
}