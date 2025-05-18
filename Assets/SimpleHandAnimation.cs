using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic; // ¡Esta línea faltaba!

public class SimpleHandAnimation : MonoBehaviour
{
    public float closedRotation = 45f; // Ajusta según necesidad
    private Quaternion openRotation;
    private InputDevice targetDevice;

    void Start()
    {
        openRotation = transform.localRotation;
        var characteristics = transform.parent.name.Contains("Left") ? 
            InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right;
        var devices = new List<InputDevice>(); // Ahora List<> funcionará
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        if (devices.Count > 0) targetDevice = devices[0];
    }

    void Update()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            // Interpola entre rotación abierta y cerrada
            transform.localRotation = Quaternion.Slerp(
                openRotation,
                Quaternion.Euler(closedRotation, 0, 0),
                gripValue
            );
        }
    }
}