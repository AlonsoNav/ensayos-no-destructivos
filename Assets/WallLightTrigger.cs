using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;


public class WallLightTrigger : MonoBehaviour
{
    public Renderer wallRenderer;
    public Color emissionColor = Color.cyan;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Material wallMaterial;
    private bool isInteracting = false; // Para controlar si se está interactuando

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
        wallMaterial = wallRenderer.material;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Iniciar la corutina para iluminar la pared brevemente
        if (!isInteracting)
        {
            StartCoroutine(ActivateWallLight());
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Apagar la luz inmediatamente cuando se suelta
        StopAllCoroutines();
        wallMaterial.SetColor("_EmissionColor", Color.black);
    }

    // Corutina que controla el parpadeo de la luz
    private IEnumerator ActivateWallLight()
    {
        isInteracting = true;
        
        // Activar la emisión
        wallMaterial.EnableKeyword("_EMISSION");
        wallMaterial.SetColor("_EmissionColor", emissionColor);

        // Esperar un segundo (puedes ajustar el tiempo)
        yield return new WaitForSeconds(1f);

        // Apagar la emisión después del tiempo de espera
        wallMaterial.SetColor("_EmissionColor", Color.black);
        
        isInteracting = false;
    }
}
