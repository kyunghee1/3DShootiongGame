using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;

public class PlayerFire : MonoBehaviour
{
    //목표: 마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던지고 싶다.
    //필요속성
    //- 수류탄 프리팹
    public GameObject BombPrefab;
    //-수류탄 던지는 위치
    public Transform FirePosition;
    //수류탄 던지는 파워
    public float ThrowPower = 15f;

    public int BombRemainCount;
    public int BombMaxCount = 3;
    public Text BombTextUI;

    
    public GameObject BombEffect;
    ParticleSystem BombEffectParticle;


    
   
    private List<GameObject> BombPool;
    public int BombPoolsize =5;
   

   



    

   //구현순서:
   //1. 마우스 오른쪽 버튼을 감지
   //2. 수류탄 던지는 위치에다가 수류탄 생성
   //3. 시선이 바라보는 방향으로 수류탄 투척

    void Start()
    {
        BombRemainCount = BombMaxCount;
        RefreshUI();
        BombPool = new List<GameObject>();
        for(int i = 0; i < BombRemainCount; i++)
        {
            GameObject bombObject = Instantiate(BombPrefab); //1. 생성
            bombObject.SetActive(false);                     //2. 비활성화
            BombPool.Add(bombObject);                        //3. 창고에 집어 넣는다.
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
        //구현순서:
        //1. 마우스 오른쪽 버튼을 감지
        if (Input.GetMouseButtonDown(1) && BombRemainCount > 0)
        {


            BombRemainCount--;
            RefreshUI();
            
           

            
          

            //2. 창고에서 수류탄을 꺼낸다음  던지는 위치로 조절
            GameObject bomb = null;
            for (int i = 0; i < BombRemainCount; i++) //1.창고를 뒤진다.
            {
                if (BombPool[i].activeInHierarchy == false) //2. 쓸만한 폭탄을 찾는다
                {
                    bomb = BombPool[i];
                    bomb.SetActive(true); //3.꺼낸다
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



            //3. 시선이 바라보는 방향(카메라가 바라보는 방향 =카메라의 전방)으로 수류탄 투척
            Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Camera.main.transform.forward * 15, ForceMode.Impulse);
        }

    }
  
        

}
