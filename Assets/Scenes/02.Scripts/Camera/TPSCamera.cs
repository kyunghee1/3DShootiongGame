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
    public Transform Target;
    public Vector3 Offset = new Vector3(0, 3f, -3f);

    private void CalculateCameraAngles()
    {
        Vector3 targetToCameraVector = transform.position - Target.position;

        // ���� ȸ�� ���� x�� ����մϴ�.
        float x = Mathf.Atan2(targetToCameraVector.x, targetToCameraVector.z) * Mathf.Rad2Deg;

        // ���� ȸ�� ���� y�� ����մϴ�.
        // Ÿ�����κ��� ī�޶������ ���͸� ����鿡 ������ ��, �̿� ī�޶� ���� ���� ������ ������ ����մϴ�.
        float y = Mathf.Asin(targetToCameraVector.y / targetToCameraVector.magnitude) * Mathf.Rad2Deg;
    }

    public float a;
    private void LateUpdate()
    {
        transform.position = Target.position + Offset;
        transform.LookAt(Target);

        Vector2 xy = CameraManager.Instance.XY;
        transform.RotateAround(Target.position, Vector3.up, xy.x);
        transform.RotateAround(Target.position, transform.right, -xy.y);


        transform.position = Target.position - transform.forward * Offset.magnitude + Vector3.up * (Offset.y - a);
    }
}