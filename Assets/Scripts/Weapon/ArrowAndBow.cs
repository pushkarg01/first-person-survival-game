using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAndBow : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 30f;
    public float deactivateTimer = 3f;
    public float damage = 15f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Invoke("DeactivateGO",deactivateTimer);
    }

    public void Launch(Camera mainCamera)
    {
        rb.velocity = mainCamera.transform.forward * speed;

        transform.LookAt(transform.position+rb.velocity);
    }

   void DeactivateGO()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
