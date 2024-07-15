using UnityEngine;

public class TitleSceneHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.Stage1);
    }
}
