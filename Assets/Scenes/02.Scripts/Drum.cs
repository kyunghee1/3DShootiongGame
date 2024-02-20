using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UIElements;

public class Drum : MonoBehaviour, IHitable
{
    public int _hitCount = 0;

    public GameObject ExplosionParticlePrefabs;
    private Rigidbody _rigidbody;
    public float UpPower = 50f;

    public int Damage = 70;
    public float ExplosionRadius = 10f;

    private bool _isExplosion = false;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
      
    }
    
    
    public void Hit (int damage)
    {
        _hitCount += 1;
        if (_hitCount >=3)
        {

            Explosion();

        }
    }
 
    private void Explosion()
    {
        if (_isExplosion)
        {
            return;
        }
        _isExplosion = true;

        GameObject explosion = Instantiate(ExplosionParticlePrefabs);
        explosion.transform.position = this.transform.position;
        _rigidbody.AddForce(Vector3.up * UpPower, ForceMode.Impulse);
        _rigidbody.AddTorque(new Vector3(1, 0, 1) * UpPower / 2f);

        // 실습 과제 22. 드럼통 폭발할 때 주변 Hitable한 Monster와 Player에게 데미지 70
        //1.폭발 범위 내 콜라이더 찾기
        int findLayer = LayerMask.GetMask("Player") | LayerMask.GetMask("monster") ;
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, findLayer);
        //2. 콜라이더 내에서 Hitable 찾기
        foreach (Collider c in colliders)
        {
            int i = 0;
            IHitable hitable = null;
            if (c.TryGetComponent<IHitable>(out hitable) | i < colliders.Length)
            {
                //3. 데미지 주기
                hitable.Hit(Damage);
            
            }

        }
        Destroy(gameObject);
        int environmentLayer = LayerMask.GetMask("Environment");
        Collider[] environmentColliders = Physics.OverlapSphere(transform.position, ExplosionRadius, environmentLayer);
        foreach (Collider c in environmentColliders)
        {
            Drum drum = null;
            if (c.TryGetComponent(out drum))
            {
                drum.Explosion();
            }
        }
        StartCoroutine(Kill_Coroutine());
    }

    private IEnumerator Kill_Coroutine()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
   

