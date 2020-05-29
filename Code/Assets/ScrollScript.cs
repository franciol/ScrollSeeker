using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController2>().pickScroll();
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
