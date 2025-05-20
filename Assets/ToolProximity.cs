using UnityEngine;

public class ToolProximity : MonoBehaviour
{
    private ToolPickup toolPickup;

    private void Start()
    {
        toolPickup = GetComponentInParent<ToolPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            toolPickup.ShowPopup();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            toolPickup.HidePopup();
        }
    }
}

