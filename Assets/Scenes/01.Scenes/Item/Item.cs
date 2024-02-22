using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum ItemType
{
    Health, //ü���� ������
    Stamina, //���׹̳� ������
    Bullet  // ���� ��� �ִ� �Ѿ��� 
}
public class Item
{
    public ItemType ItemType;
    public int Count;

    public Item(ItemType itemTypem, int count)
    {
        ItemType = itemTypem;
        Count = count;
    }
   public bool TryUse()
    {
        if (Count == 0)
        {
            return false;
        }
        Count -= 1;
        switch(ItemType)
        {
            case ItemType.Health:
            {
                //Todo :�÷��̾� ü�� ������
                PlayerMoveAbility playerMoveAbility = GameObject.FindWithTag("player").GetComponent<PlayerMoveAbility>();
                playerMoveAbility.Health = playerMoveAbility.Maxhealth;
                break;
            }
            case ItemType.Stamina:
            {
                //Todo :�÷��̾� ���¹̳� ������
                PlayerMoveAbility playerMoveAbility = GameObject.FindWithTag("Player").GetComponent< PlayerMoveAbility>();
                playerMoveAbility.Stamina = PlayerMoveAbility.MaxStamina;
                break;
            }
            case ItemType.Bullet:
            {
                //Todo :�÷��̾� �Ѿ� ������
                PlayerGunFireAbility ability = GameObject.FindWithTag("Player").GetComponent<PlayerGunFireAbility>();
                ability.CurrentGun.BulletRemainCount = ability.CurrentGun.BulletMaxCount;
                ability.RefreshUI();
                break;
            }
        }
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
