using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackEvent : MonoBehaviour
{
    private Monster _owner;

    // Start is called before the first frame update
    void Start()
    {
        _owner = GetComponentInParent<Monster>();
    }
    public void AttackEvnent()
    {
        Debug.Log("어택 발생");
        _owner.PlayerAttack();
    }
   
}