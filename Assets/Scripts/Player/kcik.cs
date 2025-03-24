using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kcik : MonoBehaviour
{
    [SerializeField] float radius = 10f; // Radio de la explosión
    [SerializeField] float force = 100f; // Fuerza de la explosión
    [SerializeField] ContactFilter2D contactFilter; // Filtro de contacto para 2D
    [SerializeField] Collider2D[] affectedColliders = new Collider2D[25]; // Arreglo para almacenar los colliders afectados
    [SerializeField] public GameObject bombaPrefab; // Prefab de la bomba
    [SerializeField] float throwForce = 5f; // Fuerza con la que se lanza la bomba

    private Rigidbody2D rb; // Referencia al Rigidbody2D del objeto

    // Método que lanza la bomba desde la posición del personaje
    public void ThrowBomb()
    {
        // Crear la bomba en la posición actual del personaje
        GameObject bomba = Instantiate(bombaPrefab, transform.position, Quaternion.identity);

        // Obtener el Rigidbody2D de la bomba para poder lanzarla
        Rigidbody2D bombaRb = bomba.GetComponent<Rigidbody2D>();

        // Lanzar la bomba hacia adelante (si es necesario ajustar la dirección, puedes hacerlo aquí)
        Vector2 direction = transform.up; // Puedes cambiar esto si quieres que la bomba se lance en otra dirección
        bombaRb.velocity = direction * throwForce;

        // Iniciar la explosión después de 3 segundos
        Destroy(bomba, 3f); // Esto destruye la bomba después de 3 segundos
        bomba.AddComponent<Explosion>().ExplodeAfterDelay(3f, radius, force, contactFilter, affectedColliders); // Llamamos a la explosión después de un retraso
    }
}

public class Explosion : MonoBehaviour
{
    public void ExplodeAfterDelay(float delay, float radius, float force, ContactFilter2D contactFilter, Collider2D[] affectedColliders)
    {
        StartCoroutine(ExplosionCoroutine(delay, radius, force, contactFilter, affectedColliders));
    }

    private IEnumerator ExplosionCoroutine(float delay, float radius, float force, ContactFilter2D contactFilter, Collider2D[] affectedColliders)
    {
        yield return new WaitForSeconds(delay); // Esperar 3 segundos

        int numColliders = Physics2D.OverlapCircle(transform.position, radius, contactFilter, affectedColliders);

        if (numColliders > 0)
        {
            for (int i = 0; i < numColliders; i++)
            {
                // Primero aplica la fuerza a los Rigidbodies dentro del rango
                if (affectedColliders[i].gameObject.TryGetComponent(out Rigidbody2D rb))
                {
                    // Calcula la dirección de la fuerza hacia fuera de la bomba
                    Vector2 forceDirection = (rb.transform.position - transform.position).normalized;
                    rb.AddForce(forceDirection * force, ForceMode2D.Impulse);
                }
            }
        }

        // Destruye la bomba después de la explosión
        Destroy(gameObject);
    }
}
