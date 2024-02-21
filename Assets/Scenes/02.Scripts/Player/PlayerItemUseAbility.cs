using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemUseAbility : MonoBehaviour
{
   
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
           bool result = ItemManager.Instance.TryUseItem(ItemType.Health);
            if(result)
            {
                //todo :아이템 효과음 재생
                //todo: 파티클 시스템 재생
                ItemManager.Instance.RefreshUI();
            }
            else
            {
                //todo: 알림창(아이템이 부족합니다)
            }
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            ItemManager.Instance.TryUseItem(ItemType.Stamina);
            ItemManager.Instance.RefreshUI();
        }
        else if(Input.GetKeyDown(KeyCode.U))
        {
            // 총알 아이템 사용
            ItemManager.Instance.TryUseItem(ItemType.Bullet);
            ItemManager.Instance.RefreshUI();
        }
    }
}
