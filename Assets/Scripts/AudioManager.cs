using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip flip, match, mismatch, gameComplete;
    private AudioSource source;

    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlayFlip()
    {
        if (source == null || flip == null) return;
        source.PlayOneShot(flip);
    }

    public void PlayMatch()
    {
        if (source == null || match == null) return;
        source.PlayOneShot(match);
    }

    public void PlayMismatch()
    {
        if (source == null || mismatch == null) return;
        source.PlayOneShot(mismatch);
    }

    public void PlayGameComplete()
    {
        if (source == null || gameComplete == null) return;
        source.PlayOneShot(gameComplete);
    }
}