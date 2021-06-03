using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItem : MonoBehaviour
{
    private RaycastHit raycastHit;
    
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 50, Color.yellow);
        /*if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out raycastHit,
            Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);
        }*/
    }
}
