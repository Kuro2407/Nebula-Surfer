using UnityEngine;

public class SolarWave : MonoBehaviour
{
    public float waveSpeed;

    private PlayerMovement player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * waveSpeed * Time.deltaTime);

        if(transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(player != null)
            {
                player.SolarWave();
            }
            Destroy(gameObject);
        }
    }
}
