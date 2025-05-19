using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtractorNuclear : MonoBehaviour
{
    public GameObject[] circles2D;
    public Color hoverColor = Color.green;
    public Color defaultColor = Color.white;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private GameObject closestCircle;
    private Renderer circleRenderer;
    public AudioClip extractionSound;
    private AudioSource audioSource;
    public XRController rightHandController;

    [Header("Extracción Nuclear")]
    public float extractionTime = 6f; // Duración de la extracción
    private bool isExtracting = false;
    private float extractionTimer = 0f;
    private bool wasButtonPressed = false;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Inicializar todos los círculos en blanco
        foreach (GameObject circle in circles2D)
        {
            if (circle != null)
            {
                circle.SetActive(false);
                circle.GetComponent<Renderer>().material.color = defaultColor;
            }
        }
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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
        // Activar todos los círculos al agarrar el cilindro
        foreach (GameObject circle in circles2D)
        {
            if (circle != null)
                circle.SetActive(true);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        closestCircle = FindClosestCircle();

        // Resetear color de TODOS los círculos a blanco
        foreach (GameObject circle in circles2D)
        {
            if (circle != null)
            {
                circle.GetComponent<Renderer>().material.color = defaultColor;
                if (circle != closestCircle)
                    circle.SetActive(false);
            }
        }

        // Cambiar color del círculo más cercano a verde y pegar el cilindro
        if (closestCircle != null)
        {
            circleRenderer = closestCircle.GetComponent<Renderer>();
            circleRenderer.material.color = hoverColor;

            transform.position = closestCircle.transform.position;
            GetComponent<Rigidbody>().isKinematic = true;
        }
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

    //Cambia a verde cuando el cilindro está cerca (pero sin soltarlo)
    void Update()
    {
        if (grabInteractable.isSelected) // Si el cilindro está agarrado
        {
            GameObject nearestCircle = FindClosestCircle();
            if (nearestCircle != null && Vector3.Distance(transform.position, nearestCircle.transform.position) < 0.2f)
            {
                nearestCircle.GetComponent<Renderer>().material.color = hoverColor;
            }
        }
        if (closestCircle != null && !grabInteractable.isSelected)
        {
            CheckControllerInput();
        }
        if (isExtracting)
        {
            extractionTimer += Time.deltaTime;
            if (extractionTimer >= extractionTime)
            {
                FinishExtraction();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartExtraction();
        }
    }

    // Método para iniciar la extracción (llamar desde el botón)
    public void StartExtraction()
    {
        if (!isExtracting && closestCircle != null)
        {
            isExtracting = true;
            extractionTimer = 0f;
            audioSource.PlayOneShot(extractionSound);
            Debug.Log("Extracción iniciada con botón VR!");
        }
    }

    private void FinishExtraction()
    {
        isExtracting = false;
        //audioSource.Stop();
        Debug.Log("Extracción completada!");
        // Aquí luego añadiremos la generación del núcleo
    }

     private void CheckControllerInput()
    {
        if (rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            // Detecta el momento en que se PRESIONA el botón (no mientras se mantiene)
            if (isPressed && !wasButtonPressed)
            {
                StartExtraction();
                wasButtonPressed = true;
            }
            else if (!isPressed)
            {
                wasButtonPressed = false;
            }
        }
    }
}