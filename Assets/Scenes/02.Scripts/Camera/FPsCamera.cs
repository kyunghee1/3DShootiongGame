using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

// 1인칭 슈팅 (First Person Shooter)
// 게임상의 캐릭터의 시점을 보는 카메라
public class FPSCamera : MonoBehaviour
{
    // ** 카메라 회전 **
    // 목표: 마우스를 조작하면 카메라를 그 방향으로 회전시키고 싶다.
    // 필요 속성:
    // - 회전 속도
    //  public float RotationSpeed = 200f; // 초당 200도까지 회전 가능한 속도
    // 누적할 x각도와 y각도
    //   private float _mx = 0;
    //  private float _my = 0;



    /** 카메라 이동 **/
    // 목표: 카메라를 캐릭터의 눈으로 이동시키고 싶다.
    // 필요 속성:
    // - 캐릭터의 눈 위치
    public Transform Target;

    
    // 구현 순서:
    // 1. 캐릭터의 눈 위치로 카메라를 이동시킨다.






  
    
      
    private void LateUpdate()
        {
        if(GameManager.Instance.State != GameState.Go)
        {
            return;
        }
            transform.localPosition = Target.position;

            Vector2 xy = CameraManager.Instance.XY;
            transform.eulerAngles = new Vector3(-xy.y, xy.x, 0);


            // 오일러 각도의 단점
            // 1. 짐벌락 현상
            // 2. 0보다 작아지면 -1이 아닌 359(360-1)가 된다. (유니티 내부에서 이렇게 자동 연산)
            // 위 2번 문제 해결을 위해서 우리가 미리 연산을 해줘야 한다.
        }
    }