using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;




public class PlayerGunFire : MonoBehaviour
{

    public Gun CurrentGun; //���� ��� �ִ� ��

    //��ǥ: ���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� ���� �߻��ϰ� �ʹ�.
    //�ʿ�Ӽ�
    //-�Ѿ� Ƣ�� ����Ʈ ������
    public ParticleSystem HitEffect;
    //-�߻�  ��Ÿ��
    private float _timer;

    public List<Gun> GunInventory;


    //�����ڷ�ƾ

    private bool _isReloading = false;
    public GameObject ReloadTextObject;

    //-�Ѿ� ����
    public int BulletRemainCount;
    public int BulletMaxCount = 30;





    //-�Ѿ� ���� �ؽ�Ʈ UI
    public Text BulletTextUI;



    private void Start()
    {
        RefreshGun();

        RefreshUI();

    }
    private void RefreshUI()
    {
        BulletTextUI.text = $"{CurrentGun.BulletRemainCount}/{CurrentGun.BulletMaxCount}";
    }

    private IEnumerator Reload_Coroutine()
    {
        _isReloading = true;

        // RŰ ������ 1.5�� �� ������, (�߰��� �� ��� ������ �ϸ� ������ ���)
        yield return new WaitForSeconds(CurrentGun.ReloadTime);
        CurrentGun.BulletRemainCount = CurrentGun.BulletMaxCount;
        RefreshUI();

        _isReloading = false;
    }

    private void RefreshGun()
    {
        foreach (Gun gun in GunInventory)
        {
            /**if (gun == CurrentGun)
            {
                gun.gameObject.SetActive(true);
            }
            else
            {
                gun.gameObject.SetActive(false);
            }**/
            gun.gameObject.SetActive(gun == CurrentGun);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentGun = GunInventory[0];
            RefreshGun();
            RefreshUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentGun = GunInventory[1];
            RefreshGun();
            RefreshUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentGun = GunInventory[2];
            RefreshGun();
            RefreshUI();
        }

        if (Input.GetKeyDown(KeyCode.R) && CurrentGun.BulletRemainCount < CurrentGun.BulletMaxCount)
        {
            if (!_isReloading)
            {
                StartCoroutine(Reload_Coroutine());
            }
        }
        ReloadTextObject.SetActive(_isReloading);

        _timer += Time.deltaTime;


        //��������
        //1. ���࿡ ���콺 ���� ��ư�� ������ 
        if (Input.GetMouseButton(0) && _timer >= CurrentGun.FireCooltime && CurrentGun.BulletRemainCount > 0)
        {
            if (_isReloading)
            {
                StopAllCoroutines();
                Debug.Log("�������� ���");
                _isReloading = false;
            }
            BulletRemainCount -= 1;
            RefreshUI();
            _timer = 0;

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
                if (hitObject != null) // ���� �� �ִ� ģ���ΰ���?
                {
                    hitObject.Hit(CurrentGun.Damage);


                }
            }
        }
    }
}




















