using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//���� : �����۵��� �������ִ� ������
// ������ ���� -> �����͸� ����, ����, ����, ��ȸ(�˻�), ����
public class ItemManager : MonoBehaviour
{
    public UnityEvent OnDataChanged;
    //������ ��(��Ʃ��)����
    //�����ڰ� �����ϰ� �ִ� ��Ʃ���� ���°� ��ȭ�� ������
    //��Ʃ���� �����ڿ��� �̺�Ʈ�� �����ϰ�, �����ڵ��� �̺�Ʈ �˸��� �޾� �����ϰ�
    //�ൿ�ϴ� ����
  
    public static ItemManager Instance { get; private set; }
  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }


    }
    public List<Item> ItemList = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        ItemList.Add(new Item(ItemType.Health, 3));// 0: Health
        ItemList.Add(new Item(ItemType.Stamina, 5));//1;Stamina
        ItemList.Add(new Item(ItemType.Bullet, 7));
       


    }
    //1. ������ �߰�(����)
    public void AddItem(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                ItemList[i].Count++;
                if(OnDataChanged != null)
                {
                    OnDataChanged.Invoke();
                }
                break;
            }
        }
    }

    // 2. ������ ���� ��ȸ
    public int GetItemCount(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                return ItemList[i].Count;
            }
        }

        return 0;
    }


    // 3. ������ ���
    public bool TryUseItem(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                bool result = ItemList[i].TryUse();
             
                return result;
            }
        }

        return false;
    }
   
}
