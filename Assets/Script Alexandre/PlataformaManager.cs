using UnityEngine;

public class PlataformaManager : MonoBehaviour
{
    [Header("Prefabs de casas completas (1 a 4 plantas)")]
    public GameObject[] casasPrefabs; // Índice 0 = 1 planta, índice 3 = 4 plantas

    [Header("Spawning")]
    public float retardoEntreTorres = 2f;
    public float distanciaX = 6f;
    public float offsetY = -2f;

    [Header("Movimiento")]
    public float velocidadMovimiento = 2f;
    public float alturaPlanta = 1.5f;

    private float temporizador = 0f;
    private int indiceTorre = 0;

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            GenerarParDeTorres();
            temporizador = retardoEntreTorres;
        }
    }

    void GenerarParDeTorres()
    {
        // Elegir prefab aleatorio
        int index = Random.Range(0, casasPrefabs.Length);
        GameObject prefabSeleccionado = casasPrefabs[index];

        // Calcular número de plantas y su altura en Y
        int numPlantas = index + 1;
        float alturaTotal = numPlantas * alturaPlanta;
        float yBase = offsetY + (alturaTotal / 2f);

        float xOffset = indiceTorre * distanciaX + 2f;

        // Torre derecha
        Instanciar(prefabSeleccionado, new Vector3(0f, yBase, 0f), 1f);

        // Torre izquierda
        Instanciar(prefabSeleccionado, new Vector3(0f, yBase, 0f), -1f);

        indiceTorre++;
    }

    void Instanciar(GameObject prefab, Vector3 posicion, float direccion)
    {
        GameObject torre = Instantiate(prefab, posicion, Quaternion.identity);

        MovimientoPlataforma mover = torre.GetComponent<MovimientoPlataforma>();
        if (mover == null)
        {
            mover = torre.AddComponent<MovimientoPlataforma>();
        }

        mover.direccionX = direccion;
        mover.velocidad = velocidadMovimiento;
    }
}
