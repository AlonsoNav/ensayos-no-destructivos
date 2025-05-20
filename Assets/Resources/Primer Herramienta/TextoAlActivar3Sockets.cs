using UnityEngine;
using TMPro;
using UnityEngine.UI; // Necesario para trabajar con Slider

public class TextoAlActivarTresCirculos : MonoBehaviour
{
    [Header("Círculos con Script")]
    public SocketSpriteColorYNumero circle1;
    public SocketSpriteColorYNumero circle2;
    public SocketSpriteColorYNumero circle3;

    [Header("Objeto a mostrar")]
    public GameObject objetoParaMostrar;  // El objeto que se mostrará o ocultará

    [Header("Slider")]
    public Slider slider;  // El slider que se moverá

    private void OnEnable()
    {
        // Asegurarse de que el slider tenga un evento asignado para detectar cambios
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderChanged);
        }

        // Inicializar el objeto oculto al inicio
        if (objetoParaMostrar != null)
        {
            objetoParaMostrar.SetActive(false);
        }
    }


    private void OnDisable()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderChanged);
        }
    }

    // Este método se llama cuando el slider cambia
    private void OnSliderChanged(float value)
    {
        VerificarCondicion(value); // Ahora pasamos el valor del slider
    }

    private void VerificarCondicion(float sliderValue)
    {
        if (objetoParaMostrar == null)
            return;

        // Verifica si el slider está en el valor máximo y si los tres círculos están activos
        if (sliderValue == slider.maxValue && circle1.estaActivado && circle2.estaActivado && circle3.estaActivado)
        {
            objetoParaMostrar.SetActive(true);
        }
        else
        {
            objetoParaMostrar.SetActive(false);
        }
    }
}
