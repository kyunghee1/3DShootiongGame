using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTestExample : MonoBehaviour
{
  
    void Start()
    {
        //����Ʈ ������: �������� ���� �� ��Ʈ�� ����/���������� �̵��ϴ� ����
        //����: ���� �ӵ��� ������.
        //����: �Ӹ��� ����� �� �ȵȴ�.

        int number = 3;         //...0000 0000 0000 0011
                                // 0000 0011
        Debug.Log(number);  // 0000 0011 :3
        //<<: ���� ����Ʈ ������: ��Ʈ�� �־��� ����ŭ �������� �̵�
        Debug.Log(number << 1); // 0000 0110 : 6

        //<<: ������ ����Ʈ ������: ��Ʈ�� �־��� ����ŭ ���������� �̵�
        Debug.Log(number >> 1); // 0000 0001:1
    }

  
    void Update()
    {
        
    }
}
