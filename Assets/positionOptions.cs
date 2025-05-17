using UnityEngine;

[ExecuteAlways] // esto permite que se ejecute en modo edición
public class CircularMenuLayout : MonoBehaviour
{
    public float radius = 2.5f;
    public float startAngle = -30f;
    public float endAngle = 30f;

    void Update()
    {
        if (!Application.isPlaying) LayoutChildren(); // solo en edición
    }

    void Start()
    {
        if (Application.isPlaying) LayoutChildren(); // solo en ejecución
    }

    void LayoutChildren()
    {
        int childCount = transform.childCount;
        if (childCount == 0) return;

        for (int i = 0; i < childCount; i++)
        {
            float t = (float)i / (childCount - 1);
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            float rad = angle * Mathf.Deg2Rad;

            Vector3 pos = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)) * radius;
            Transform child = transform.GetChild(i);
            child.localPosition = pos;
            child.LookAt(transform.position);
            child.Rotate(0, 180, 0);
        }
    }
}

