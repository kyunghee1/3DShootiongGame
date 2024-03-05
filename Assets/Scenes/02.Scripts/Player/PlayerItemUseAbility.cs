using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemUseAbility : MonoBehaviour
{
   
   
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }
        //�ǽ����� 31. T/Y/U ��ư ������ ������ ��� ����
        if (Input.GetKeyDown(KeyCode.T))
        {
            // ü�� ������ ���
           bool result = ItemManager.Instance.TryUseItem(ItemType.Health);
            if(result)
            {
                //todo :������ ȿ���� ���
                //todo: ��ƼŬ �ý��� ���  
            }
            else
            {
                //todo: �˸�â(�������� �����մϴ�)
            }
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            ItemManager.Instance.TryUseItem(ItemType.Stamina);
           
        }
        else if(Input.GetKeyDown(KeyCode.U))
        {
            // �Ѿ� ������ ���
            ItemManager.Instance.TryUseItem(ItemType.Bullet);
           
        }
    }
}
