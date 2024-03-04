using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFactory : MonoBehaviour
{
    public static BloodFactory Instance { get; private set; }

    [Header("�� ȿ�� ������")]
    public GameObject BloodPrefab;

    private List<GameObject> _pool;
    public int PoolSize = 10;


    // Todo. ������Ʈ Ǯ�� �����غ�����.

    private void Awake()
    {
        Instance = this;

        _pool = new List<GameObject>();
        for (int i = 0; i < PoolSize; ++i)
        {
            GameObject bloodObject = Instantiate(BloodPrefab);
            _pool.Add(bloodObject);
            bloodObject.SetActive(false);
        }
    }

    public void Make(Vector3 position, Vector3 normal)
    {
        foreach (GameObject bloodObject in _pool)
        {
            if (bloodObject.activeInHierarchy == false)
            {
                bloodObject.GetComponent<DestroyTime>()?.Init();
                bloodObject.transform.position = position;
                bloodObject.transform.forward = normal;
                bloodObject.SetActive(true);
                break;
            }
        }
    }
}
