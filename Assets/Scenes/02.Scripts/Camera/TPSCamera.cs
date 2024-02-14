using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3��Ī ���� (Third Person Shooter)
// ���ӻ��� ĳ���Ͱ� ���� ������ �ƴ�, ĳ���͸� ���� ���� ��, 3��Ī ������ ������ ī�޶�
public class TPSCamera : MonoBehaviour
{
    // ** ī�޶� ȸ�� **
    // ��ǥ: ���콺�� �����ϸ� ī�޶� ĳ���� �߽ɿ� ���� �� �������� ȸ����Ű�� �ʹ�.

    // �ʿ� �Ӽ�:
    // - ȸ�� �ӵ�
    public float RotationSpeed = 200;
    // - Ÿ��(ĳ����)
    public Transform Target;
    public Vector3 Offset = new Vector3(0, 3f, -3f);
    // - ������ x������ y����
    private float _mx = 0;
    private float _my = 0;

    private void LateUpdate()
    {
        if (CameraManager.Instance.Mode == CameraMode.TPS)
        {
            // ���� ����:
            // 1. ī�޶� Ÿ��(�÷��̾�� ���� ������ �Ÿ�)���� �̵���Ų��.(����ٴϰ� �Ѵ�.)
            transform.position = Target.position + Offset;

            // 2. �÷��̾ �Ĵٺ��� �Ѵ�.
            // LookAt: Rotates the transform so the forward vector points at target's current position
            transform.LookAt(Target);
        }

        // 3. ���콺 �Է��� �޴´�.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 4. ���콺 �Է¿� ���� ȸ�� ������ �����Ѵ�.
        _mx += mouseX * RotationSpeed * Time.deltaTime;
        _my += mouseY * RotationSpeed * Time.deltaTime;
        //_my = Mathf.Clamp(_my, -20f, 10f);

        if (CameraManager.Instance.Mode == CameraMode.TPS)
        {
            // 5. Ÿ�� �߽����� ȸ�� ���⿡ �°� ȸ���Ѵ�.
            transform.RotateAround(Target.position, Vector3.up, _mx);
            transform.RotateAround(Target.position, transform.right, -_my);
        }


        //transform.position = Target.position - transform.forward * Offset.magnitude + Vector3.up * Offset.y;
    }
}