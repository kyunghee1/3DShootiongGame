using System;
using UnityEngine;

public enum DeleteType
{
    Destory,
    Inactive,
}
public class DestroyTime : MonoBehaviour
{
    public DeleteType DeleteType; //파괴까지의 시간

    public float DeleteTime = 1.5f; // 경과 시간을 측정하기 위한 타이머
    private float _timer = 0;

    public void Init()
    {
        _timer = 0f;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        Init();
    }


    private void Update()
    {

        _timer += Time.deltaTime;
        if (_timer >= DeleteTime)
        {
            if (DeleteType == DeleteType.Destory)
            {
                Destroy(this.gameObject);
            }

            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
