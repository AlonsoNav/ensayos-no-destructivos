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

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        if (desactivarCoroutine != null)
            StopCoroutine(desactivarCoroutine);

        estaActivado = true;

        if (visualCircleSprite != null)
            visualCircleSprite.color = colorConObjeto;

        if (textDisplay != null)
        {
            int numeroAleatorio = Random.Range(0, 101);
            textDisplay.text = numeroAleatorio.ToString();
        }
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        if (desactivarCoroutine != null)
            StopCoroutine(desactivarCoroutine);

        desactivarCoroutine = StartCoroutine(EsperarAntesDeOcultar());
    }

    private IEnumerator EsperarAntesDeOcultar()
    {
        yield return new WaitForSeconds(tiempoAntesDeOcultar);

        estaActivado = false;

        if (visualCircleSprite != null)
            visualCircleSprite.color = colorSinObjeto;

        if (textDisplay != null)
            textDisplay.text = "";
    }
}
