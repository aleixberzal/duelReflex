using UnityEngine;

public class PlataformaBambuHabilidad : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject plataformaPrefab;
    public float duracionPlataforma = 1f;
    public Vector2 offset = new Vector2(0f, -1f);

    [Header("Habilidad")]
    public KeyCode teclaInvocacion = KeyCode.J;
    public float cooldown = 15f;

    private float tiempoUltimaInvocacion = -Mathf.Infinity;
    private GameObject plataformaActual;

    void Update()
    {
        if (Input.GetKeyDown(teclaInvocacion) && PuedeUsarHabilidad())
        {
            InvocarPlataforma();
        }
    }

    bool PuedeUsarHabilidad()
    {
        return Time.time >= tiempoUltimaInvocacion + cooldown && plataformaActual == null;
    }

    void InvocarPlataforma()
    {
        Vector3 posicion = transform.position + (Vector3)offset;
        plataformaActual = Instantiate(plataformaPrefab, posicion, Quaternion.identity);

        Destroy(plataformaActual, duracionPlataforma);
        tiempoUltimaInvocacion = Time.time;
    }
}
