using UnityEngine;
using TMPro;

public class PopUpDescripcion : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject popupUI;            // El panel o texto que quieres mostrar
    public TextMeshProUGUI descripcionText; // Texto dentro del Pop Up
    [TextArea]
    public string descripcion = "Descripción del objeto"; // Texto que quieres mostrar

    // private void Start()
    // {
    //     if (popupUI != null)
    //         popupUI.SetActive(false); // Inicialmente oculto
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (popupUI != null)
            {
                popupUI.SetActive(true);
                descripcionText.text = descripcion;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (popupUI != null)
                popupUI.SetActive(false);
        }
    }
}