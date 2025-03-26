using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public string tagObjetivo = "Player1";
    private AudioManager audioManager;
    public string tagObjetivo2 = "Player2";

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagObjetivo) || other.CompareTag(tagObjetivo2))
        {
            Destroy(other.gameObject); // Elimina al objetivo

            if (audioManager != null && audioManager.SFXSource != null && audioManager.death != null)
            {
                audioManager.SFXSource.clip = audioManager.death;
                audioManager.SFXSource.time = 0.2f;
                audioManager.SFXSource.Play();
                audioManager.SFXSource.volume = 0.5f;

                Debug.Log("Ha sonado la patada desde 0.3s");
            }
        }

    }
}
