using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log(0);
    }

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log(0);
    }
}
