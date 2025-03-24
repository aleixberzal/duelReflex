using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    public float velocidad = 2f;
    public float direccionX = 1f;
    public float limiteDesaparicion = 30f;

    void Update()
    {
        transform.position += new Vector3(direccionX * velocidad * Time.deltaTime, 0f, 0f);

        if (Mathf.Abs(transform.position.x) > limiteDesaparicion)
        {
            Destroy(gameObject);
        }
    }
}
