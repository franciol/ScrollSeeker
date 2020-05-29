using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight01 : MonoBehaviour
{
    private Light light;
    private bool up;
    private float Intensity;
    public float speed;
    public float min;
    public float max;
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
        Intensity = Random.Range(min, max);
        up = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        light.range = Intensity;
        if (up)
        {
            if(Intensity>=max)
            {
                up = false;
            }
            else
            {
                Intensity += speed;
            }
        }
        else
        {
            if(Intensity <= min)
            {
                up = true;
            }
            else
            {
                Intensity -= speed;
            }
        }

    }
}
