using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCircleOnGrab : MonoBehaviour
{
    public GameObject[] circles2D;
    public Color hoverColor = Color.green;
    public Color defaultColor = Color.white;
    public float snapThreshold = 0.2f; // Distancia máxima para hacer snap

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private GameObject closestCircle;
    private Renderer circleRenderer;
    private bool wasMovedByUser = false;
    private Vector3 lastPosition;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        lastPosition = transform.position;
        
        foreach (GameObject circle in circles2D)
        {
            if (circle != null)
            {
                circle.SetActive(false);
                circle.GetComponent<Renderer>().material.color = defaultColor;
            }
        }
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Marcar que el usuario está moviendo el objeto
        wasMovedByUser = true;
        
        // Activar todos los círculos
        foreach (GameObject circle in circles2D)
        {
            if (circle != null)
            {
                circle.SetActive(true);
                circle.GetComponent<Renderer>().material.color = defaultColor;
            }
        }
        
        // Asegurarse de que la física está activa
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Solo hacer snap si no fue movido manualmente o está cerca de un círculo
        closestCircle = FindClosestCircle();
        
        if (closestCircle != null && Vector3.Distance(transform.position, closestCircle.transform.position) < snapThreshold)
        {
            // Hacer snap
            transform.position = closestCircle.transform.position;
            GetComponent<Rigidbody>().isKinematic = true;
            
            // Cambiar color del círculo
            closestCircle.GetComponent<Renderer>().material.color = hoverColor;
        }
        else
        {
            // Si no está cerca de ningún círculo, mantenerlo donde está
            GetComponent<Rigidbody>().isKinematic = false;
        }
        
        // Desactivar círculos no usados
        foreach (GameObject circle in circles2D)
        {
            if (circle != null && circle != closestCircle)
            {
                circle.SetActive(false);
            }
        }
        
        wasMovedByUser = false;
    }

    private GameObject FindClosestCircle()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject circle in circles2D)
        {
            if (circle == null || !circle.activeSelf) continue;

            float distance = Vector3.Distance(transform.position, circle.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = circle;
            }
        }
        
        return closest;
    }

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            // Verificar si el usuario está moviendo el objeto
            if (Vector3.Distance(transform.position, lastPosition) > 0.01f)
            {
                wasMovedByUser = true;
            }
            lastPosition = transform.position;

            // Resaltar círculo cercano
            GameObject nearestCircle = FindClosestCircle();
            if (nearestCircle != null)
            {
                float distance = Vector3.Distance(transform.position, nearestCircle.transform.position);
                nearestCircle.GetComponent<Renderer>().material.color = 
                    distance < snapThreshold ? hoverColor : defaultColor;
            }
        }
    }
}