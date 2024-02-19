using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerGunFire : MonoBehaviour
{
    public int Damage = 1;
    //��ǥ: ���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� ���� �߻��ϰ� �ʹ�.
    //�ʿ�Ӽ�
    //-�Ѿ� Ƣ�� ����Ʈ ������
    public ParticleSystem HitEffect;
    //-�߻�  ��Ÿ��
    float _Timer;
    public float FireCOOL_Time = 0.2f;

    //�����ڷ�ƾ
    private const float RELOAD_Time = 1.5f;
    private bool _isReloading;
    public GameObject ReloadTextObject;

    //-�Ѿ� ����
    public int BulletRemainCount;
    public int BulletMaxCount = 30;


    //-�Ѿ� ���� �ؽ�Ʈ UI
    public Text BulletTextUI;



    private void Start()
    {
        // �Ѿ� ���� �ʱ�ȭ
        BulletRemainCount = BulletMaxCount;
        RefreshUI();

    }
    private void RefreshUI()
    {
        BulletTextUI.text = $"{BulletRemainCount}/{BulletMaxCount}";
    }

    private IEnumerator Reload_Coroutine()
    {
        Debug.Log("��������");
        _isReloading = true;
        yield return new WaitForSeconds(RELOAD_Time);
        _isReloading = false;
        BulletRemainCount = BulletMaxCount;
        RefreshUI();

        Debug.Log("��������");
    }




    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!_isReloading)
            {
                StartCoroutine(Reload_Coroutine());
            }
        }
        ReloadTextObject.SetActive(_isReloading);

        _Timer += Time.deltaTime;
        //��������
        //1. ���࿡ ���콺 ���� ��ư�� ������ 
        if (Input.GetMouseButton(0) && _Timer >= FireCOOL_Time && BulletRemainCount > 0)
        {
            if (_isReloading)
            {
                StopAllCoroutines();
                Debug.Log("�������� ���");
                _isReloading = false;
            }
            BulletRemainCount -= 1;
            RefreshUI();
            _Timer = 0;

            //2. ����(����)�� �����ϰ�, ��ġ�� ������ �����Ѵ�.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //3. ���̸� �߻��Ѵ�.
            //4. ���̰� �ε��� ����� ������ �޾ƿ´�
            RaycastHit hitInfo;
            bool IsHit = Physics.Raycast(ray, out hitInfo);
            if (IsHit)
            {
             /*f(hitInfo.collider.CompareTag("Monster"))
                {
                    Monster monster = hitInfo.collider.GetComponent<Monster>();
                    monster.Hit(Damage);
                }*/
                //5. �ε��� ��ġ�� (�Ѿ��� Ƣ��) ����Ʈ�� �����Ѵ�.
                HitEffect.gameObject.transform.position = hitInfo.point;

                //6. ����Ʈ�� �Ĵٺ��� ������ �ε��� ��ġ�� ���� ���ͷ� �Ѵ�.
                HitEffect.gameObject.transform.forward = hitInfo.normal;
                HitEffect.Play();
                //�ǽ� ���� 18. �������� ���Ϳ��� ���� �� ���� ü�� ��� ��� ����
                IHitable hitObject = hitInfo.collider.GetComponent<IHitable>();
                if(hitObject != null) // ���� �� �ִ� ģ���ΰ���?
                {
                    hitObject.Hit(Damage);

                   
                }

            }


        }

    }
  
}


    
  
 











