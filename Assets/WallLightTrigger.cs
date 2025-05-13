using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallLightTrigger : MonoBehaviour
{
    public Renderer wallRenderer;
    public Color emissionColor = Color.cyan;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Material wallMaterial;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
        wallMaterial = wallRenderer.material;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        wallMaterial.EnableKeyword("_EMISSION");
        wallMaterial.SetColor("_EmissionColor", emissionColor);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        wallMaterial.SetColor("_EmissionColor", Color.black);
    }
}
