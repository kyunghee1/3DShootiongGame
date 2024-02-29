using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeleType
{
    Destory,
    Inactive,
}
public class DestroyTime : MonoBehaviour
{
    public DeleType DeleType; //파괴까지의 시간

    public float DeleteTime = 1.5f; // 경과 시간을 측정하기 위한 타이머
    private float _timer = 0;

   
    private void Update()
    {
        
        _timer += Time.deltaTime;
        if ( _timer >= DeleteTime)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
