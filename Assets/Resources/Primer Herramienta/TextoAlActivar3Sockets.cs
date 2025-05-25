using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextoAlActivarTresCirculos : MonoBehaviour
{
    [Header("Círculos con Script")]
    public SocketSpriteColorYNumero circle1;
    public SocketSpriteColorYNumero circle2;
    public SocketSpriteColorYNumero circle3;

    [Header("Objeto a mostrar")]
    public GameObject objetoParaMostrar;  // El objeto que se mostrará u ocultará

    [Header("Canvas a mostrar")]
    public GameObject canvasParaMostrar;  // El Canvas que se mostrará u ocultará

    [Header("Slider dentro del Canvas")]
    public Slider slider;  // El slider que sigue controlando la activación del objeto

    private void OnEnable()
    {
        if (canvasParaMostrar != null)
            canvasParaMostrar.SetActive(false);

        if (objetoParaMostrar != null)
            objetoParaMostrar.SetActive(false);

        if (slider != null)
        {
            slider.gameObject.SetActive(false);
            slider.onValueChanged.AddListener(OnSliderChanged);
        }
    }

    private void Update()
    {
        VerificarCirculos();
    }

    private void OnDisable()
    {
        if (slider != null)
            slider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    // Mostrar u ocultar Canvas basado en círculos activos
    private void VerificarCirculos()
    {
        if (circle1.estaActivado && circle2.estaActivado && circle3.estaActivado)
        {
            if (canvasParaMostrar != null && !canvasParaMostrar.activeSelf)
            {
                canvasParaMostrar.SetActive(true);
                if (slider != null)
                    slider.gameObject.SetActive(true);
            }
        }
        else
        {
            if (canvasParaMostrar != null && canvasParaMostrar.activeSelf)
            {
                canvasParaMostrar.SetActive(false);
                if (slider != null)
                    slider.gameObject.SetActive(false);
            }
            if (objetoParaMostrar != null)
                objetoParaMostrar.SetActive(false);
        }
    }

    // Controlar activación del objeto según valor del slider
    private void OnSliderChanged(float value)
    {
        if (objetoParaMostrar == null) return;

        if (value == slider.maxValue && circle1.estaActivado && circle2.estaActivado && circle3.estaActivado)
        {
            objetoParaMostrar.SetActive(true);
        }
        else
        {
            objetoParaMostrar.SetActive(false);
        }
    }
}
