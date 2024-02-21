using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;
  
    //todo 1. ������ �������� 3��(Health, Stamina, Bullet) �����.(�����̳� ���� �ٸ����ؼ� ����)
    //todo2. �÷��̾�� ���� �Ÿ��� �Ǹ� �������� �Ծ����� �������.
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            //1. ������ �Ŵ���(�κ��丮)�� �߰��ϰ�,
            //float distance = Vector3.Distance(collider.transform.position, transform.position);
            //Debug.Log(distance);

            switch (ItemType)
            {
                case ItemType.Health:
                    ItemManagerr._instance.inventory[0].Count++;
                    Debug.Log(ItemManagerr._instance.inventory[0].Count);
                    break;
                case ItemType.Stamina:
                    ItemManagerr._instance.inventory[1].Count++;
                    Debug.Log(ItemManagerr._instance.inventory[1].Count);
                    break;
                case ItemType.Bullet:
                    ItemManagerr._instance.inventory[2].Count++;
                    Debug.Log(ItemManagerr._instance.inventory[2].Count);
                    break;
            }
            //2. �������.
            Destroy(gameObject);
        }
    }

    // �ǽ� ���� 31. ���Ͱ� ������ �������� ���(Health: 20%, Stamina: 20%, Bullet: 10%)
    // �ǽ� ���� 32. ���� �Ÿ��� �Ǹ� �������� ������ ����� ������� �ϱ�
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
