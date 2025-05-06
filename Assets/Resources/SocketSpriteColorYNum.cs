using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Collections;

public class SocketSpriteColorYNumero : MonoBehaviour
{
    [Header("Referencias")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;                   // Socket que detecta el objeto
    public SpriteRenderer visualCircleSprite;           // Sprite del círculo
    public TextMeshPro textDisplay;                     // Texto que muestra el número

    [Header("Colores")]
    public Color colorSinObjeto = Color.white;          // Color por defecto
    public Color colorConObjeto = Color.green;          // Color al colocar objeto

    [Header("Temporizador")]
    public float tiempoAntesDeOcultar = 2f;             // Tiempo de espera antes de desactivarse

    [HideInInspector] public bool estaActivado = false; // Estado visible del círculo

    [Header("Número a Mostrar")]
    public int numeroManual = 25; // El número que se mostrará en lugar del aleatorio

    private Coroutine desactivarCoroutine;

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnObjectPlaced);
        socket.selectExited.AddListener(OnObjectRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnObjectPlaced);
        socket.selectExited.RemoveListener(OnObjectRemoved);
    }

    // Llamado cuando el objeto es colocado en el socket
    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        ActivarCirculo();
    }

    // Llamado cuando el objeto es retirado del socket
    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        if (desactivarCoroutine != null)
            StopCoroutine(desactivarCoroutine);

        desactivarCoroutine = StartCoroutine(EsperarAntesDeOcultar());
    }

    // Activar manualmente el círculo
    [ContextMenu("Activar Circulo")]
    public void ActivarCirculo()
    {
        if (desactivarCoroutine != null)
            StopCoroutine(desactivarCoroutine);

        estaActivado = true;

        if (visualCircleSprite != null)
            visualCircleSprite.color = colorConObjeto;

        if (textDisplay != null)
        {
            // Mostrar el número manualmente en lugar de uno aleatorio
            textDisplay.text = numeroManual.ToString();
        }
    }

    // Desactivar manualmente el círculo
    [ContextMenu("Desactivar Circulo")]
    public void DesactivarCirculo()
    {
        if (desactivarCoroutine != null)
            StopCoroutine(desactivarCoroutine);

        estaActivado = false;

        if (visualCircleSprite != null)
            visualCircleSprite.color = colorSinObjeto;

        if (textDisplay != null)
            textDisplay.text = "";
    }

    // Esperar antes de desactivar
    private IEnumerator EsperarAntesDeOcultar()
    {
        yield return new WaitForSeconds(tiempoAntesDeOcultar);

        DesactivarCirculo();
    }
}
