using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtractorButton : MonoBehaviour
{
    public ExtractorNuclear extractor;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (extractor != null)
        {
            extractor.StartExtraction();
        }
    }
}