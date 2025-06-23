using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationToObject : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] int Speed;
    bool lookx;
    private void FixedUpdate()
    {
        SmoothLook();
    }
    private void SmoothLook()
    {
        Vector3 direction = Object.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Speed * Time.deltaTime);
        
    }
}
