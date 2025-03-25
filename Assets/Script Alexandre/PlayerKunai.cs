using Unity.VisualScripting;
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

    [Header("Retardo de disparo")]
    public float retardoDisparo = 0.2f;

    [Header("Tag del objetivo")]
    public string tagObjetivo = "Player2"; // Este valor se puede cambiar desde el Inspector

    private float direccionActual = 1f;

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
        if (Animator != null)
        {
            Animator.SetTrigger("Kunai");
        }

        direccionActual = mirandoDerecha ? 1f : -1f;

        Invoke(nameof(DispararKunai), retardoDisparo);
    }

    void DispararKunai()
    {
        GameObject kunai = Instantiate(kunaiPrefab, puntoDisparo.position, Quaternion.identity);

        Rigidbody2D rb = kunai.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(direccionActual * velocidadKunai, 0f);

            Vector3 escala = kunai.transform.localScale;
            escala.x *= direccionActual > 0 ? 1 : -1;
            kunai.transform.localScale = escala;
        }

        // Asignar el tag del objetivo al kunai
        Kunai kunaiScript = kunai.GetComponent<Kunai>();
        if (kunaiScript != null)
        {
            kunaiScript.tagObjetivo = tagObjetivo;
        }
    }
}
