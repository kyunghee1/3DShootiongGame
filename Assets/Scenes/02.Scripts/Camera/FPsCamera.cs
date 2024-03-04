using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

// 1��Ī ���� (First Person Shooter)
// ���ӻ��� ĳ������ ������ ���� ī�޶�
public class FPSCamera : MonoBehaviour
{
    // ** ī�޶� ȸ�� **
    // ��ǥ: ���콺�� �����ϸ� ī�޶� �� �������� ȸ����Ű�� �ʹ�.
    // �ʿ� �Ӽ�:
    // - ȸ�� �ӵ�
    //  public float RotationSpeed = 200f; // �ʴ� 200������ ȸ�� ������ �ӵ�
    // ������ x������ y����
    //   private float _mx = 0;
    //  private float _my = 0;



    /** ī�޶� �̵� **/
    // ��ǥ: ī�޶� ĳ������ ������ �̵���Ű�� �ʹ�.
    // �ʿ� �Ӽ�:
    // - ĳ������ �� ��ġ
    public Transform Target;

    
    // ���� ����:
    // 1. ĳ������ �� ��ġ�� ī�޶� �̵���Ų��.






  
    
      
    private void LateUpdate()
        {
        if(GameManager.Instance.State != GameState.Go)
        {
            return;
        }
            transform.localPosition = Target.position;

            Vector2 xy = CameraManager.Instance.XY;
            transform.eulerAngles = new Vector3(-xy.y, xy.x, 0);


            // ���Ϸ� ������ ����
            // 1. ������ ����
            // 2. 0���� �۾����� -1�� �ƴ� 359(360-1)�� �ȴ�. (����Ƽ ���ο��� �̷��� �ڵ� ����)
            // �� 2�� ���� �ذ��� ���ؼ� �츮�� �̸� ������ ����� �Ѵ�.
        }
    }