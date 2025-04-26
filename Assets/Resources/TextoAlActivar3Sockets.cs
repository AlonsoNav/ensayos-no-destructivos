using UnityEngine;
using TMPro;

public class TextoAlActivarTresCirculos : MonoBehaviour
{
    [Header("Círculos con Script")]
    public SocketSpriteColorYNumero circle1;
    public SocketSpriteColorYNumero circle2;
    public SocketSpriteColorYNumero circle3;

    [Header("Texto")]
    public TextMeshPro textoCambiar;
    public string textoCuandoActivos = "¡Los 3 círculos están activos!";

    private void Update()
    {
        VerificarCondicion();
    }

    private void VerificarCondicion()
    {
        if (circle1.estaActivado && circle2.estaActivado && circle3.estaActivado)
        {
            textoCambiar.text = textoCuandoActivos;
        }
        else
        {
            textoCambiar.text = "";
        }
    }
}
