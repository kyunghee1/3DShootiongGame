using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� : �����۵��� �������ִ� ������
// ������ ���� -> �����͸� ����, ����, ����, ��ȸ(�˻�), ����
public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance { get; private set; }
    public Text HealthItemCountTextUI;
    public Text StaminaItemCountTextUI;
    public Text BulletItemCountTextUI;


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
        RefreshUI();



    }
    //1. ������ �߰�(����)
    public void AddItem(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                ItemList[i].Count++;
                RefreshUI();
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
                RefreshUI();
                return result;
            }
        }

        return false;
    }
    public void RefreshUI()
    {
        HealthItemCountTextUI.text = $"x{GetItemCount(ItemType.Health)}";
        StaminaItemCountTextUI.text = $"x{GetItemCount(ItemType.Stamina)}";
        BulletItemCountTextUI.text = $"x{GetItemCount(ItemType.Bullet)}";
    }
}
