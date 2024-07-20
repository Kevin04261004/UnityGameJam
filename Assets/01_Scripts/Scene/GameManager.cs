using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _settingPanel;
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI; 
    
    public enum EGameType
    {
        Start,
        Title,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }
    public EGameType GameType { get; set; } = EGameType.Start;
    
    private void Awake()
    {
        // Application.targetFrameRate = 60;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetActiveGameOverUI(bool condition)
    {
        gameOverUI.SetActive(condition);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SettingOn()
    {
        _settingPanel.SetActive(true);
    }
}
