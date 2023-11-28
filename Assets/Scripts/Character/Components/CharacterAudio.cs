﻿using System.Linq;
using UnityEngine;

public class FirstPersonAudio : MonoBehaviour
{
    public FirstPersonMovement character;

    [Header("Step")]
    public AudioSource stepAudio;
    public AudioSource runningAudio;
    public AudioSource shootAudio;
    public AudioSource crystalAudio;


    [Tooltip("Minimum velocity for moving audio to play")]
    /// <summary> "Minimum velocity for moving audio to play" </summary>
    public float velocityThreshold = .01f;
    Vector2 lastCharacterPosition;
    Vector2 CurrentCharacterPosition => new Vector2(character.transform.position.x, character.transform.position.z);

    AudioSource[] MovingAudios => new AudioSource[] { stepAudio, runningAudio };

    void OnEnable() => SubscribeToEvents();

    void OnDisable() => UnsubscribeToEvents();

    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>();
        stepAudio = GetOrCreateAudioSource("Step Audio");
        runningAudio = GetOrCreateAudioSource("Running Audio");
        shootAudio = GetOrCreateAudioSource("Shoot Audio");
        crystalAudio = GetOrCreateAudioSource("Crystal Audio");
    }

    void FixedUpdate()
    {
        // Play moving audio if the character is moving and on the ground.
        float velocity = Vector3.Distance(CurrentCharacterPosition, lastCharacterPosition);
        if (velocity >= velocityThreshold)
        {
            if (character.IsRunning)
            {
                SetPlayingMovingAudio(runningAudio);
            }
            else
            {
                SetPlayingMovingAudio(stepAudio);
            }
        }
        else
        {
            SetPlayingMovingAudio(null);
        }

        // Remember lastCharacterPosition.
        lastCharacterPosition = CurrentCharacterPosition;
    }


    /// <summary>
    /// Pause all MovingAudios and enforce play on audioToPlay.
    /// </summary>
    /// <param name="audioToPlay">Audio that should be playing.</param>
    void SetPlayingMovingAudio(AudioSource audioToPlay)
    {
        // Pause all MovingAudios.
        foreach (var audio in MovingAudios.Where(audio => audio != audioToPlay && audio != null))
        {
            audio.Pause();
        }

        // Play audioToPlay if it was not playing.
        if (audioToPlay && !audioToPlay.isPlaying)
        {
            audioToPlay.Play();
        }
    }

    #region Subscribe/unsubscribe to events.
    void SubscribeToEvents()
    {
        GameEvents.instance.OnShoot += () => PlayClip(shootAudio);
        GameEvents.instance.OnGetPowerUp += () => PlayClip(crystalAudio);
    }

    void UnsubscribeToEvents()
    {
        GameEvents.instance.OnShoot -= () => PlayClip(shootAudio);
        GameEvents.instance.OnGetPowerUp -= () => PlayClip(crystalAudio);
    }
    #endregion

    #region Utility.
    /// <summary>
    /// Get an existing AudioSource from a name or create one if it was not found.
    /// </summary>
    /// <param name="name">Name of the AudioSource to search for.</param>
    /// <returns>The created AudioSource.</returns>
    AudioSource GetOrCreateAudioSource(string name)
    {
        // Try to get the audiosource.
        AudioSource result = System.Array.Find(GetComponentsInChildren<AudioSource>(), a => a.name == name);
        if (result)
            return result;

        // Audiosource does not exist, create it.
        result = new GameObject(name).AddComponent<AudioSource>();
        result.spatialBlend = 1;
        result.playOnAwake = false;
        result.transform.SetParent(transform, false);
        return result;
    }

    static void PlayClip(AudioSource audio)
    {

        if (!audio)
            return;

        audio.Play();
    }
    #endregion 
}
