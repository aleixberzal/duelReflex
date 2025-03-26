using UnityEngine;

public class AudioToggleButton : MonoBehaviour
{
    public AudioSource audioSource;
    private bool estaActivo = true;

    public void ToggleAudio()
    {
        estaActivo = !estaActivo;
        audioSource.mute = !estaActivo;
    }
}
