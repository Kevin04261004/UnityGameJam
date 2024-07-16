using UnityEngine;

public class SetDialoguePanelComponent : MonoBehaviour, IInteractableObject
{
    [TextArea][SerializeField] private string str;
    [SerializeField] private float durationTime = 2f;
    [SerializeField] private bool TypingMode = true;
    public void Interact()
    {
        DialogueManager.Instance.SetPanel(str, Color.white, true, durationTime);
    }
}
