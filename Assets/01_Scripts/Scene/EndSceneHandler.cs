using UnityEngine;

public class EndSceneHandler : MonoBehaviour
{
    public void CreditEnd()
    {
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.TitleScene);
    }

    public void SetEndingSound(AudioClip audioClip)
    {
        BGMManager.Instance.PlayBGM(audioClip);
    }
}
