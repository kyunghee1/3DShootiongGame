using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerBombFire : MonoBehaviour
{
    // ��ǥ: ���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ����ź�� ������ �ʹ�.
    // �ʿ� �Ӽ�:
    // - ����ź ������
    public GameObject BombPrefab;
    // - ����ź ������ ��ġ
    public Transform FirePosition;
    // - ����ź ������ �Ŀ�
    public float ThrowPower = 15f;

    // ��ź ���� 3���� ����
    public int BombRemainCount;
    public int BombMaxCount = 3;
    // UI ���� text�� ǥ���ϱ� (ex. 1/3)
    public Text BombTextUI;

    // �ǽ� ���� 10. ��ź�� ������Ʈ Ǯ��(â��) ����
    public List<GameObject> BombPool; // ��ź â��
    public int BombPoolSize = 5;

    private void Start()
    {
        // ��ź â�� ����
        BombPool = new List<GameObject>();
        for (int i = 0; i < BombPoolSize; i++) // ������ ��ź ���� ��ŭ �ݺ�
        {
            GameObject bombObject = Instantiate(BombPrefab); // 1. ����
            bombObject.SetActive(false);                     // 2. ��Ȱ��ȭ
            BombPool.Add(bombObject);                        // 3. â�� ���� �ִ´�.
        }

        BombRemainCount = BombMaxCount;

        RefreshUI();
    }

    private void RefreshUI()
    {
        BombTextUI.text = $"{BombRemainCount}/{BombMaxCount}";
    }

    private void Update()
    {
        /* ����ź ��ô */
        // 1. ���콺 ������ ��ư�� ������ �� && ����ź ������ 0���� ũ��
        if (Input.GetMouseButtonDown(1) && BombRemainCount > 0)
        {
            BombRemainCount--;

            RefreshUI();

            // 2. â���� ����ź�� ���� ���� ������ ��ġ�� ����
            GameObject bomb = null;
            for (int i = 0; i < BombPool.Count; ++i)        // 1. â�� ������.
            {
                if (BombPool[i].activeInHierarchy == false) // 2. ������ ��ź�� ã�´�.
                {
                    bomb = BombPool[i];
                    bomb.SetActive(true);                   // 3. ������.
                    break;
                }
            }

            bomb.transform.position = FirePosition.position;

            // 3. �ü��� �ٶ󺸴� ����(ī�޶� �ٶ󺸴� ���� = ī�޶��� ����) ���� ����ź ��ô
            Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Camera.main.transform.forward * ThrowPower, ForceMode.Impulse);


        }



          
        }


    }
  
        


