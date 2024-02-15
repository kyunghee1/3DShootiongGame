using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;

public class PlayerFire : MonoBehaviour
{
    //��ǥ: ���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ����ź�� ������ �ʹ�.
    //�ʿ�Ӽ�
    //- ����ź ������
    public GameObject BombPrefab;
    //-����ź ������ ��ġ
    public Transform FirePosition;
    //����ź ������ �Ŀ�
    public float ThrowPower = 15f;

    public int BombRemainCount;
    public int BombMaxCount = 3;
    public Text BombTextUI;

    
    public GameObject BombEffect;
    ParticleSystem BombEffectParticle;


    
   
    private List<GameObject> BombPool;
    public int BombPoolsize =5;
   

   



    

   //��������:
   //1. ���콺 ������ ��ư�� ����
   //2. ����ź ������ ��ġ���ٰ� ����ź ����
   //3. �ü��� �ٶ󺸴� �������� ����ź ��ô

    void Start()
    {
        BombRemainCount = BombMaxCount;
        RefreshUI();
        BombPool = new List<GameObject>();
        for(int i = 0; i < BombRemainCount; i++)
        {
            GameObject bombObject = Instantiate(BombPrefab); //1. ����
            bombObject.SetActive(false);                     //2. ��Ȱ��ȭ
            BombPool.Add(bombObject);                        //3. â�� ���� �ִ´�.
        }

        BombEffectParticle = BombEffect.GetComponent<ParticleSystem>();

    }
    private void RefreshUI()
    {
        BombTextUI.text = $"{BombRemainCount}/{BombMaxCount}";
    }



    // Update is called once per frame
    void Update()
    {
        //��������:
        //1. ���콺 ������ ��ư�� ����
        if (Input.GetMouseButtonDown(1) && BombRemainCount > 0)
        {


            BombRemainCount--;
            RefreshUI();
            
           

            
          

            //2. â���� ����ź�� ��������  ������ ��ġ�� ����
            GameObject bomb = null;
            for (int i = 0; i < BombRemainCount; i++) //1.â�� ������.
            {
                if (BombPool[i].activeInHierarchy == false) //2. ������ ��ź�� ã�´�
                {
                    bomb = BombPool[i];
                    bomb.SetActive(true); //3.������
                    break;
                }
            }
            bomb.transform.position = FirePosition.position;
            if (Input.GetMouseButton(0))
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                RaycastHit hitINfo = new RaycastHit();

                if (Physics.Raycast(ray, out hitINfo))
                {
                    BombEffect.transform.position = hitINfo.point;
                    BombEffectParticle.Play();
                }
            }



            //3. �ü��� �ٶ󺸴� ����(ī�޶� �ٶ󺸴� ���� =ī�޶��� ����)���� ����ź ��ô
            Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Camera.main.transform.forward * 15, ForceMode.Impulse);
        }

    }
  
        

}
