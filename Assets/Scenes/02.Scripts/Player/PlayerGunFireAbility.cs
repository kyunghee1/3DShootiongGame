using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;




public class PlayerGunFireAbility : MonoBehaviour
{
    private int _currentGunIndex;
    public Gun CurrentGun; //���� ��� �ִ� ��

    //��ǥ: ���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� ���� �߻��ϰ� �ʹ�.
    //�ʿ�Ӽ�
    //-�Ѿ� Ƣ�� ����Ʈ ������
    public ParticleSystem HitEffect;
    //-�߻�  ��Ÿ��
    private float _timer;
    private float _progress;
    private float Duration = 0.2f;

    public List<Gun> GunInventory;


    //�����ڷ�ƾ

    private bool _isReloading = false; //������ ���̳�?
    public GameObject ReloadTextObject;

    private const int DefaultFOV = 60;
    private const int ZoomFOV = 20;
    private bool _isZoomMode = false;

    private const float ZoomInDuration = 0.3f;
    private const float ZoomOutDuration = 0.2f;
    private float _zoomProgress;

    public GameObject CrosshairUI;
    public GameObject CrosshairZoomUI;

    //-�Ѿ� ���� �ؽ�Ʈ UI
    public Text BulletTextUI;

   

    public Image GunImageUI;
    
    private void Start()
    { 
        _currentGunIndex = 0;
        //�Ѿ� ���� �ʱ�ȭ
        RefreshGun();
        RefreshUI();
    }
    public void RefreshUI()
    {
        GunImageUI.sprite = CurrentGun.ProfileImage;
        BulletTextUI.text = $"{CurrentGun.BulletRemainCount:D2}/{CurrentGun.BulletMaxCount}";

        CrosshairUI.SetActive(!_isZoomMode);
        CrosshairZoomUI.SetActive(_isZoomMode);
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
    //�� ��忡 ���� ī�޶� FOV �������ִ� �޼���
    private void RefreshZoomMode()
    {
        _progress += Time.deltaTime / Duration;
        if (!_isZoomMode)
        {
            Camera.main.fieldOfView = DefaultFOV;
            
        }
        else
        {
            Camera.main.fieldOfView = ZoomFOV;
           
        }
       
    }
    private void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }

        //���콺 �� ��ư ������ �� && ���� ����(��)�� ��������
        if (Input.GetMouseButtonDown(2) && CurrentGun.Gtype == GunType.Sniper )
        {
            _isZoomMode = !_isZoomMode; //�� ��� ������
            _zoomProgress = 0f;
           
            RefreshUI();
        }
        if(CurrentGun.Gtype == GunType.Sniper && _zoomProgress < 1)
        {
            if(_isZoomMode) //����
            {
                _zoomProgress += Time.deltaTime / ZoomInDuration;
                Camera.main.fieldOfView = Mathf.Lerp(DefaultFOV, ZoomFOV, _zoomProgress);
            }
            else
            {
                {
                    _zoomProgress += Time.deltaTime / ZoomInDuration;
                    Camera.main.fieldOfView = Mathf.Lerp(ZoomFOV, DefaultFOV, _zoomProgress);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket)) // '['
        {

            // �ڷΰ��� 
            _currentGunIndex--;
            if (_currentGunIndex < 0)
            {
                _currentGunIndex = GunInventory.Count - 1;
            }
            CurrentGun = GunInventory[_currentGunIndex];
            _isZoomMode = false;
            _zoomProgress = 1f;
            RefreshZoomMode();
            RefreshGun();
            RefreshUI();
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket)) // ']'
        {
            // ������ ����
            _currentGunIndex++;
            if (_currentGunIndex >= GunInventory.Count)
            {
                _currentGunIndex = 0;
            }
            CurrentGun = GunInventory[_currentGunIndex];
            _isZoomMode = false;
            _zoomProgress = 1f;
            

            RefreshZoomMode();
            RefreshGun();
            RefreshUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentGunIndex = 0;
            CurrentGun = GunInventory[0];
            _isZoomMode = false;
            _zoomProgress = 1f;
           
            RefreshZoomMode();
            RefreshGun();
            RefreshUI();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentGunIndex = 1;
            CurrentGun = GunInventory[1];
            _isZoomMode = false;
            _zoomProgress = 1f;
            RefreshZoomMode();
            RefreshGun();
            RefreshUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentGunIndex = 2;
            CurrentGun = GunInventory[2];
            _isZoomMode = false;
            _zoomProgress = 1f;
            RefreshZoomMode();
            RefreshGun();
            RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.R) && CurrentGun.BulletRemainCount < CurrentGun.BulletMaxCount)
        {
            if (!_isReloading)
            {
                StartCoroutine(Reload_Coroutine());
                _isReloading = false;
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
            CurrentGun.BulletRemainCount -= 1;
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
                //�ǽ� ���� 18. �������� ���Ϳ��� ���� �� ���� ü�� ��� ��� ����
                IHitable hitObject = hitInfo.collider.GetComponent<IHitable>();
                if (hitObject != null) // ���� �� �ִ� ģ���ΰ���?
                {
                    hitObject.Hit(CurrentGun.Damage);


                }
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
            }
        }
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
   /* private IEnumerator ZoomIn_Coroutine()
    {
        float time = 0.3f;    // ���ϴ� �ð�
        float timer = 0f;     // �ð� ���� ����

        while (true)
        {
            timer += Time.deltaTime / time;
            Camera.main.fieldOfView = Mathf.Lerp(DefaultFOV, ZoomFOV, timer);
            yield return null;

            if (timer > 1f)
            {
                yield break;
            }
        }
    }*/
}




















