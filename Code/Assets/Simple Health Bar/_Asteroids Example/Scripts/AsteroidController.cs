/* Written by Kaz Crowe */
/* AsteroidController.cs */
using UnityEngine;
using System.Collections;


public class AsteroidController : MonoBehaviour
{
    // Reference Variables //
    Rigidbody myRigidbody;

    // Controller Booleans //





    void OnCollisionEnter(Collision theCollision)
    {

        // Else if the collision was from the player...
        if (theCollision.gameObject.name == "Player")
        {
            theCollision.gameObject.GetComponent<PlayerController2>().hurt(0.4f);
            Explode();
        }

    }

    void Explode()
    {
        Destroy(gameObject);
    }


}
