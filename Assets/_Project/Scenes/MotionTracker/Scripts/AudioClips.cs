using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips", menuName = "Prototypes/AudioClips", order = 0)]
public class AudioClips : ScriptableObject
{
    public AudioClip[] sounds;
}