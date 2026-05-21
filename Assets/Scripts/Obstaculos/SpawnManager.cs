using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Meteoritos")]
    [SerializeField] private GameObject meteoritoPrefab;
    [SerializeField] private float tiempoEntreSpawn = 1.5f;
    [SerializeField] private float tiempoMinimo = 0.2f;
    [SerializeField] private float incrementoDificultad = 0.05f;
    [SerializeField] private float velocidadMeteorito = 6f;
    [SerializeField] private float incrementoVelocidad = 0.1f;
    [SerializeField] private float spawnMinY = -4f;
    [SerializeField] private float spawnMaxY = 4f;
    [SerializeField] private float separacionMinimaY = 2f;
    private float ultimaY = 0f;
    private float tiempoJuego;
    [SerializeField] private float tiempoMaxDificultad;

    [Header("PowerUps")]
    [SerializeField] private GameObject powerUpEscudoPrefab;
    [SerializeField] private GameObject powerUpTeleportePrefab;
    [SerializeField] private float tiempoEntrePowerUp = 5f;
    private float timerPowerUp;

    private float timer;

    void Update()
    {
        tiempoJuego += Time.deltaTime;
        timer += Time.deltaTime;
        timerPowerUp += Time.deltaTime;

        if (timer >= tiempoEntreSpawn)
        {
            Spawnear();
            timer = 0f;

            if (tiempoJuego < tiempoMaxDificultad)
            {
                tiempoEntreSpawn = Mathf.Max(tiempoMinimo, tiempoEntreSpawn - incrementoDificultad);
                velocidadMeteorito += incrementoVelocidad;
            }
        }

        if (timerPowerUp >= tiempoEntrePowerUp)
        {
            SpawnearPowerUp();
            timerPowerUp = 0f;
        }
    }

    public void PararSpawn()
    {
        enabled = false;
    }

    void Spawnear()
    {
        float y;
        int intentos = 0;
        separacionMinimaY =  Random.Range(0, 5);

        do
        {
            y = Random.Range(spawnMinY, spawnMaxY);
            intentos++;
        }
        while (Mathf.Abs(y - ultimaY) < separacionMinimaY && intentos < 10);

        ultimaY = y;

        GameObject m1 = Instantiate(meteoritoPrefab, new Vector3(12f, y, 0f), Quaternion.identity);
        m1.GetComponent<Meteorito>().SetVelocidad(velocidadMeteorito);
    }

    void SpawnearPowerUp()
    {
        float y = Random.Range(spawnMinY, spawnMaxY);
        Vector3 pos = new Vector3(12f, y, 0f);

        if (Random.value < 0.5f)
            Instantiate(powerUpEscudoPrefab, pos, Quaternion.identity);
        else
            Instantiate(powerUpTeleportePrefab, pos, Quaternion.identity);
    }
}