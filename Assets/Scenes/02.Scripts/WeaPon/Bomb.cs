using System;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    //��ǥ: ����ź ���� ���� ������ ��� ����
    // �ʿ�Ӽ�:
    // -����
    public float ExplsionRadius = 3;
    //��������:
    //1. ���� ��
    //2. �����ȿ� �ִ� ��� �ݶ��̴��� ã�´�.
    //3. ã�� �ݶ��̴� �߿��� Ÿ�� ������(IHitable) ������Ʈ�� ã�´�.
    //4. Hit()�Ѵ�.


    //�÷��̾ �����ϰ� ��ü�� ������( =�浹�� �Ͼ��)
    //�ڱ� �ڽ��� ���� ������Ʈ�� ������� �ϴ� �ڵ�
    public GameObject BombEffectPrefab;
    public int Damage = 60;
    


    //��������:
    //1. ���� ��
    // �ǽ� ���� 8. ����ź�� ������ ��(����� �� ) ���� ����Ʈ�� �ڱ� ��ġ�� �����ϱ�
    void OnCollisionEnter(Collision other)
    {

        gameObject.SetActive(false); // â�� �ִ´�.

        GameObject Effect = Instantiate(BombEffectPrefab);
        Effect.transform.position = this.gameObject.transform.position;

        //2. �����ȿ� �ִ� ��� �ݶ��̴��� ã�´�.
        int layer = LayerMask.GetMask("Monster") | LayerMask.NameToLayer("Player");
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplsionRadius, layer); //| ��Ʈ�� ������//LayerMask. 1 << 8, 1<<9
        //-> ������.������ �Լ��� Ư�� ����(radius)�ȿ� �ִ� Ư�� ���̾���� ���� ������Ʈ��
        //�ݶ��̴� ������Ʈ���� ��� ã�� �迭�� ��ȯ�ϴ� �Լ�
        //������ ����: ���Ǿ�, ť��, ĸ��

        //3. ã�� �ݶ��̴� �߿��� Ÿ�� ������(IHitable) ������Ʈ�� ã�´�.
        foreach(Collider collider in colliders)
            {
            IHitable hitable = collider.GetComponent<IHitable>();
            if (hitable != null)
            {
                hitable.Hit(Damage);
            }
           
        }
        //4. Hit()�Ѵ�.





    }


}
