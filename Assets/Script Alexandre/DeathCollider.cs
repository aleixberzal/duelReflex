using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public string tagObjetivo = "Player1";

    public string tagObjetivo2 = "Player2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagObjetivo) || other.CompareTag(tagObjetivo2))
        {
            Destroy(other.gameObject); // Elimina al objetivo
        }
    }
}
