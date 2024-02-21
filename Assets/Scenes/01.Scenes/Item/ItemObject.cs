using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;
  
    //todo 1. 아이템 프리팹을 3개(Health, Stamina, Bullet) 만든다.(도형이나 색깔 다르게해서 구현)
    //todo2. 플레이어와 일정 거리가 되면 아이템이 먹어지고 사라진다.
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            //1. 아이템 매니저(인벤토리)에 추가하고,
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
            //2. 사라진다.
            Destroy(gameObject);
        }
    }

    // 실습 과제 31. 몬스터가 죽으면 아이템이 드랍(Health: 20%, Stamina: 20%, Bullet: 10%)
    // 실습 과제 32. 일정 거리가 되면 아이템이 베지어 곡선으로 날라오게 하기
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
