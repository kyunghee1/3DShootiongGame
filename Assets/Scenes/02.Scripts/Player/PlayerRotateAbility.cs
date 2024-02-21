using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateAbility : MonoBehaviour
{
    // 목표 : 마우스를 조작하면 플레이어를 좌우방향으로 회전시키고 싶다.
    // 필요속성:
    //회전속도
    public float RotationSpeed = 200;//초당 200 도까지 회전 가능한 속도
    //누적할 x 각도
    private float _mx =0;
   

   
  

  
    void Update()
    {
        if (!CameraManager.Focus)
        {
            return;
        }
        //1. 마우스 입력(drag)받는다
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
       

        //2. 마우스 입력 값만큼 x값을 누적한다.
        _mx += mouseX * RotationSpeed * Time.deltaTime;
        //-mx = Mathf.Clamp(_mx, -270f, 270f);
        //Vector3 dir = new Vector3(-mouse_Y, mouse_X, 0);

        //3.누적한 값에 따라 회전한다.
        transform.eulerAngles = new Vector3(x: 0f, y:_mx, z: 0);
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime;
    }
    public void ResetX()
    {
        _mx = 0;
    }
}
