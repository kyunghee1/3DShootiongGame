using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerGunFire : MonoBehaviour
{

    //��ǥ: ���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� ���� �߻��ϰ� �ʹ�.
    //�ʿ�Ӽ�
    //-�Ѿ� Ƣ�� ����Ʈ ������
    public ParticleSystem HitEffect;
    //-�߻�  ��Ÿ��
    float _Timer;
    public float FireCOOL_Time = 0.2f;

    //�����ڷ�ƾ
    private Coroutine _reloadCoroutine;
    private bool _isReloading;

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
   
    private IEnumerator Reload()
    {
        Debug.Log("��������");
        _isReloading = true;
        yield return new WaitForSeconds(1.5f);
        _isReloading = false;
        BulletRemainCount = BulletMaxCount;
        RefreshUI();

        Debug.Log("��������");
    }




    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
        {
            _reloadCoroutine = StartCoroutine(Reload());
            RefreshUI() ;
        }

        _Timer += Time.deltaTime;
        //��������
        //1. ���࿡ ���콺 ���� ��ư�� ������ 
        if (Input.GetMouseButton(0) && _Timer >= FireCOOL_Time && BulletMaxCount > 0)
        {
            if (_isReloading )
            {
                StopCoroutine(_reloadCoroutine);
                Debug.Log("�������� ���");
                _isReloading=false;
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
                //5. �ε��� ��ġ�� (�Ѿ��� Ƣ��) ����Ʈ�� �����Ѵ�.
                HitEffect.gameObject.transform.position = hitInfo.point;
               
                //6. ����Ʈ�� �Ĵٺ��� ������ �ε��� ��ġ�� ���� ���ͷ� �Ѵ�.
                HitEffect.gameObject.transform.forward = hitInfo.normal;
                HitEffect.Play();

            }
        }
    }
   
}











