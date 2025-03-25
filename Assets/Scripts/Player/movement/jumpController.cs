using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("Fuerza del Salto")]
    [SerializeField] private float fuerzaDeSalto = 8.5f;
    public bool isJumping;
    public bool grounded;
    private basicMovement movement;
    public float jumpReduction = 3f;
    private float originalSpeed;
    public string jumpTecla = "space";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground")) 
        {
            grounded = true;
            isJumping = false; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground")) 
        {
            grounded = false;
        }
    }

    private void Start()
    {
        movement = GetComponent<basicMovement>(); 
        rb2D = GetComponent<Rigidbody2D>();
        originalSpeed = movement.movementSpeed; 
    }

    private void Update()
    {
        if (!isJumping && grounded) 
        {
            Saltar();
        }
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(jumpTecla)) 
        {
            if (grounded)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, fuerzaDeSalto); 
                isJumping = true; 
                grounded = false;
            }
        }
    }
}
