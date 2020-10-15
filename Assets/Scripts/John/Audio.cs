using System;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour // I dont like the way the audio is called, should be called externaly not internaly
{
    public static Audio Instance;

    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private AudioMixerGroup[] audioMixerGroups;

    [SerializeField]
    private float stackingDoubleMaxTimer = 0.75f;

    private AudioSource audioSource;

    private float stackingDoubleTimer;
    private float stackingPitch = 1;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameManager.Instance.OnSharkKilled += AudioOnSharkKill;
    }
    private void Update()
    {
        stackingDoubleTimer += Time.deltaTime;
    }
    private void AudioOnSharkKill(float timeToKillShark)
    {
        if (timeToKillShark < 0.75f)
        {
            if (stackingDoubleTimer < stackingDoubleMaxTimer)
            {
                stackingPitch += 0.1f;
                PlaySoundEffect("DoubleEffect", "ArcadeBlip1", stackingPitch);

                stackingDoubleTimer = 0;
            }
            else
            {
                PlaySoundEffect("DoubleEffect", "ArcadeBlip1");

                stackingDoubleTimer = 0;
                stackingPitch = 1;
            }

        }
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
