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

    public  Texture[] textures; //�������� ������ �ؽ�ó �迭 
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

        // �ǽ� ���� 22. �巳�� ������ �� �ֺ� Hitable�� Monster�� Player���� ������ 70
        //1.���� ���� �� �ݶ��̴� ã��
        int findLayer = LayerMask.GetMask("Player") | LayerMask.GetMask("monster") ;
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, findLayer);
        //2. �ݶ��̴� ������ Hitable ã��
        foreach (Collider c in colliders)
        {
            
            IHitable hitable = null;
            if (c.TryGetComponent<IHitable>(out hitable))
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                //3. ������ �ֱ�
                hitable.Hit(damageInfo);
            }
        }
      // �ǽ����� 23. 
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
   

