using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private float timeFootStep;
    public GameObject footPrint;
    private float _velocity;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FootPrint();
    }

    private void InivisiblePlayer()
    {
        // if (Time.time >= timeFootStep)
        // {
        //     GameObject f = Instantiate(footPrint, transform.position, transform.rotation);
        //     Destroy(f, 3f)
        //     t
        // }
    }

    private void FootPrint()
    {
        if (Time.time >= timeFootStep)
        {
            GameObject print = Instantiate(footPrint, transform.position, transform.rotation);
            Destroy(print, 3f);
            timeFootStep = Time.time + 0.3f;
        }
    }
}
