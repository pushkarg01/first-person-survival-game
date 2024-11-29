using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footStepSound;
    private CharacterController characterController;

   private float accumulatedDistance;

    [SerializeField] private AudioClip[] footClips;

    [HideInInspector] public float volumeMin, volumeMax;
    [HideInInspector] public float stepDistence;

    private void Awake()
    {
        footStepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        CheckToPlayFootSound();
    }

    void CheckToPlayFootSound()
    {
        if (!characterController.isGrounded) return;

        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;
            if (accumulatedDistance > stepDistence)
            {
                footStepSound.volume = Random.Range(volumeMin, volumeMax);
                footStepSound.clip= footClips[Random.Range(0,footClips.Length)];
                footStepSound.Play();

                accumulatedDistance = 0f;
            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
    }
}
