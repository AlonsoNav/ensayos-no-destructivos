using UnityEngine;
using UnityEngine.InputSystem;

public class CarouselRotator : MonoBehaviour
{
    public InputActionAsset inputActions;

    public string actionMapName = "UI";
    public string rotateLeftActionName = "RotateLeft";
    public string rotateRightActionName = "RotateRight";

    public float rotationAngle = 30f;
    public float rotationSpeed = 5f;

    private InputAction rotateLeft;
    private InputAction rotateRight;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;

        // Buscar las acciones desde el asset
        var map = inputActions.FindActionMap(actionMapName);
        rotateLeft = map.FindAction(rotateLeftActionName);
        rotateRight = map.FindAction(rotateRightActionName);

        rotateLeft.Enable();
        rotateRight.Enable();

        rotateLeft.performed += _ => RotateLeft();
        rotateRight.performed += _ => RotateRight();
    }

    void OnDisable()
    {
        rotateLeft.Disable();
        rotateRight.Disable();
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void RotateLeft()
    {
        targetRotation *= Quaternion.Euler(0, -rotationAngle, 0);
    }

    public void RotateRight()
    {
        targetRotation *= Quaternion.Euler(0, rotationAngle, 0);
    }
}
