using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
    public static readonly string TitleScene = "TitleScene";
    public static readonly string CharacterScene = "CharacterScene";
    public static readonly string Stage1 = "Stage1";
    public static readonly string Stage2 = "Stage2";
    public static readonly string Stage3 = "Stage3";
    public static readonly string Stage4 = "Stage4";
    public static readonly string EndScene = "EndScene";
    public AudioListener audioListener;
    public static readonly string Bridge1 = "Bridge1";
    public static readonly string Bridge2 = "Bridge2";
    public static readonly string Bridge3 = "Bridge3";
    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();
    // public delegate void SceneLoadedEvent();
    // public Dictionary<string, SceneLoadedEvent> sceneLoadedEvents = new Dictionary<string, SceneLoadedEvent>();
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    [field:SerializeField] public StageDataSO _stageDataSO { get; private set; }
    
    #region Debug
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

    public void ReLoadCurStage()
    {
        if (IsSceneLoaded(_stageDataSO.curStage))
        {
            SceneManager.UnloadSceneAsync(_stageDataSO.curStage);
            LoadSceneWithFade(_stageDataSO.curStage);
        }
    }

    public void ContinueStage()
    {
        if (_stageDataSO.curStage != Stage1)
        {
            LoadSceneWithFade(_stageDataSO.curStage);
        }
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartStage1();
        }
        // else if (Input.GetKeyDown(KeyCode.Keypad2))
        // {
        //     StartStage2();
        // }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            StartStage3();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            StartStage4();
        }
    }

    #endregion

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
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        InitSceneInfo();
        OnSceneLoaded(new Scene(), LoadSceneMode.Additive);
        if (SceneManager.sceneCount != 1)
        {
            if (IsSceneLoaded(CharacterScene))
            {
                audioListener.enabled = false;
            }
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
        loadScenes.Add(EndScene, LoadSceneMode.Additive);
        loadScenes.Add(Bridge1, LoadSceneMode.Additive);
        loadScenes.Add(Bridge2, LoadSceneMode.Additive);
        loadScenes.Add(Bridge3, LoadSceneMode.Additive);
        
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
            GameManager.Instance.GameType = GameManager.EGameType.Title;
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
            _stageDataSO.curStage = Stage1;
            GameManager.Instance.GameType = GameManager.EGameType.Stage1;
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
            _stageDataSO.curStage = Stage2;
            GameManager.Instance.GameType = GameManager.EGameType.Stage2;

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
            _stageDataSO.curStage = Stage3;
            GameManager.Instance.GameType = GameManager.EGameType.Stage3;

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
            _stageDataSO.curStage = Stage4;
            GameManager.Instance.GameType = GameManager.EGameType.Stage4;

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

        if (sceneName == EndScene)
        {
            yield return SceneManager.UnloadSceneAsync(CharacterScene);
        }
        else
        {
            if (IsSceneLoaded(EndScene))
            {
                yield return SceneManager.UnloadSceneAsync(EndScene);
            }
        }
        
        if (sceneName == Bridge1)
        {
            _stageDataSO.curStage = Bridge1;
            GameManager.Instance.GameType = GameManager.EGameType.Bridge1;
            if (!IsSceneLoaded(CharacterScene))
            {
                SceneManager.LoadSceneAsync(CharacterScene, mode);
            }
        }
        else
        {
            if (IsSceneLoaded(Bridge1))
            {
                yield return SceneManager.UnloadSceneAsync(Bridge1);
            }
        }
        
        if (sceneName == Bridge2)
        {
            _stageDataSO.curStage = Bridge2;
            GameManager.Instance.GameType = GameManager.EGameType.Bridge2;
            if (!IsSceneLoaded(CharacterScene))
            {
                SceneManager.LoadSceneAsync(CharacterScene, mode);
            }
        }
        else
        {
            if (IsSceneLoaded(Bridge2))
            {
                yield return SceneManager.UnloadSceneAsync(Bridge2);
            }
        }
        
        if (sceneName == Bridge3)
        {
            _stageDataSO.curStage = Bridge3;
            GameManager.Instance.GameType = GameManager.EGameType.Bridge3;
            if (!IsSceneLoaded(CharacterScene))
            {
                SceneManager.LoadSceneAsync(CharacterScene, mode);
            }
        }
        else
        {
            if (IsSceneLoaded(Bridge3))
            {
                yield return SceneManager.UnloadSceneAsync(Bridge3);
            }
        }
        

        if (IsSceneLoaded(sceneName))
        {
            yield break;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, mode);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioListener.enabled = (!IsSceneLoaded(CharacterScene));

        if (TryGetObjectFromScene(CharacterScene, out PlayerHandler playerHandler))
        {
            /* spawnPoint */
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            if (spawnPoint != null)
            {
                playerHandler.SpawnToPoint(spawnPoint.transform.position);
            }


            /* global Light */
            TryGetObjectFromScene(CharacterScene, out Light2D globalLight);
            Debug.Assert(globalLight != null, "globalLight != null");
            globalLight.intensity = scene.name == Stage4 ? 0 : 1;
            
            
            /* 플레이어 폼 체인지 */
            if (scene.name == Stage1)
            {
            	playerHandler.CurType = PlayerHandler.EMovementType.Run;
            }
            else if (scene.name == Stage3)
            {
                playerHandler.CurType = PlayerHandler.EMovementType.Static;
            }
            else if (scene.name == Stage4)
            {
                playerHandler.CurType = PlayerHandler.EMovementType.Lamp;
            }else if (scene.name == Bridge1 || scene.name == Bridge3)
            {
                playerHandler.CurType = PlayerHandler.EMovementType.Platformer;
            }else if (scene.name == Bridge2)
            {
                playerHandler.CurType = PlayerHandler.EMovementType.Run;
            }
            
            /* global Volume */
            if (GameManager.Instance.GlobalVolume.profile.TryGet(out ColorLookup lookUp))
            {
                lookUp.contribution.value = (scene.name == Stage4 ? 1f : 0f);
            }
        }
        else
        {
            Debug.Log("PlayerHandler가 존재하지 않거나, CharacterScene이 아닙니다.");
        }
    }

    public static bool TryGetObjectFromScene<T>(string sceneName, out T obj) where T : class
    {
        obj = null;
        
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            Scene curScene = SceneManager.GetSceneAt(i);
            if (curScene.name != sceneName)
            {
                continue;
            }
            foreach (var go in curScene.GetRootGameObjects())
            {
                if (go.TryGetComponent(out obj))
                {
                    return true;
                }
            }
            break;
        }
        return false;
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
    public IEnumerator FadeOut()
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

    public IEnumerator FadeIn()
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

    public void ReloadScene(string sceneName)
    {
        if (IsSceneLoaded(sceneName))
        {
            StartCoroutine(ReloadSceneRoutine(sceneName,LoadSceneMode.Additive));
        } 
        
    }

    private IEnumerator ReloadSceneRoutine(string sceneName, LoadSceneMode mode)
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.UnloadSceneAsync(sceneName);
        yield return StartCoroutine(LoadSceneFadeRoutine(sceneName, mode));
        //yield return StartCoroutine(FadeIn());        

    }
    
}
