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
                //todo :������ ȿ���� ���
                //todo: ��ƼŬ �ý��� ���
                ItemManager.Instance.RefreshUI();
            }
            else
            {
                //todo: �˸�â(�������� �����մϴ�)
            }
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            ItemManager.Instance.TryUseItem(ItemType.Stamina);
            ItemManager.Instance.RefreshUI();
        }
        else if(Input.GetKeyDown(KeyCode.U))
        {
            // �Ѿ� ������ ���
            ItemManager.Instance.TryUseItem(ItemType.Bullet);
            ItemManager.Instance.RefreshUI();
        }
    }
}
