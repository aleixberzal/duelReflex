using UnityEngine;

public class PlayerKock : MonoBehaviour
{
    [Header("Golpe")]
    public float fuerzaEmpuje = 10f;
    public float radioGolpe = 1f;
    public Vector2 direccionGolpe = Vector2.right;
    public KeyCode teclaPatada = KeyCode.K;

    [Header("Detección")]
    public Transform puntoGolpe;
    public LayerMask capaEnemigo;

    void Update()
    {
        if (Input.GetKeyDown(teclaPatada))
        {
            LanzarPatada();
        }
    }

    void LanzarPatada()
    {
        
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(puntoGolpe.position, radioGolpe, capaEnemigo);

        foreach (Collider2D enemigo in enemigos)
        {
            Rigidbody2D rb = enemigo.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calcular dirección desde el jugador hacia el enemigo
                Vector2 direccion = (enemigo.transform.position - transform.position).normalized;
                rb.AddForce(direccion * fuerzaEmpuje, ForceMode2D.Impulse);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (puntoGolpe != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoGolpe.position, radioGolpe);
        }
    }
}
