using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    [SerializeField] float Time;
    void Start()
    {
        Destroy(this.gameObject, Time);
    }


}
