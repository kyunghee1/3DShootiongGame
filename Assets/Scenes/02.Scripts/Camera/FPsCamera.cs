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
    public float RotationSpeed = 200f; // �ʴ� 200������ ȸ�� ������ �ӵ�
    // ������ x������ y����
    private float _mx = 0;
    private float _my = 0;



    /** ī�޶� �̵� **/
    // ��ǥ: ī�޶� ĳ������ ������ �̵���Ű�� �ʹ�.
    // �ʿ� �Ӽ�:
    // - ĳ������ �� ��ġ
    public Transform Target;
    // ���� ����:
    // 1. ĳ������ �� ��ġ�� ī�޶� �̵���Ų��.






    private void Start()
    {
        // ���콺 Ŀ�� ���ְ�, ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // ����:
    // 1. ���콺 �Է�(drag) �޴´�.
    // 2. ���콺 �Է� ���� �̿��� ȸ�� ������ ���Ѵ�.
    // 3. ȸ�� ���� ȸ���Ѵ�.

    private void LateUpdate()
    {
        if (CameraManager.Instance.Mode == CameraMode.FPS)
        {
            // 1. ĳ������ �� ��ġ�� ī�޶� �̵���Ų��.
            transform.position = Target.position;
        }



        // 1. ���콺 �Է�(drag) �޴´�.
        float mouseX = Input.GetAxis("Mouse X"); // ���⿡ ���� -1 ~ 1 ������ �� ��ȯ 
        float mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log($"GetAxis: {mouseX},{mouseY}");
        //Vector2 mousePosition = Input.mousePosition; // ��¥ ���콺 ��ǥ��
        //Debug.Log($"mousePosition: {mousePosition.x}, {mousePosition.y}");

        // 2. ���콺 �Է� ���� �̿��� ȸ�� ������ ���Ѵ�.
        Vector3 rotationDir = new Vector3(mouseX, mouseY, 0);
        // rotationDir.Normalize(); // ����ȭ

        // 3. ȸ�� ���� ȸ���Ѵ�.
        // ���ο� ��ġ = ���� ��ġ + ���� * �ӵ� * �ð�
        // ���ο� ȸ�� = ���� ȸ�� + ���� * �ӵ� * �ð�
        // 3-1 ȸ�� ���⿡ ���� ���콺 �Է� �� ��ŭ �̸� ������Ų��.
        _mx += rotationDir.x * RotationSpeed * Time.deltaTime;
        _my += rotationDir.y * RotationSpeed * Time.deltaTime;

        // 4. �ü��� ���� ������ -90 ~ 90�� ���̷� �����ϰ� �ʹ�.
        _my = Mathf.Clamp(_my, -90f, 90f);
        //_mx = Mathf.Clamp(_mx, -270f, 270f);

        if (CameraManager.Instance.Mode == CameraMode.FPS)
        {
            transform.eulerAngles = new Vector3(-_my, _mx, 0);
        }

        // ���Ϸ� ������ ����
        // 1. ������ ����
        // 2. 0���� �۾����� -1�� �ƴ� 359(360-1)�� �ȴ�. (����Ƽ ���ο��� �̷��� �ڵ� ����)
        // �� 2�� ���� �ذ��� ���ؼ� �츮�� �̸� ������ ����� �Ѵ�.
    }
}