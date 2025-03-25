using UnityEngine;

public class Kunai : MonoBehaviour
{
    [Header("Ajustes")]
    public string tagObjetivo = "Player";
    public float tiempoDesaparicion = 5f;

    private void Start()
    {
        Destroy(gameObject, tiempoDesaparicion); // Se autodestruye tras X segundos por limpieza
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            Destroy(other.gameObject); // Elimina al jugador golpeado
            Destroy(gameObject);       // Elimina el kunai
        }
        else if (!other.isTrigger) // Si choca con cualquier otra cosa sólida
        {
            Destroy(gameObject);
        }
    }
}
