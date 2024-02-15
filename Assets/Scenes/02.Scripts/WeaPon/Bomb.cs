using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //플레이어를 제외하고 물체에 닿으면( =충돌이 일어나면)
    //자기 자신의 게임 오브젝트를 사라지게 하는 코드
    public GameObject BombEffectPrefab;
    
    void OnCollisionEnter(Collision other)
    {
        GameObject Effect = Instantiate(BombEffectPrefab);
        Effect.transform.position = this. transform.position;


        Destroy(gameObject);


    }


}
