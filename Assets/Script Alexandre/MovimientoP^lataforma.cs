using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    public float velocidad = 2f;
    public float direccionX = 1f;
    public float limiteDesaparicion = 30f;

    [Header("Fade")]
    public float fadeInicio = 0f;
    public float fadeFin = 3f;

    private SpriteRenderer[] sprites;

    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        SetAlpha(0f); // Comienza invisible
    }

    void Update()
    {
        transform.position += new Vector3(direccionX * velocidad * Time.deltaTime, 0f, 0f);

        float distancia = Mathf.Abs(transform.position.x);
        float alpha = Mathf.InverseLerp(fadeInicio, fadeFin, distancia);
        SetAlpha(alpha);

        if (distancia > limiteDesaparicion)
        {
            Destroy(gameObject);
        }
    }

    void SetAlpha(float alpha)
    {
        foreach (var sr in sprites)
        {
            Color c = sr.color;
            c.a = Mathf.Clamp01(alpha);
            sr.color = c;
        }
    }
}
