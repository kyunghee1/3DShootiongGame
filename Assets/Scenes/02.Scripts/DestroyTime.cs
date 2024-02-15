using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{

    public float DeleteTime = 1.5f;
    private float _timer = 0;

    public void Start()
    {
        
    }

    private void Update()
    {
        _timer = Time.time;
        if ( _timer >= DeleteTime)
        {
            Destroy(gameObject);
        }
    }
}
