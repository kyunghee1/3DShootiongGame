using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

   public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Hit (int damage)
    {
        _hitCount += 1;
        if (_hitCount >=3)
        {
          
            Destroy(gameObject, damage);
            Kill();
        }
    }
    private void Kill()
    {
        GameObject explosion = Instantiate(ExplosionParticlePrefabs);

        explosion.transform.position = this.transform.position;
        _rigidbody.AddForce(Vector3.up * UpPower, ForceMode.Impulse);
        _rigidbody.AddTorque(new Vector3(1, 0, 1) * UpPower / 2f);
    
        //1.폭발 범위 내 콜라이더 찾기
        int findLayer = LayerMask.GetMask("Player") | LayerMask.GetMask("monster");
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, findLayer);
        //2. 콜라이더 내에서 Hitable 찾기
        foreach (Collider c in colliders)
        {
            IHitable hitable = null;
            if (c.TryGetComponent<IHitable>(out hitable))
            {
                //3. 데미지 주기
                hitable.Hit(Damage);
            }

        }
        Destroy(gameObject, 3f);
    }
        
   
}
