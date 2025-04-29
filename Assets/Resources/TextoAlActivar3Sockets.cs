using UnityEngine;
using TMPro;

public class TextoAlActivarTresCirculos : MonoBehaviour
{
    [Header("Círculos con Script")]
    public SocketSpriteColorYNumero circle1;
    public SocketSpriteColorYNumero circle2;
    public SocketSpriteColorYNumero circle3;
    public SocketSpriteColorYNumero circle4;
    public SocketSpriteColorYNumero circle5;
    public SocketSpriteColorYNumero circle6;
    public SocketSpriteColorYNumero circle7;
    public SocketSpriteColorYNumero circle8;
    public SocketSpriteColorYNumero circle9;
    public SocketSpriteColorYNumero circle10;

    [Header("Texto")]
    public TextMeshPro textoCambiar;
    public string textoCuandoActivos = "¡Los 10 círculos están activos!";

    private void Update()
    {
        VerificarCondicion();
    }

    private void VerificarCondicion()
    {
        if (circle1.estaActivado && circle2.estaActivado && circle3.estaActivado && 
            circle4.estaActivado && circle5.estaActivado && circle6.estaActivado &&
            circle7.estaActivado && circle8.estaActivado && circle9.estaActivado &&
            circle10.estaActivado)
        {
            textoCambiar.text = textoCuandoActivos;
        }
        else
        {
            textoCambiar.text = "";
        }
    }
}
