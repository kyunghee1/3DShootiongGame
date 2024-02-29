using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Drum : MonoBehaviour, IHitable
{
    public int _hitCount = 0;
    public GameObject ExplosionParticlePrefabs;
    private Rigidbody _rigidbody;
    public float UpPower = 50f;

    public int Damage = 70;
    public float ExplosionRadius = 10f;

    private bool _isExplosion = false;

    public  Texture[] textures; //무작위로 적용할 텍스처 배열 
    private new MeshRenderer renderer;
    public float radius = 5f;

    public void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        int index= Random.Range(0, textures.Length);
      // renderer.material.mainTexture = textures[index];
       
       
       
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Hit(DamageInfo damage)
    {
        _hitCount += 1;
        if (_hitCount >= 3)
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
            
            IHitable hitable = null;
            if (c.TryGetComponent<IHitable>(out hitable))
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                //3. 데미지 주기
                hitable.Hit(damageInfo);
            }
        }
      // 실습과제 23. 
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
        ItemObjectFactory.Instance.MakePercent(transform.position);
        StartCoroutine(Kill_Coroutine());
    }

    private IEnumerator Kill_Coroutine()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
   

