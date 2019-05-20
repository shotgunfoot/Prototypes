using UnityEngine;
using TMPro;

/*
    Class : ConsoleInput
    Behaviour: This class contains reference to an InputField and any audio required to play
    when interacting with said Inputfield.
 */
public class ConsoleInput : MonoBehaviour
{
    
    public TMP_InputField InputField;
    public AudioClips clips;

    private AudioSource audioSource;    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
        
    public void PlayRandomTypeSound()
    {
        audioSource.PlayOneShot(clips.sounds[UnityEngine.Random.Range(0, clips.sounds.Length)]);
    }
    
    public void FocusOnInputField()
    {
        InputField.ActivateInputField();
    }

}
