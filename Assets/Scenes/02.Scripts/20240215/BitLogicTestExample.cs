using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitLogicTestExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //��Ʈ � ������: ���� ���� ��Ʈ(bit)�� �����ϴ� ������
        int a = 10; // 0000 1010
        int b = 6; // 0000 0110 =6

        // 0000 1010 = 10
        // 0000 0110 = 6
        //����(&): �� ��Ʈ�� ��� 1�϶��� 1
        // 0000 0010 =2
        Debug.Log(a & b); // 2

        // 0000 1010 = 10
        // 0000 0110 =6
        //����(|): �� ��Ʈ�� �ϳ��� 1 �̾ 1
        // 0000 1112 = 14
        Debug.Log(a |b);

        // 0000 1010 = 10
        // 0000 0110 =6
        //��Ÿ�� ����(^): �� ��Ʈ�� �޶�� 1
        // 0000 1100 =12
        Debug.Log(a ^b);

        // 0000 1010 = 10

        //������(~): �ݴ��
        // 1111 0101 = -11
        Debug.Log(~a);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
