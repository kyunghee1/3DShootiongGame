using System;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    //�÷��̾ �����ϰ� ��ü�� ������( =�浹�� �Ͼ��)
    //�ڱ� �ڽ��� ���� ������Ʈ�� ������� �ϴ� �ڵ�
    public GameObject BombEffectPrefab;
    
    // �ǽ� ���� 8. ����ź�� ������ ��(����� �� ) ���� ����Ʈ�� �ڱ� ��ġ�� �����ϱ�
    void OnCollisionEnter(Collision other)
    {

        gameObject.SetActive(false); // â�� �ִ´�.
        GameObject Effect = Instantiate(BombEffectPrefab);
        Effect.transform.position = this. transform.position;


        


    }


}
