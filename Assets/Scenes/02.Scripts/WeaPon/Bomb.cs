using System;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    //목표: 수류탄 폭발 범위 데미지 기능 구현
    // 필요속성:
    // -범위
    public float ExplsionRadius = 3;
    //구현순서:
    //1. 터질 때
    //2. 범위안에 있는 모든 콜라이더를 찾는다.
    //3. 찾은 콜라이더 중에서 타격 가능한(IHitable) 오브젝트를 찾는다.
    //4. Hit()한다.


    //플레이어를 제외하고 물체에 닿으면( =충돌이 일어나면)
    //자기 자신의 게임 오브젝트를 사라지게 하는 코드
    public GameObject BombEffectPrefab;
    public int Damage = 60;
    private Collider[] _collidedrs = new Collider[10];
    


    //구현순서:
    //1. 터질 때
    // 실습 관제 8. 수류탄이 폭발할 때(사라질 때 ) 폭발 이펙트를 자기 위치에 생성하기
    void OnCollisionEnter(Collision other)
    {

        gameObject.SetActive(false); // 창고에 넣는다.

        GameObject Effect = Instantiate(BombEffectPrefab);
        Effect.transform.position = this.gameObject.transform.position;

        //2. 범위안에 있는 모든 콜라이더를 찾는다.
        int layer = LayerMask.GetMask("Monster") | LayerMask.NameToLayer("Player");
        int count = Physics.OverlapSphereNonAlloc(transform.position, ExplsionRadius, _collidedrs ,layer); //| 비트합 연산자//LayerMask. 1 << 8, 1<<9
        //-> 피직스.오버랩 함수는 특정 영역(radius)안에 있는 특정 레이어들의 게임 오브젝트의
        //콜라이더 컴포넌트들을 모두 찾아 배열로 반환하는 함수
        //영역의 형태: 스피어, 큐브, 캡슐

        //3. 찾은 콜라이더 중에서 타격 가능한(IHitable) 오브젝트를 찾는다.
        for(int i = 0; i < count; i++)
            {
            Collider c = _collidedrs[i];
           IHitable hitable = c.GetComponent<IHitable>();
            if (hitable != null)
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);

                hitable.Hit(damageInfo);
            }
           
        }
        //4. Hit()한다.





    }


}
