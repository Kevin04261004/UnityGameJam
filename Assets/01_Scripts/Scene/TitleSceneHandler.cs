using UnityEngine;

public class TitleSceneHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.Stage1);
    }

    public void ContinueGame()
    {
        SceneHandler.Instance.ContinueStage();
    }

    public void SettingOn()
    {
        GameManager.Instance.SettingOn();
    }
}
