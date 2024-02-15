using System;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    //플레이어를 제외하고 물체에 닿으면( =충돌이 일어나면)
    //자기 자신의 게임 오브젝트를 사라지게 하는 코드
    public GameObject BombEffectPrefab;
    
    // 실습 관제 8. 수류탄이 폭발할 때(사라질 때 ) 폭발 이펙트를 자기 위치에 생성하기
    void OnCollisionEnter(Collision other)
    {

        gameObject.SetActive(false); // 창고에 넣는다.
        GameObject Effect = Instantiate(BombEffectPrefab);
        Effect.transform.position = this. transform.position;


        


    }


}
