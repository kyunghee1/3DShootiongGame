using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // ��ǥ: ī�޶� ���� �ð����� �����ϰ� ���� �ʹ�.
    // �ʿ� �Ӽ�:
    // - ����ŷ �ð�
    public float ShakingDuration = 0.2f;
    // - ����ŷ ���� �ð�
    private float _shakingTimer = 0f;
    // - ����ŷ �Ŀ�
    public float ShakingPower = 0.025f;
    // - ����ŷ ���̳�?
    private bool _isShaking = false;

    public void Shake()
    {
        _shakingTimer = 0f;
        _isShaking = true;
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }
        if (!_isShaking)
        {
            return;
        }

        // ���� ����:
        // 1. �ð��� �帥��.
        _shakingTimer += Time.deltaTime;

        // 2. �����Ѱ� ����.
        transform.position = Vector3.zero + Random.insideUnitSphere * ShakingPower;

        // 3. ���� �ð��� ������ �ʱ�ȭ
        if (_shakingTimer >= ShakingDuration)
        {
            _isShaking = false;
            transform.position = Vector3.zero;
        }
    }
}

