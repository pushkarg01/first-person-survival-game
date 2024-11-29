using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, lookRoot;

    [SerializeField] private bool invert;
    [SerializeField] private float senstivity = 5f;

    // [SerializeField] private bool canUnlock = true;

    // [SerializeField]private float smoothWeight = 0.4f;
    // [SerializeField]private int smoothSteps = 10;
    // [SerializeField]private float rollAngle = 1f;
    // [SerializeField]private float rollSpeed = 1f;

    [SerializeField]private Vector2 defaultLookLimits = new Vector2(-70f, 80f);
    private Vector2 lookAngles;
    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    private float currentRollAngle;
    private int lastLookFrame;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LockAndUnlockCursor();
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        lookAngles.x += currentMouseLook.x * senstivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * senstivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x,defaultLookLimits.x,defaultLookLimits.y);

        // currentRollAngle = Mathf.Lerp(currentRollAngle,Input.GetAxisRaw(MouseAxis.MOUSE_X) * rollAngle,Time.deltaTime * rollSpeed);

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x,0f,0f);
        playerRoot.localRotation= Quaternion.Euler(0,lookAngles.y,0);
    }

}
