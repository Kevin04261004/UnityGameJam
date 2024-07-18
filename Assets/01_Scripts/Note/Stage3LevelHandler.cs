using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage3LevelHandler : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> OnLevelStart;
    [SerializeField] private List<GameObject> levelList;
    private Coroutine coroutine;
    private WaitForSeconds oneWFS = new WaitForSeconds(1f);
    
    [ContextMenu("Level 1")]
    private void Level1()
    {
        StartLevel(1);
    }
    [ContextMenu("Level 2")]
    private void Level2()
    {
        StartLevel(2);
    }
    [ContextMenu("Level 3")]
    private void Level3()
    {
        StartLevel(3);
    }

    public void StartLevel(int level)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(LevelSettingRoutine(level));
    }
    public IEnumerator LevelSettingRoutine(int level)
    {
        yield return SceneHandler.Instance.FadeOut();
        level--;
        Debug.Assert(level < levelList.Count);
        for (int i = 0; i < levelList.Count; ++i)
        {
            levelList[i].SetActive(false);
        }
        levelList[level].SetActive(true);
        OnLevelStart[level].Invoke();
        yield return oneWFS;
        yield return SceneHandler.Instance.FadeIn();
    }

    public void StageClear()
    {
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.Stage4);
    }
}
