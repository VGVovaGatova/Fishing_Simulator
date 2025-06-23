using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwiming : MonoBehaviour
{
    [SerializeField] int Speed;
    [SerializeField] float SpeedFish = 0.5f;
    [SerializeField] GameObject[] Points;
    [SerializeField] GameObject ObjectNeedToucnh;
    [SerializeField] GameObject[] ObjectsFish;
    [SerializeField] GameObject Player;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] bool CanLook;
    int Checkint;
    public float reachThreshold = 0.5f;
    
    int Random;
    int RandomPoint;
    Rigidbody rb;
    bool Debounce;
    Vector3 Derection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("Swiming");
        Debug.Log(Player.GetComponent<FishAndInventory>().FishName.Length);
        FishSkin();
    }
    void FishSkin()
    {
        if (Player.GetComponent<FishAndInventory>().FishName[Checkint] == gameObject.name)
        {
            ObjectsFish[Checkint].SetActive(true);
            Checkint = 0;
        }
        else
        {
            Checkint++;
            if (Checkint >= Player.GetComponent<FishAndInventory>().FishName.Length)
            {
                ObjectsFish[0].SetActive(true);
                Debug.Log("Not have Skin Fish");
                Checkint = 0;
            }
            else
            {
                FishSkin();
            }
        }
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Points[RandomPoint].transform.position, SpeedFish * Time.deltaTime);
    }
    IEnumerator Swiming()
    {
        while (true)
        {
        yield return new WaitForSeconds(1);
        Random = UnityEngine.Random.Range(0, 3);
        if (Random == 0 && !Debounce)
        {
            Random = -1;
            RandomPoint = UnityEngine.Random.Range(0, Points.Length);
            SpeedFish = UnityEngine.Random.Range(0.1f, 1);
            
        //    Derection = (Points[RandomPoint].transform.position - transform.position).normalized;
       //     ObjectNeedToucnh = Points[RandomPoint];
         //   rb.AddForce(Derection * Speed);
         yield return new WaitForSeconds(1);
            Debounce = false;
            CanLook = true;
          //  StartCoroutine("ReturnBack");
        }
        }
    }
    IEnumerator ReturnBack()
    {
        yield return new WaitForSeconds(5);
        rb.velocity = Vector3.zero;
        ObjectNeedToucnh = null;
        Debounce = false;
        CanLook = false;
    }

    private void FixedUpdate() 
    {
        if (ObjectNeedToucnh != null)
        {
            if (Vector3.Distance(transform.position, Points[RandomPoint].transform.position) <= reachThreshold)
            {
                StopCoroutine("ReturnBack");
                
                rb.velocity = Vector3.zero;
                ObjectNeedToucnh = null;
                Debounce = false;
                CanLook = false;
            }
        }
        if (CanLook)
        {
            SmoothLook();
        }
    }
    private void SmoothLook()
    {
        Vector3 direction = Points[RandomPoint].transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
