using UnityEngine;
using UnityEngine.UI;

public class TilleFondo : MonoBehaviour
{
    [SerializeField] private float speedX = 0.05f;
    [SerializeField] private float speedY = 0f;

    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        rawImage.uvRect = new Rect(
            rawImage.uvRect.x + speedX * Time.deltaTime,
            rawImage.uvRect.y + speedY * Time.deltaTime,
            rawImage.uvRect.width,
            rawImage.uvRect.height
        );
    }
}