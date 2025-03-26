using UnityEngine;

public class Kunai : MonoBehaviour
{
    private AudioManager audioManager;
    [Header("Ajustes")]
    public string tagObjetivo = "Player2"; // Por defecto

    public float tiempoDesaparicion = 5f;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Destroy(gameObject, tiempoDesaparicion);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagObjetivo))
        {

            if (audioManager != null && audioManager.SFXSource != null && audioManager.death != null)
            {
                audioManager.SFXSource.clip = audioManager.death;
                audioManager.SFXSource.time = 0.1f;
                audioManager.SFXSource.Play();

                Debug.Log("Ha sonado la patada desde 0.3s");
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
