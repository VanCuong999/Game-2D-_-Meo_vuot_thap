
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("-------------Audio Source -----------")]
    [SerializeField] AudioSource musicSoure;
    [SerializeField] AudioSource SFXSounre;

    [Header("-----------Audio Clip---------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;
    private void Start() 
    {
        musicSoure.clip = background;
        musicSoure.Play();    
    }
}
