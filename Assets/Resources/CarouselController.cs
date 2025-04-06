using UnityEngine;

public class CarouselController : MonoBehaviour
{
    public RectTransform[] panels;
    public float spacing = 600f;
    public float moveSpeed = 10f;

    private int currentIndex = 1;
    private Vector2[] targetPositions;

    void Start()
    {
        targetPositions = new Vector2[panels.Length];
        UpdateTargetPositions(true);
    }

    void Update()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].anchoredPosition = Vector2.Lerp(
                panels[i].anchoredPosition,
                targetPositions[i],
                Time.deltaTime * moveSpeed
            );
        }
    }

    public void MoveLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateTargetPositions();
        }
    }

    public void MoveRight()
    {
        if (currentIndex < panels.Length - 1)
        {
            currentIndex++;
            UpdateTargetPositions();
        }
    }

    void UpdateTargetPositions(bool instant = false)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            float xOffset = (i - currentIndex) * spacing;
            Vector2 newPos = new Vector2(xOffset, 0);

            if (instant)
                panels[i].anchoredPosition = newPos;

            targetPositions[i] = newPos;
        }
    }
}