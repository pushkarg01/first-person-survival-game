using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerFootsteps playerSteps;
    private Transform lookRoot;

    private PlayerStats playerStats;
    private float sprintValue = 100f;
    public float sprintTreshold = 10f;

    private float sprintVolume =1.0f;
    private float crouchVolume =0.1f;
    private float walkVolumeMin = 0.2f, walkVolumeMax = 0.6f;

    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed =2f ;

    private float standHeight = 1.6f;
    private float crouchHeight = 1f;

    private bool isCrouching;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        playerSteps=GetComponentInChildren<PlayerFootsteps>();

        lookRoot= transform.GetChild(0);
        playerStats=GetComponent<PlayerStats>();
    }

    private void Start()
    {
        playerSteps.volumeMin=walkVolumeMin;
        playerSteps.volumeMax=walkVolumeMax;
        playerSteps.stepDistence=walkStepDistance;
    }

    private void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if (sprintValue > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                movement.speed = sprintSpeed;

                playerSteps.stepDistence = sprintStepDistance;
                playerSteps.volumeMin = sprintVolume;
                playerSteps.volumeMax = sprintVolume;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            movement.speed = moveSpeed;

            playerSteps.stepDistence = walkStepDistance;
            playerSteps.volumeMin = walkVolumeMin;
            playerSteps.volumeMax = walkVolumeMax;
        }

        SprintStaminaStats();
    }

    void SprintStaminaStats()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprintValue -= sprintTreshold * Time.deltaTime;
            if (sprintValue <= 0f)
            {
                sprintValue = 0f;

                movement.speed = moveSpeed;
                playerSteps.stepDistence = walkStepDistance;
                playerSteps.volumeMin = walkVolumeMin;
                playerSteps.volumeMax = walkVolumeMax;
            }
            playerStats.DisplayStaminaStats(sprintValue);
        }
        else
        {
            if (sprintValue != 100f)
            {
                sprintValue += (sprintTreshold / 2f) * Time.deltaTime;
                playerStats.DisplayStaminaStats(sprintValue);

                if (sprintValue > 100f)
                {
                    sprintValue = 100f;
                }
            }
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                lookRoot.localPosition = new Vector3(0f,standHeight,0f);
                movement.speed = moveSpeed;

                playerSteps.stepDistence = walkStepDistance;
                playerSteps.volumeMin = walkVolumeMin;
                playerSteps.volumeMax = walkVolumeMax;

                isCrouching = false;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f,crouchHeight, 0f);
                movement.speed = crouchSpeed;

                playerSteps.stepDistence = crouchStepDistance;
                playerSteps.volumeMin = crouchVolume;
                playerSteps.volumeMax = crouchVolume;

                isCrouching = true;
            }
        }

    }

}
