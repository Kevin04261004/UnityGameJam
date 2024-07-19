using DYLib;
using TMPro;
using UnityEngine;

public class KeySettingHandler : MonoBehaviour
{
    [SerializeField] private InputDataSO _inputDataSO;

    [SerializeField]
    private SerializableDictionary<EKeyType, TextMeshProUGUI> keyDictionary =
        new SerializableDictionary<EKeyType, TextMeshProUGUI>();

    private EKeyType pressedKeyType = EKeyType.Size;
    public void Awake()
    {
        _inputDataSO.OnLoadDataSuccess += SetKeyDictionaryPanel;
        _inputDataSO.OnSaveDataSuccess += SetKeyDictionaryPanel;
    }

    private void SetKeyDictionaryPanel()
    {
        foreach (var pair in _inputDataSO._inputDictionary)
        {
            keyDictionary[pair.Key].text = GetKeyCodeString(pair.Value);
        }
    }

    public void SetKey(EKeyType keyType, KeyCode keyCode)
    {
        /* Key Setting후 바로 파일에 쓰고, 파일에 써지면, Delegate로 적용. */
        _inputDataSO.SetKeyCode(keyType, keyCode);
        _inputDataSO.SaveData();
        keyDictionary[keyType].color = Color.black;
    }

    private void OnGUI()
    {
        if (pressedKeyType != EKeyType.Size && Event.current.isKey)
        {
            KeyCode keyCode = Event.current.keyCode;
            SetKey(pressedKeyType, keyCode);
            pressedKeyType = EKeyType.Size;
        }
    }
    
    public void ListenNextKey(int keyType)
    {
        if (pressedKeyType != EKeyType.Size)
        {
            keyDictionary[(EKeyType)keyType].color = Color.black;
        }
        pressedKeyType = (EKeyType)keyType;
    }

    public void SetTMPColorRed(TextMeshProUGUI tmp)
    {
        tmp.color = Color.red;
    }

    private string GetKeyCodeString(KeyCode keycode)
    {
        string str = keycode.ToString();
        switch (str)
        {
            case "Return":
                str = "Enter";
                break;
            case "Escape":
                str = "Esc";
                break;
            case "Alpha0":
                str = "0";
                break;
            case "Alpha1":
                str = "1";
                break;
            case "Alpha2":
                str = "2";
                break;
            case "Alpha3":
                str = "3";
                break;
            case "Alpha4":
                str = "4";
                break;
            case "Alpha5":
                str = "5";
                break;
            case "Alpha6":
                str = "6";
                break;
            case "Alpha7":
                str = "7";
                break;
            case "Alpha8":
                str = "8";
                break;
            case "Alpha9":
                str = "9";
                break;
            case "Delete":
                str = "del";
                break;
            case "KeypadPeriod":
                str = ".";
                break;
            case "KeypadDivide":
                str = "/";
                break;
            case "KeypadMultiply":
                str = "*";
                break;
            case "KeypadMinus":
                str = "-";
                break;
            case "KeypadPlus":
                str = "+";
                break;
            case "KeypadEnter":
                str = "Enter";
                break;
            case "KeypadEquals":
                str = "=";
                break;
            case "Insert":
                str = "Ins";
                break;
            case "Exclaim":
                str = "Exc";
                break;
            case "DoubleQuote":
                str = "\"";
                break;
            case "Ampersand":
                str = "&";
                break;
            case "Quote":
                str = "\'";
                break;
            case "Asterisk":
                str = "*";
                break;
            case "Plus":
                str = "+";
                break;
            case "Comma":
                str = ",";
                break;
            case "Minus":
                str = "-";
                break;
            case "Period":
                str = ".";
                break;
            case "Slash":
                str = "/";
                break;
            case "Colon":
                str = ":";
                break;
            case "SemiColon":
                str = ";";
                break;
            case "Less":
                str = "<";
                break;
            case "Equals":
                str = "=";
                break;
            case "Greater":
                str = ">";
                break;
            case "Question":
                str = "?";
                break;
            case "At":
                str = "@";
                break;
            case "LeftBracket":
                str = "[";
                break;
            case "Backslash":
                str = "\\";
                break;
            case "RightBracket":
                str = "\"";
                break;
            case "Underscore":
                str = "_";
                break;
            case "BackQuote":
                str = "`";
                break;
            default:
                break;
        }

        return str;
    }
}
