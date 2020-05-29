using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScript : MonoBehaviour
{
    PlayerController2 pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<PlayerController2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            int tipe = pl.getAttack();
            if(tipe == 1)
            {
                other.GetComponent<VampireController>().getHurt(0.5f);

            }
            else if(tipe == 2)
            {
                other.GetComponent<VampireController>().getHurt(0.7f);
            }
        }
    }
}
