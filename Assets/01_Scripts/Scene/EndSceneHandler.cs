using UnityEngine;

public class EndSceneHandler : MonoBehaviour
{
    public void CreditEnd()
    {
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.TitleScene);
    }
}
