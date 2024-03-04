using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Drum : MonoBehaviour, IHitable
{
    public int _hitCount = 0;
    public GameObject ExplosionPaticlePrefab;
    private Rigidbody _rigidbody;
    public float UpPower = 50f;

    public int Damage = 70;
    public float ExplosionRadius = 10f;

    private bool _isExplosion = false;

  
    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Hit(DamageInfo damageInfo)
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

        GameObject explosion = Instantiate(ExplosionPaticlePrefab);
        explosion.transform.position = this.transform.position;
        _rigidbody.AddForce(Vector3.up * UpPower, ForceMode.Impulse);
        _rigidbody.AddTorque(new Vector3(1, 0, 1) * UpPower / 2f);

        // �ǽ� ���� 22. �巳�� ������ �� �ֺ� Hitable�� Monster�� Player���� ������ 70
        // 1. ���� ���� �� �ݶ��̴� ã��
        int findLayer = LayerMask.GetMask("Player") | LayerMask.GetMask("Monster");
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, findLayer);

        // 2. �ݶ��̴� ������ Hitable ã��
        foreach (Collider c in colliders)
        {
            IHitable hitable = null;
            if (c.TryGetComponent<IHitable>(out hitable))
            {
                // 3. ������ �ֱ�
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                hitable.Hit(damageInfo);
            }
        }

        // �ǽ� ���� 23. �巳�� ������ �� �ֺ� �巳�뵵 ���� ���ߵǰ� ����
        int environmentLayer = LayerMask.GetMask("Environment");
        Collider[] environmentColliders = Physics.OverlapSphere(transform.position, ExplosionRadius, environmentLayer);
        foreach (Collider c in environmentColliders)
        {
            Drum drum = null;
            if (c.TryGetComponent<Drum>(out drum))
            {
                // �ֺ� �巳 ����
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
   

