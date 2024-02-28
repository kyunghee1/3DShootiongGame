using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttackEvent : MonoBehaviour
{
    private PlayerBombFireAbility _owner;

    // Start is called before the first frame update
    void Start()
    {
        _owner = GetComponentInParent<PlayerBombFireAbility>();
    }
    public void AttackEvnent()
    {
        _owner.BombFire();
    }
   
    
}
