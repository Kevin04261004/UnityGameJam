using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private TextMeshProUGUI TMP;
    [SerializeField] private float timeForReadOneChar;
    private WaitForSeconds timeForReadOneCharWFS;
    private Coroutine coroutine;
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
        
        Debug.Assert(TMP != null, "TMP 가 NULL입니다.");
        timeForReadOneCharWFS = new WaitForSeconds(timeForReadOneChar);
    }

    public void SetPanel(string str, Color color = default, bool typingMode = true, float durationTime = 2f)
    {
        if (color == default)
        {
            color = Color.white;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(ReadDialogueRoutine(str, color, typingMode, durationTime));
    }

    private IEnumerator ReadDialogueRoutine(string str, Color color, bool typingMode, float durationTime)
    {
        // TODO: 한 문자씩 적기.
        // TODO: 모든 문장을 적으면 durationTime이 지나고 alpha값 0으로.

        TMP.color = color;
        if (typingMode)
        {
            int count = str.Length;

            for (int i = 0; i < count; ++i)
            {
                string curStr = str.Substring(0, i);
                TMP.text = curStr;
                yield return timeForReadOneCharWFS;
            }

            TMP.text = str;
        }
        TMP.text = str;
        yield return new WaitForSeconds(durationTime);
        TMP.color = Color.clear;
    }
}
