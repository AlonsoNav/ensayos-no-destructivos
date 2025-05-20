using UnityEngine;
using UnityEngine.UI;

public class ToolPickup : MonoBehaviour
{
    public GameObject popupUI;
    public AudioSource loc8Audio;
    public AudioSource pickupSFX;

    private bool isHovered = false;
    private bool hasActivated = false;

    public void ShowPopup()
    {
        popupUI.SetActive(true);
        isHovered = true;
        Invoke("PlayLOC8", 1f); // espera 1 segundo para reproducir LOC8
    }

    public void HidePopup()
    {
        popupUI.SetActive(false);
        isHovered = false;
        CancelInvoke("PlayLOC8");
    }

    private void PlayLOC8()
    {
        if (isHovered && loc8Audio != null)
        {
            loc8Audio.Play();
        }
    }

    public void OnClickPickup()
    {
        if (hasActivated) return;

        hasActivated = true;
        pickupSFX?.Play();
        ToolInteractionManager.Instance.SetState(ToolInteractionManager.ToolState.Pickup);

        // Simula que el objeto está en mano (desactiváslo y pasás a la siguiente lógica)
        gameObject.SetActive(false);
        popupUI.SetActive(false);
    }
}

