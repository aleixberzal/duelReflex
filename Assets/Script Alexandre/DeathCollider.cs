using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public string tagObjetivo = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            Destroy(other.gameObject); // Elimina al objetivo
        }
    }
}
