using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerGunFire : MonoBehaviour
{

    //목표: 마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 총을 발사하고 싶다.
    //필요속성
    //-총알 튀는 이펙트 프리팹
    public ParticleSystem HitEffect;
    //-발사  쿨타임
    float _Timer;
    public float FireCOOL_Time = 0.2f;

    //장전코루틴
    private Coroutine _reloadCoroutine;
    private bool _isReloading;

    //-총알 개수
    public int BulletRemainCount;
    public int BulletMaxCount = 30;


    //-총알 개수 텍스트 UI
    public Text BulletTextUI;



    private void Start()
    {
        // 총알 개수 초기화
        BulletRemainCount = BulletMaxCount;
        RefreshUI();
        
    }
    private void RefreshUI()
    {
        BulletTextUI.text = $"{BulletRemainCount}/{BulletMaxCount}";
     }
   
    private IEnumerator Reload()
    {
        Debug.Log("재장전중");
        _isReloading = true;
        yield return new WaitForSeconds(1.5f);
        _isReloading = false;
        BulletRemainCount = BulletMaxCount;
        RefreshUI();

        Debug.Log("재장전끝");
    }




    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
        {
            _reloadCoroutine = StartCoroutine(Reload());
            RefreshUI() ;
        }

        _Timer += Time.deltaTime;
        //구현순서
        //1. 만약에 마우스 왼쪽 버튼을 누르면 
        if (Input.GetMouseButton(0) && _Timer >= FireCOOL_Time && BulletMaxCount > 0)
        {
            if (_isReloading )
            {
                StopCoroutine(_reloadCoroutine);
                Debug.Log("재장전중 취소");
                _isReloading=false;
            }
            BulletRemainCount -= 1;
            RefreshUI();
            _Timer = 0;

            //2. 레이(광선)을 생성하고, 위치와 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //3. 레이를 발사한다.
            //4. 레이가 부딪힌 대상의 정보를 받아온다
            RaycastHit hitInfo;
            bool IsHit = Physics.Raycast(ray, out hitInfo);
            if (IsHit)
            {
                //5. 부딪힌 위치에 (총알이 튀는) 이펙트를 생성한다.
                HitEffect.gameObject.transform.position = hitInfo.point;
               
                //6. 이펙트가 쳐다보는 방향을 부딪힌 위치의 법선 벡터로 한다.
                HitEffect.gameObject.transform.forward = hitInfo.normal;
                HitEffect.Play();

            }
        }
    }
   
}











