using UnityEngine;

public class PlataformaSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject tejadoPrefab;
    public GameObject plantaPrefab;
  
    [Header("Altura base")]
    public float offsetVertical = -2f;

    [Header("Spawning")]
    public float retardoEntreTorre = 2f;
    public float distanciaXDesdeCentro = 6f;
    public int minPlantas = 1;
    public int maxPlantas = 5;
    public float alturaPlanta = 1.5f;

    [Header("Escalas por planta (de arriba a abajo)")]
    public float[] escalaXPorPlanta = { 1.0f, 1.1f, 1.2f, 1.3f, 1.4f };

    [Header("Movimiento")]
    public float velocidadMovimiento = 2f;

    private float temporizador = 0f;
    private int indiceTorre = 0;

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            ConstruirParDeTorres();
            temporizador = retardoEntreTorre;
        }
    }

    void ConstruirParDeTorres()
    {
        int numPlantas = Random.Range(minPlantas, maxPlantas + 1);
        float offsetX = indiceTorre * distanciaXDesdeCentro + 2f;

        // Torre derecha
        ConstruirTorre(Vector3.zero, 1f, offsetX, numPlantas);

        // Torre izquierda
        ConstruirTorre(Vector3.zero, -1f, -offsetX, numPlantas);

        indiceTorre++;
    }

    void ConstruirTorre(Vector3 origen, float direccion, float posicionFinalX, int numPlantas)
    {
        GameObject torre = new GameObject("Torre");
        torre.transform.position = origen;
        torre.transform.localScale = Vector3.one;

        MovimientoPlataforma mover = torre.AddComponent<MovimientoPlataforma>();
        mover.direccionX = direccion;
        mover.velocidad = velocidadMovimiento;

        // Offset vertical global para bajar toda la torre
        float alturaInicialY = offsetVertical + numPlantas * alturaPlanta;

        // Tejado en lo alto
        Vector3 posTejado = new Vector3(origen.x, alturaInicialY, 0);
        Instantiate(tejadoPrefab, posTejado, Quaternion.identity, torre.transform);

        // Construcción de plantas desde arriba hacia abajo
        for (int i = 0; i < numPlantas; i++)
        {
            float y = alturaInicialY - ((i + 1) * alturaPlanta);
            Vector3 posPlanta = new Vector3(origen.x, y, 0);
            GameObject planta = Instantiate(plantaPrefab, posPlanta, Quaternion.identity, torre.transform);

            // Escalado según piso
            float escalaX = 1f;
            if (i < escalaXPorPlanta.Length)
            {
                escalaX = escalaXPorPlanta[i];
            }

            Vector3 escalaOriginal = planta.transform.localScale;
            planta.transform.localScale = new Vector3(
                escalaOriginal.x * escalaX,
                escalaOriginal.y,
                escalaOriginal.z
            );
        }
    }

}
