using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SonidoAlConectar : MonoBehaviour
{
    public AudioClip sonido; // El clip de audio a reproducir
    public float volumen = 1f;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnObjetoConectado);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnObjetoConectado);
    }

    private void OnObjetoConectado(SelectEnterEventArgs args)
    {
        // Reproducir el sonido en la posición del socket
        if (sonido != null)
        {
            AudioSource.PlayClipAtPoint(sonido, transform.position, volumen);
        }
    }
}
