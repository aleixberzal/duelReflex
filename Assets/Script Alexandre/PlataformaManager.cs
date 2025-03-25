using UnityEngine;

public class PlataformaManager : MonoBehaviour
{
    [Header("Prefab de plataforma de bambú")]
    public GameObject plataformaBambuPrefab;

    [Header("Spawning")]
    public float retardoEntrePlataformas = 1.5f;
    public float alturaInicial = -4f;
    public int cantidadNiveles = 5;
    public float separacionEntreNiveles = 2f;

    [Header("Escala aleatoria")]
    public float escalaMin = 0.8f;
    public float escalaMax = 2f;

    [Header("Velocidad aleatoria")]
    public float velocidadMin = 1f;
    public float velocidadMax = 3f;

    private float temporizador = 0f;
    private float[] nivelesY;
    private int ultimoNivelUsado = -1;

    void Start()
    {
        // Generar alturas fijas
        nivelesY = new float[cantidadNiveles];
        for (int i = 0; i < cantidadNiveles; i++)
        {
            nivelesY[i] = alturaInicial + i * separacionEntreNiveles;
        }
    }

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            GenerarParDePlataformas();
            temporizador = retardoEntrePlataformas;
        }
    }

    void GenerarParDePlataformas()
    {
        int indiceNivel = ElegirNivelAleatorioSinRepetir();
        float posY = nivelesY[indiceNivel];
        float escalaX = Random.Range(escalaMin, escalaMax);
        float velocidad = Random.Range(velocidadMin, velocidadMax);

        Vector3 posicion = new Vector3(0f, posY, 0f);

        // Derecha
        Instanciar(plataformaBambuPrefab, posicion, 1f, escalaX, velocidad);

        // Izquierda
        Instanciar(plataformaBambuPrefab, posicion, -1f, escalaX, velocidad);

        // Guardar nivel usado
        ultimoNivelUsado = indiceNivel;
    }

    int ElegirNivelAleatorioSinRepetir()
    {
        if (cantidadNiveles <= 1)
            return 0;

        int nuevoIndice;
        do
        {
            nuevoIndice = Random.Range(0, nivelesY.Length);
        }
        while (nuevoIndice == ultimoNivelUsado);

        return nuevoIndice;
    }

    void Instanciar(GameObject prefab, Vector3 posicion, float direccion, float escalaX, float velocidad)
    {
        GameObject plataforma = Instantiate(prefab, posicion, Quaternion.identity);

        Vector3 escalaOriginal = plataforma.transform.localScale;
        plataforma.transform.localScale = new Vector3(
            escalaOriginal.x * escalaX,
            escalaOriginal.y,
            escalaOriginal.z
        );

        MovimientoPlataforma mover = plataforma.GetComponent<MovimientoPlataforma>();
        if (mover == null)
        {
            mover = plataforma.AddComponent<MovimientoPlataforma>();
        }

        mover.direccionX = direccion;
        mover.velocidad = velocidad;
    }
}
