using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DestroyFish : MonoBehaviour
{
    [SerializeField] GameObject Particle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fish")
        {
            GameObject Particles = Instantiate(Particle, other.gameObject.transform.position, quaternion.identity);
            Particles.SetActive(true);
            Destroy(other.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
