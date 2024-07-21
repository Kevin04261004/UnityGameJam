using UnityEngine;

public class EndSceneHandler : MonoBehaviour
{
    private void Awake()
    {
        
        print(1);
    }
    public void CreditEnd()
    {
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.TitleScene);
    }

    public void SetEndingSound(AudioClip audioClip)
    {
        BGMManager.Instance.PlayBGM(audioClip);
    }
}
