using System;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour 
{
    public static Audio Instance; 

    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private AudioMixerGroup[] audioMixerGroups;

    private AudioSource audioSource;

    private float stackingDoubleTimer;
    private float stackingPitch = 0.9f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.OnSharkKilled += AudioOnSharkKilled;
    }
    private void Update()
    {
        stackingDoubleTimer += Time.deltaTime;

        if (stackingDoubleTimer >= GameManager.Instance.doubleTime) // Resets the stacking pitch if too much time passes 
        {
            stackingDoubleTimer = 0;
            stackingPitch = 0.9f;
        }
    }
    public void AudioOnSharkKilled(float sharkTimeToKill)
    {
        if (CheckForGun())
        {
            PlaySoundEffect("Gunshot", "ArcadeShot"); // Increases the pitch everytime
            return;
        }

        if (sharkTimeToKill < GameManager.Instance.doubleTime)
        {
            PlaySoundEffect("DoubleEffect", "ArcadeBlip1", stackingPitch += 0.1f); // Increases the pitch everytime
            stackingDoubleTimer = 0;
            return;
        }

        // If all else fails, we just play a punch
        //PlaySoundEffect("Punch", "Punch");
    }

    private bool CheckForGun()
    {
        return PowerupManager.Instance.gunActive;
    }

    public void PlaySoundEffect(string audioMixerGroupToFind, string audioClipToFind, float pitch = 1)
    {
        AudioMixerGroup audioMixerGroupFound = FindMixerGroup(audioMixerGroupToFind);

        if (audioMixerGroupFound != null)
        {
            audioMixerGroupFound.audioMixer.SetFloat("DoubleEffectPitch", pitch);
            audioSource.outputAudioMixerGroup = audioMixerGroupFound;
        }

        AudioClip audioClipToPlay = FindAudioClip(audioClipToFind);

        if (audioClipToPlay != null)
        {
            audioSource.PlayOneShot(audioClipToPlay);
        }
    }

    private AudioMixerGroup FindMixerGroup(string groupToFind)
    {
        foreach (var group in audioMixerGroups)
        {
            if (group.name == groupToFind)
            {
                return group;
            }
        }

        print("Audio Mixer Group Could Not Be Found");
        return null;
    }

    private AudioClip FindAudioClip(string clipToFind)
    {
        foreach (var clip in audioClips)
        {
            if (clip.name == clipToFind)
            {
                return clip;
            }
        }

        print("Audio Clip Could Not Be Found");
        return null;
    }
}
