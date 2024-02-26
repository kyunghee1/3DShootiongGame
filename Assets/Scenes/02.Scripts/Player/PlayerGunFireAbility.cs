using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;




public class PlayerGunFireAbility : MonoBehaviour
{
    private int _currentGunIndex;
    public Gun CurrentGun; //현재 들고 있는 총

    //목표: 마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 총을 발사하고 싶다.
    //필요속성
    //-총알 튀는 이펙트 프리팹
    public ParticleSystem HitEffect;
    //-발사  쿨타임
    private float _timer;
    private float _progress;
    private float Duration = 0.2f;

    public List<Gun> GunInventory;


    //장전코루틴

    private bool _isReloading = false; //재장전 중이냐?
    public GameObject ReloadTextObject;

    private const int DefaultFOV = 60;
    private const int ZoomFOV = 20;
    private bool _isZoomMode = false;

    private const float ZoomInDuration = 0.3f;
    private const float ZoomOutDuration = 0.2f;
    private float _zoomProgress;

    public GameObject CrosshairUI;
    public GameObject CrosshairZoomUI;

    //-총알 개수 텍스트 UI
    public Text BulletTextUI;

   

    public Image GunImageUI;
    
    private void Start()
    { 
        _currentGunIndex = 0;
        //총알 개수 초기화
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

        // R키 누르면 1.5초 후 재장전, (중간에 총 쏘는 행위를 하면 재장전 취소)
        yield return new WaitForSeconds(CurrentGun.ReloadTime);
        CurrentGun.BulletRemainCount = CurrentGun.BulletMaxCount;
        RefreshUI();

        _isReloading = false;
    }
    //줌 모드에 따라 카메라 FOV 수정해주는 메서드
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

        //마우스 휠 버튼 눌렀을 때 && 현재 무기(총)이 스나이퍼
        if (Input.GetMouseButtonDown(2) && CurrentGun.Gtype == GunType.Sniper )
        {
            _isZoomMode = !_isZoomMode; //줌 모드 뒤집기
            _zoomProgress = 0f;
           
            RefreshUI();
        }
        if(CurrentGun.Gtype == GunType.Sniper && _zoomProgress < 1)
        {
            if(_isZoomMode) //줌인
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

            // 뒤로가기 
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
            // 앞으로 가기
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

        //구현순서
        //1. 만약에 마우스 왼쪽 버튼을 누르면 
        if (Input.GetMouseButton(0) && _timer >= CurrentGun.FireCooltime && CurrentGun.BulletRemainCount > 0)
        {
            if (_isReloading)
            {
                StopAllCoroutines();
                Debug.Log("재장전중 취소");
                _isReloading = false;
            }
            CurrentGun.BulletRemainCount -= 1;
            RefreshUI();
            _timer = 0;

            //2. 레이(광선)을 생성하고, 위치와 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //3. 레이를 발사한다.
            //4. 레이가 부딪힌 대상의 정보를 받아온다
            RaycastHit hitInfo;
            bool IsHit = Physics.Raycast(ray, out hitInfo);
            if (IsHit)
            {
                //실습 과제 18. 레이저를 몬스터에게 맞출 시 몬스터 체력 닳는 기능 구현
                IHitable hitObject = hitInfo.collider.GetComponent<IHitable>();
                if (hitObject != null) // 때릴 수 있는 친구인가요?
                {
                    hitObject.Hit(CurrentGun.Damage);


                }
                /*f(hitInfo.collider.CompareTag("Monster"))
                   {
                       Monster monster = hitInfo.collider.GetComponent<Monster>();
                       monster.Hit(Damage);
                   }*/
                //5. 부딪힌 위치에 (총알이 튀는) 이펙트를 생성한다.
                HitEffect.gameObject.transform.position = hitInfo.point;

                //6. 이펙트가 쳐다보는 방향을 부딪힌 위치의 법선 벡터로 한다.
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
        float time = 0.3f;    // 원하는 시간
        float timer = 0f;     // 시간 누적 변수

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




















