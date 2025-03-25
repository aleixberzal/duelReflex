using UnityEngine;

public class JumpController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private basicMovement movement;

    [Header("Fuerza del Salto")]
    public float fuerzaDeSalto = 8.5f;
    public float jumpReduction = 3f;
    public string jumpTecla = "space";

    [Header("Detección de suelo")]
    public Transform puntoSuelo;
    public float radioSuelo = 0.1f;
    public LayerMask capaSuelo;

    public bool grounded;
    public bool isJumping;
    private float originalSpeed;

    void Start()
    {
        movement = GetComponent<basicMovement>();
        rb2D = GetComponent<Rigidbody2D>();
        originalSpeed = movement.movementSpeed;
    }

    void Update()
    {
        // Detecció de terra via Overlap + tag
        grounded = false;
        Collider2D[] colisiones = Physics2D.OverlapCircleAll(puntoSuelo.position, radioSuelo);
        foreach (Collider2D col in colisiones)
        {
            if (col.CompareTag("ground"))
            {
                grounded = true;
                break;
            }
        }

        // Salt
        if (!isJumping && grounded)
        {
            Saltar();
        }

        // Detectar si ha aterrat realment
        if (isJumping && grounded && Mathf.Abs(rb2D.velocity.y) < 0.1f)
        {
            isJumping = false;
        }
    }


    void Saltar()
    {
        if (Input.GetKeyDown(jumpTecla))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, fuerzaDeSalto);
            isJumping = true;
            grounded = false;
        }
    }


    void OnDrawGizmosSelected()
    {
        if (puntoSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoSuelo.position, radioSuelo);
        }
    }
}
