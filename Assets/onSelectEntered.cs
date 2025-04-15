using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MenuOption : MonoBehaviour
{
    public string optionName;

    private void OnEnable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(OnSelect);
    }

    private void OnDisable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnSelect);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        Debug.Log($"Seleccionaste la opción: {optionName}");
        // Aquí puedes cargar una escena, activar UI, etc.
    }
}
