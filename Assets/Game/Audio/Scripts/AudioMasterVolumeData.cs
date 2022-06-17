using UnityEngine;

[CreateAssetMenu(fileName = "AudioMasterVolume", menuName = "AudioMasterVolume", order = 0)]
public class AudioMasterVolumeData : ScriptableObject
{
    public float MusicVolume = 1f;
    public float SoundVolume = 1f;
}
