using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //�÷��̾ �����ϰ� ��ü�� ������( =�浹�� �Ͼ��)
    //�ڱ� �ڽ��� ���� ������Ʈ�� ������� �ϴ� �ڵ�
    public GameObject BombEffectPrefab;
    
    void OnCollisionEnter(Collision other)
    {
        GameObject Effect = Instantiate(BombEffectPrefab);
        Effect.transform.position = this. transform.position;


        Destroy(gameObject);


    }


}
