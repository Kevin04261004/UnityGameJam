using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public enum EGameType
    {
        Start,
        Title,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }
    public static SceneHandler Instance { get; private set; }
    public static readonly string TitleScene = "TitleScene";
    public static readonly string CharacterScene = "CharacterScene";
    public static readonly string Stage1 = "Stage1";
    public static readonly string Stage2 = "Stage2";
    public static readonly string Stage3 = "Stage3";
    public static readonly string Stage4 = "Stage4";
    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public EGameType GameType { get; private set; } = EGameType.Start;
    
    /* Debug */

    [ContextMenu("Stage 1")]
    private void StartStage1()
    {
        LoadSceneWithFade(Stage1);
    }
    [ContextMenu("Stage 2")]
    private void StartStage2()
    {
        LoadSceneWithFade(Stage2);
    }
    [ContextMenu("Stage 3")]
    private void StartStage3()
    {
        LoadSceneWithFade(Stage3);
    }
    [ContextMenu("Stage 4")]
    private void StartStage4()
    {
        LoadSceneWithFade(Stage4);
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InitSceneInfo();
        if (SceneManager.sceneCount != 1)
        {
            return;
        }
        LoadSceneWithFade(TitleScene);
    }
    
    private void InitSceneInfo()
    {
        loadScenes.Add(TitleScene, LoadSceneMode.Additive);
        loadScenes.Add(CharacterScene, LoadSceneMode.Additive);
        loadScenes.Add(Stage1, LoadSceneMode.Additive);
        loadScenes.Add(Stage2, LoadSceneMode.Additive);
        loadScenes.Add(Stage3, LoadSceneMode.Additive);
        loadScenes.Add(Stage4, LoadSceneMode.Additive);
    }

    public void LoadSceneWithFade(string sceneName)
    {
        if (loadScenes.ContainsKey(sceneName))
        {
            StartCoroutine(LoadSceneFadeRoutine(sceneName, loadScenes[sceneName]));
        }
        else
        {
            Debug.LogError($"Scene {sceneName} not found in loadScenes dictionary");
        }
    }

    public void LoadScene(string sceneName)
    {
        if (loadScenes.ContainsKey(sceneName))
        {
            StartCoroutine(LoadSceneRoutine(sceneName, loadScenes[sceneName]));
        }
        else
        {
            Debug.LogError($"Scene {sceneName} not found in loadScenes dictionary");
        }
    }
    private IEnumerator LoadSceneFadeRoutine(string sceneName, LoadSceneMode mode)
    {
        yield return StartCoroutine(FadeOut());

        yield return StartCoroutine(LoadSceneRoutine(sceneName, mode));

        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator LoadSceneRoutine(string sceneName, LoadSceneMode mode)
    {
        if (sceneName == TitleScene)
        {
            GameType = EGameType.Title;
        }
        else
        {
            if (IsSceneLoaded(TitleScene))
            {
                yield return SceneManager.UnloadSceneAsync(TitleScene);
            }
        }
        
        if (sceneName == Stage1)
        {
            GameType = EGameType.Stage1;
            if (!IsSceneLoaded(CharacterScene))
            {
                SceneManager.LoadSceneAsync(CharacterScene, mode);
            }
        }
        else
        {
            if (IsSceneLoaded(Stage1))
            {
                yield return SceneManager.UnloadSceneAsync(Stage1);
            }
        }
        if (sceneName == Stage2)
        {
            GameType = EGameType.Stage2;
            if (!IsSceneLoaded(CharacterScene))
            {
                SceneManager.LoadSceneAsync(CharacterScene, mode);
            }
        }
        else
        {
            if (IsSceneLoaded(Stage2))
            {
                yield return SceneManager.UnloadSceneAsync(Stage2);
            }
        }
        if (sceneName == Stage3)
        {
            GameType = EGameType.Stage3;
            if (!IsSceneLoaded(CharacterScene))
            {
                SceneManager.LoadSceneAsync(CharacterScene, mode);
            }
        }
        else
        {
            if (IsSceneLoaded(Stage3))
            {
                yield return SceneManager.UnloadSceneAsync(Stage3);
            }
        }
        if (sceneName == Stage4)
        {
            GameType = EGameType.Stage4;
            if (!IsSceneLoaded(CharacterScene))
            {
                SceneManager.LoadSceneAsync(CharacterScene, mode);
            }
        }
        else
        {
            if (IsSceneLoaded(Stage4))
            {
                yield return SceneManager.UnloadSceneAsync(Stage4);
            }
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, mode);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }

        return false;
    }
    private IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color tempColor = fadeImage.color;
        fadeImage.raycastTarget = true;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = tempColor;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color tempColor = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            fadeImage.color = tempColor;
            yield return null;
        }

        fadeImage.raycastTarget = false;
    }
}
