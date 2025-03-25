using UnityEngine;

public class PlayerKunai : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject kunaiPrefab;
    public Transform puntoDisparo;
    public float velocidadKunai = 10f;
    public float cooldown = 1f;
    public KeyCode teclaDisparo = KeyCode.L;
    private Animator Animator;
    private float tiempoUltimoDisparo = -Mathf.Infinity;
    private bool mirandoDerecha = true;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Detectar dirección mirando (según escala X)
        mirandoDerecha = transform.localScale.x > 0;

        if (Input.GetKeyDown(teclaDisparo) && Time.time >= tiempoUltimoDisparo + cooldown)
        {
            LanzarKunai();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void LanzarKunai()
    {
        if(Animator != null)
        {
            Animator.SetTrigger("Kunai");
        }
        GameObject kunai = Instantiate(kunaiPrefab, puntoDisparo.position, Quaternion.identity);

        Rigidbody2D rb = kunai.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direccion = mirandoDerecha ? 1f : -1f;
            rb.velocity = new Vector2(direccion * velocidadKunai, 0f);

            // Voltear kunai si mira a la izquierda
            Vector3 escala = kunai.transform.localScale;
            escala.x *= direccion > 0 ? 1 : -1;
            kunai.transform.localScale = escala;
        }
    }
}
