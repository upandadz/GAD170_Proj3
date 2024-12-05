using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;

    /// <summary>
    /// Plays at sound once at chosen audio source
    /// </summary>
    /// <param name="source">Where to play the sound</param>
    /// <param name="clip">Choose sound from audio manager audioclips list</param>
    public void PlaySoundOnce(AudioSource source,AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays sound on a loop
    /// </summary>
    /// <param name="source"></param>
    /// <param name="clip"></param>
    public void PlaySoundLoop(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.loop = true;
        source.Play();
    }

    /// <summary>
    /// Stops audio source sound from playing - can be used to stop audio loop
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        source.Stop();
    }
}
