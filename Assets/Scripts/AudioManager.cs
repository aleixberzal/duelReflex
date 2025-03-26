using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    public AudioSource SFXSource;


    public AudioClip background;
    public AudioClip death;
    public AudioClip kick;
    public AudioClip kunaiThrow;
    public AudioClip parry;
    public AudioClip jump;
}
