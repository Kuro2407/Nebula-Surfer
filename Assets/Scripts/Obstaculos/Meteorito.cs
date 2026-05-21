using UnityEngine;

public class Meteorito : MonoBehaviour
{
    private float speed = 6f;

    public void SetVelocidad(float nuevaVelocidad)
    {
        speed = nuevaVelocidad;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -12f)
            Destroy(gameObject);
    }
}