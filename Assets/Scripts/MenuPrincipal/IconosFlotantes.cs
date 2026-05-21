using UnityEngine;

public class IconosFlotantes : MonoBehaviour
{
    public float amplitude;  // la curva de subir i bajar
    public float speed;        // velocidad
    public float offset;       // para que se muevan distinto

    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float y = Mathf.Sin((Time.time + offset) * speed) * amplitude;
        rectTransform.anchoredPosition = new Vector2(startPosition.x, startPosition.y + y);
    }
}