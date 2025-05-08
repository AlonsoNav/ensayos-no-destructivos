using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCircleOnGrab : MonoBehaviour
{
    public GameObject circle2D; // Asigna tu círculo 2D en el Inspector

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        circle2D.SetActive(false);

        // Suscribirse a eventos de interacción
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        circle2D.SetActive(true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        circle2D.SetActive(false);
    }

    void OnDestroy()
    {
        // Limpiar suscripciones
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}