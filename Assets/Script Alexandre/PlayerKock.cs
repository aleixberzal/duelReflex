using System;
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

    [Header("Tag del kunai enemigo")]
    public string tagKunai = "Kunai";
    public string nuevoTagObjetivo = "Player1"; // Cambiar según quién lanza la patada

    private Animator Animator;
    private AudioManager audioManager;
    private void Start()
    {
        Animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        

    }
    void Update()
    {
        if (Input.GetKeyDown(teclaPatada))
        {
            if (Animator != null)
            {
                Animator.SetTrigger("Kick");
                
            }
            LanzarPatada();
            if (audioManager != null && audioManager.SFXSource != null && audioManager.kick != null)
            {
                audioManager.SFXSource.clip = audioManager.kick;
                audioManager.SFXSource.time = 0.2f;
                audioManager.SFXSource.Play();

                Debug.Log("Ha sonado la patada desde 0.3s");
            }
        }
    }

    void LanzarPatada()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(puntoGolpe.position, radioGolpe);

        foreach (Collider2D obj in objetos)
        {
            // 1. Empujar enemigos (según capa)
            if (((1 << obj.gameObject.layer) & capaEnemigo) != 0)
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direccion = (obj.transform.position - transform.position).normalized;
                    rb.AddForce(direccion * fuerzaEmpuje, ForceMode2D.Impulse);
                }
               

            }

            // 2. Rebotar kunai
            if (obj.CompareTag(tagKunai))
            {
                Rigidbody2D rbKunai = obj.GetComponent<Rigidbody2D>();
                Kunai kunaiScript = obj.GetComponent<Kunai>();

                if (rbKunai != null && kunaiScript != null)
                {
                    // Invertir la dirección del kunai
                    rbKunai.velocity = new Vector2(-rbKunai.velocity.x, rbKunai.velocity.y);

                    // Voltear el sprite visual
                    Vector3 escala = obj.transform.localScale;
                    escala.x *= -1;
                    obj.transform.localScale = escala;

                    // Cambiar objetivo
                    kunaiScript.tagObjetivo = nuevoTagObjetivo;

                    if (audioManager != null && audioManager.SFXSource != null && audioManager.parry != null)
                    {
                        audioManager.SFXSource.clip = audioManager.parry;
                        audioManager.SFXSource.time = 0.2f;
                        audioManager.SFXSource.Play();

                        Debug.Log("Ha sonado la patada desde 0.3s");
                    }

                }
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
