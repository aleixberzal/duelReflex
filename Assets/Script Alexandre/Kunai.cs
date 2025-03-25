using UnityEngine;

public class Kunai : MonoBehaviour
{
    [Header("Ajustes")]
    public string tagObjetivo = "Player2"; // Por defecto

    public float tiempoDesaparicion = 5f;

    private void Start()
    {
        Destroy(gameObject, tiempoDesaparicion);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
