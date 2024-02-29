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
    public DeleType DeleType; //�ı������� �ð�

    public float DeleteTime = 1.5f; // ��� �ð��� �����ϱ� ���� Ÿ�̸�
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
