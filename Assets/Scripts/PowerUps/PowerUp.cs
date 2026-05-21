using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -12f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AplicarEfecto(other.GetComponent<PlayerMovement>());
            Destroy(gameObject);
        }
    }

    protected virtual void AplicarEfecto(PlayerMovement player)
    {
        // Cada power-up sobreescribe este método
    }
}