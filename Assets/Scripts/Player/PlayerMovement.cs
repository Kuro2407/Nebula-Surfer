using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector2 limiteMin = new Vector2(-8f, -4f);
    [SerializeField] private Vector2 limiteMax = new Vector2(8f, 4f);

    private Rigidbody2D rb;
    private Vector2 input;

    public int vidas = 3;
    [SerializeField] private Sprite corazonLleno;
    [SerializeField] private Sprite corazonVacio;
    [SerializeField] private Image[] corazones;

    [Header("Escudo")]
    public bool tieneEscudo = false;
    private bool escudoActivo = false;
    [SerializeField] private float duracionEscudo = 3f;
    [SerializeField] private GameObject escudoVisual;
    [SerializeField] private GameObject panelEscudoHUD;
    [SerializeField] private Image barraEscudo;
    private float tiempoEscudoRestante = 0f;

    [Header("Teleporte")]
    private bool tieneTeleporte;
    [SerializeField] private GameObject iconoTeleporteHUD;

    [Header("OndaSolar")]
    public float waveEffectDuration = 5f;
    private bool controlesInvertidos;
    private float timerOndaSolar;
    [SerializeField] private GameObject iconoOndaSolarHUD; // opcional

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vidas = 3;
        ActualizarCorazones();
        if (panelEscudoHUD) panelEscudoHUD.SetActive(false);
        if (barraEscudo) barraEscudo.gameObject.SetActive(false);
        if (iconoTeleporteHUD) iconoTeleporteHUD.SetActive(false);
        if (iconoOndaSolarHUD) iconoOndaSolarHUD.SetActive(false);
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // ── Onda Solar ──────────────────────────────
        if (controlesInvertidos)
        {
            timerOndaSolar -= Time.deltaTime;
            inputX = -inputX;
            inputY = -inputY;

            if (timerOndaSolar <= 0f)
            {
                controlesInvertidos = false;
                if (iconoOndaSolarHUD) iconoOndaSolarHUD.SetActive(false);
            }
        }

        input.x = inputX;
        input.y = inputY;
        // ────────────────────────────────────────────

        float targetRotation = -input.x * 20f;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, targetRotation),
            Time.deltaTime * 10f
        );

        if (Input.GetKeyDown(KeyCode.E) && tieneEscudo)
            UsarEscudo();

        if (escudoActivo)
        {
            tiempoEscudoRestante -= Time.deltaTime;
            if (barraEscudo) barraEscudo.fillAmount = tiempoEscudoRestante / duracionEscudo;
            if (tiempoEscudoRestante <= 0f)
                DesactivarEscudo();
        }

        if (Input.GetMouseButtonDown(0) && tieneTeleporte)
            Teletransportar();
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + input.normalized * speed * Time.fixedDeltaTime;
        newPos.x = Mathf.Clamp(newPos.x, limiteMin.x, limiteMax.x);
        newPos.y = Mathf.Clamp(newPos.y, limiteMin.y, limiteMax.y);
        rb.MovePosition(newPos);
    }

    // ── Llamado desde SolarWave.cs ───────────────
    public void SolarWave()
    {
        controlesInvertidos = true;
        timerOndaSolar = waveEffectDuration;
        if (iconoOndaSolarHUD) iconoOndaSolarHUD.SetActive(true);
    }
    // ─────────────────────────────────────────────

    void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidas)
                corazones[i].sprite = corazonLleno;
            else
                corazones[i].sprite = corazonVacio;
        }
    }

    public void ActivarEscudo()
    {
        tieneEscudo = true;
        if (panelEscudoHUD) panelEscudoHUD.SetActive(true);
        if (barraEscudo) barraEscudo.gameObject.SetActive(false);
    }

    void UsarEscudo()
    {
        tieneEscudo = false;
        escudoActivo = true;
        tiempoEscudoRestante = duracionEscudo;
        if (panelEscudoHUD) panelEscudoHUD.SetActive(false);
        if (escudoVisual) escudoVisual.SetActive(true);
        if (barraEscudo) barraEscudo.gameObject.SetActive(true);
        if (barraEscudo) barraEscudo.fillAmount = 1f;
    }

    void DesactivarEscudo()
    {
        escudoActivo = false;
        tiempoEscudoRestante = 0f;
        if (escudoVisual) escudoVisual.SetActive(false);
        if (panelEscudoHUD) panelEscudoHUD.SetActive(false);
        if (barraEscudo) barraEscudo.gameObject.SetActive(false);
        if (barraEscudo) barraEscudo.fillAmount = 1f;
    }

    public void GuardarTeleporte()
    {
        tieneTeleporte = true;
        if (iconoTeleporteHUD) iconoTeleporteHUD.SetActive(true);
    }

    public void Teletransportar()
    {
        if (!tieneTeleporte) return;

        Vector3 posicionMundo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionMundo.z = 0f;

        posicionMundo.x = Mathf.Clamp(posicionMundo.x, limiteMin.x, limiteMax.x);
        posicionMundo.y = Mathf.Clamp(posicionMundo.y, limiteMin.y, limiteMax.y);

        transform.position = posicionMundo;
        tieneTeleporte = false;
        if (iconoTeleporteHUD) iconoTeleporteHUD.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            if (escudoActivo)
            {
                Destroy(other.gameObject);
                return;
            }

            Destroy(other.gameObject);
            vidas--;
            ActualizarCorazones();

            if (vidas <= 0)
                Debug.Log("Game Over");
        }
    }
}