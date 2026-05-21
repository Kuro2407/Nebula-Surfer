using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool tiempo;
    public bool pausa;

    public float puntos;
    public float puntosFinales;

    public TMP_Text textoPuntos;
    public TMP_Text textoPuntosFinales;

    public GameObject panelGameOver;
    public GameObject panelPausa;
    public PlayerMovement playerMovement;
    public SpawnManager spawnManager;

    void Start()
    {
        tiempo = true;
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Time.timeScale = 1f;
        panelGameOver.SetActive(false);
    }

    void Update()
    {
        Puntos();
        PuntuacionFinal();
        Pausa();
    }

    public void Puntos()
    {
        if (tiempo)
        {
            puntos += Time.deltaTime;
            textoPuntos.text = "Puntos: " + puntos.ToString("F0");
        }
    }

    public void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pausa = !pausa;

        if (pausa)
        {
            Time.timeScale = 0f;
            panelPausa.SetActive(true);
        }
        else
        {
            panelPausa.SetActive(false);
            if (tiempo) Time.timeScale = 1f;
        }
    }

    public void PuntuacionFinal()
    {
        if (playerMovement.vidas <= 0 && tiempo)
        {
            tiempo = false;
            puntosFinales = puntos;
            panelGameOver.SetActive(true);
            Time.timeScale = 0f;
            playerMovement.gameObject.SetActive(false);
            spawnManager.PararSpawn();
            textoPuntosFinales.text = "Puntos Totales: " + puntosFinales.ToString("F0");
        }
    }
}