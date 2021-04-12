using UnityEngine;
using UnityEngine.UI;

public class VolumeValueChange : MonoBehaviour
{

    // Reference to Audio Source component
    private AudioSource audioSrc;

    // Music volume variable that will be modified
    // by dragging slider knob
    private static float musicVolume = 0.05f;

    // Use this for initialization
    void Start()
    {
        DataMantainer.Volumen = musicVolume;

        // Assign Audio Source component to control it
        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = musicVolume;
    }

    // Method that is called by slider game object
    // This method takes vol value passed by slider
    // and sets it as musicValue
    public void SetVolume(float vol)
    {
        musicVolume = vol;
        DataMantainer.Volumen = musicVolume;

        // Setting volume option of Audio Source to be equal to musicVolume
        audioSrc.volume = musicVolume;
    }
}