using UnityEngine;

public class ToolInteractionManager : MonoBehaviour
{
    public static ToolInteractionManager Instance; // Singleton opcional

    public enum ToolState
    {
        Idle,
        Hovering,
        Pickup,
        SearchSurface,
        AttachToWall,
        XRayView,
        Registering,
        Confirm,
        Results
    }

    public ToolState currentState = ToolState.Idle;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetState(ToolState newState)
    {
        currentState = newState;
        Debug.Log("State changed to: " + currentState);
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        switch (currentState)
        {
            case ToolState.Idle:
                // Mostrar la herramienta en la mesa
                break;
            case ToolState.Hovering:
                // Mostrar popup informativo
                break;
            case ToolState.Pickup:
                // Reproducir sonido, animación
                break;
            case ToolState.SearchSurface:
                // Activar sombras en paredes
                break;
            case ToolState.AttachToWall:
                // Snap de herramienta en la pared
                break;
            case ToolState.XRayView:
                // Mostrar rayos X, activar varillas
                break;
            case ToolState.Registering:
                // Permitir interacción con varillas
                break;
            case ToolState.Confirm:
                // Mostrar botón de confirmar
                break;
            case ToolState.Results:
                // Mostrar resultados
                break;
        }
    }
}

