using UnityEngine;

public class Kunai : MonoBehaviour
{
    private AudioManager audioManager;
    [Header("Ajustes")]
    public string tagObjetivo = "Player2"; // Por defecto

  
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagObjetivo))
        {

           
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (audioManager != null && audioManager.SFXSource != null && audioManager.death != null)
            {
                audioManager.SFXSource.clip = audioManager.death;
                audioManager.SFXSource.time = 0.1f;
                audioManager.SFXSource.volume = 0.5f;
                audioManager.SFXSource.Play();
               

                Debug.Log("Ha sonado la patada desde 0.3s");
            }
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
