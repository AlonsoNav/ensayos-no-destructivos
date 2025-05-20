using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GestionarAttachPoints : MonoBehaviour
{
    public Transform attachPointPunta;  // El punto de sujeción en la punta del cilindro
    public Transform attachPointLado;   // El punto de sujeción en el lado del cilindro
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Establece el punto de sujeción para el lado del cilindro
        grabInteractable.attachTransform = attachPointLado;

        // Establece el punto de sujeción secundario para la punta del cilindro
        grabInteractable.secondaryAttachTransform = attachPointPunta;

        // Añadir eventos para detectar cuando el objeto es agarrado o soltado usando la nueva firma
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    // Cuando el jugador agarra el cilindro
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Cambiar el punto de sujeción a la mano del jugador
        grabInteractable.attachTransform = attachPointLado;
    }

    // Cuando el jugador suelta el cilindro
    private void OnReleased(SelectExitEventArgs args)
    {
        // Verifica si la interacción no fue cancelada antes de cambiar el AttachPoint
        if (!args.isCanceled)
        {
            // Regresar el punto de sujeción a la punta
            grabInteractable.attachTransform = attachPointPunta;
        }
    }
}
