using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCircleOnGrab : MonoBehaviour
{
    public GameObject[] circles2D; // Arrastra TODOS tus círculos aquí en el Inspector

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        
        // Desactivar todos los círculos al inicio
        foreach (GameObject circle in circles2D)
        {
            if (circle != null)
                circle.SetActive(false);
        }
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Activar todos los círculos
        foreach (GameObject circle in circles2D)
        {
            if (circle != null)
                circle.SetActive(true);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Desactivar todos los círculos
        foreach (GameObject circle in circles2D)
        {
            if (circle != null)
                circle.SetActive(false);
        }
    }
}