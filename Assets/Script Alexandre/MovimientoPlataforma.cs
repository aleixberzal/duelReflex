using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    public float velocidad = 2f;
    public float direccionX = 1f;
    public float limiteDesaparicion = 30f;

    [Header("Fade In")]
    public float duracionFade = 1f;

    private SpriteRenderer[] sprites;
    private Collider2D[] colliders;
    private float tiempoFade = 0f;

    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        colliders = GetComponentsInChildren<Collider2D>();

        SetAlpha(0f);
        SetCollidersActivos(false); // Desactivar colisión al inicio
    }

    void Update()
    {
        transform.position += new Vector3(direccionX * velocidad * Time.deltaTime, 0f, 0f);

        // Fade-in visual
        if (tiempoFade < duracionFade)
        {
            tiempoFade += Time.deltaTime;
            float alpha = Mathf.Clamp01(tiempoFade / duracionFade);
            SetAlpha(alpha);

            if (tiempoFade >= duracionFade)
            {
                SetCollidersActivos(true); // Activar colisión al terminar el fade
            }
        }

        if (Mathf.Abs(transform.position.x) > limiteDesaparicion)
        {
            Destroy(gameObject);
        }
    }

    void SetAlpha(float alpha)
    {
        foreach (var sr in sprites)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }

    void SetCollidersActivos(bool estado)
    {
        foreach (var col in colliders)
        {
            col.enabled = estado;
        }
    }
}
