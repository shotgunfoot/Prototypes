using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips", menuName = "Holder/AudioClips", order = 0)]
public class AudioClips : ScriptableObject
{
    public AudioClip[] sounds;
}