using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] kcik bombaScript; // Referencia al script de la bomba (kcik)
    [SerializeField] KeyCode throwKey = KeyCode.K; // Tecla para lanzar la bomba

    // Update se llama una vez por frame
    void Update()
    {
        // Detectar cuando el jugador presiona la tecla de lanzar bomba
        if (Input.GetKeyDown(throwKey))
        {
            // Llamar al método ThrowBomb() del script kcik
            bombaScript.ThrowBomb();
        }
    }
}
