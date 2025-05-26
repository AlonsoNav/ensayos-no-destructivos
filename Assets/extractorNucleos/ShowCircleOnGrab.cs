using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCircleOnGrab : MonoBehaviour
{
    public GameObject[] circles2D;
    public Color hoverColor = Color.green;
    public Color defaultColor = Color.white;
    public float snapThreshold = 0.2f; // Distancia máxima para hacer snap
    public bool maintainUprightWhenSnapped = true; // Control para mantener recto el cilindro
    public float uprightRotationSpeed = 5f; // Velocidad para enderezar el cilindro

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private GameObject closestCircle;
    private Renderer circleRenderer;
    private bool wasMovedByUser = false;
    private Vector3 lastPosition;
    private bool isSnapped = false;
    private Quaternion targetUprightRotation;

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
        isSnapped = false;
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
        closestCircle = FindClosestCircle();
        
        if (closestCircle != null && Vector3.Distance(transform.position, closestCircle.transform.position) < snapThreshold)
        {
            // Hacer snap
            transform.position = closestCircle.transform.position;
            
            // Calcular rotación recta (ajustar según la orientación deseada)
            if (maintainUprightWhenSnapped)
            {
                // Mantener la rotación actual en el eje Y pero recto en X y Z
                targetUprightRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                transform.rotation = targetUprightRotation;
            }
            
            GetComponent<Rigidbody>().isKinematic = true;
            isSnapped = true;
            
            // Cambiar color del círculo
            closestCircle.GetComponent<Renderer>().material.color = hoverColor;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
            isSnapped = false;
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
        
        // Si está pegado, mantener la rotación recta
        if (isSnapped && maintainUprightWhenSnapped)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetUprightRotation, uprightRotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Si está pegado, forzar posición y rotación
        if (isSnapped && closestCircle != null)
        {
            GetComponent<Rigidbody>().MovePosition(closestCircle.transform.position);
            if (maintainUprightWhenSnapped)
            {
                GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(
                    transform.rotation, 
                    targetUprightRotation, 
                    uprightRotationSpeed * Time.deltaTime
                ));
            }
        }
    }
}