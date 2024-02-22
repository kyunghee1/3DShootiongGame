using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;

    // Todo 1. ������ �������� 3��(Health, Stamina, Bullet) �����. (�����̳� ���� �ٸ����ؼ� �����ǰ�)
    // Todo 2. �÷��̾�� ���� �Ÿ��� �Ǹ� �������� �Ծ����� �������.
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // 1. ������ �Ŵ���(�κ��丮)�� �߰��ϰ�,
            ItemManager.Instance.AddItem(ItemType);
            ItemManager.Instance.RefreshUI();

            // 2. �������.
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
