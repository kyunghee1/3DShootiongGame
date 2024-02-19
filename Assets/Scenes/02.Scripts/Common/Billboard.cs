using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
   private void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
