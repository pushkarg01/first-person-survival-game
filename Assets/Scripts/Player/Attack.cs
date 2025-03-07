using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 10f;
    public float radius = 1f;

    public LayerMask layerMask;

    private void Update()
    {
        Collider[] hits =Physics.OverlapSphere(transform.position,radius,layerMask);

        if(hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<Health>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
