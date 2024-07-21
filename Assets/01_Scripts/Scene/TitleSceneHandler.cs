using UnityEngine;

public class TitleSceneHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.Bridge1);
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
