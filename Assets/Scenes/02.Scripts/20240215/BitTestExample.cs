using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BitTestExample : MonoBehaviour
{
   
    void Start()
    {
        int number0 = 0;
        int number1 = Int32.MaxValue;
        int number2 = Int32.MinValue;
        Debug.Log(Convert.ToString(number0, 2).PadLeft(32, '0'));
        Debug.Log(Convert.ToString(number1, 2).PadLeft(32, '0'));
        Debug.Log(Convert.ToString(number2, 2).PadLeft(32, '0'));

        //��Ʈ ���� ����
        // ��Ʈ �����ڴ� : ���� ���� ��Ʈ(bit)�� �����Ѵ�.
       /* bool result1= true; // false ���������� 8��Ʈ ��� (true, false 2����)
        bool result2 = true; // false ���������� 8��Ʈ ��� (true, false 2����)
        bool result3 = false; // false ���������� 8��Ʈ ��� (true, false 2����)
        bool result4= false; // false ���������� 8��Ʈ ��� (true, false 2����)
        bool result5 = true; // false ���������� 8��Ʈ ��� (true, false 2����)*/

       // short a = 1;// 8��Ʈ�� ��� _____1101
                    // ___
    }               //������ �޸� �뷮 ����ȭ�� �ϴ� �渶���ڵ�

    //����ϱ� ��ư� ��ٷ����� �˾� �� �ʿ�� �ִ�.

    
    void Update()
    {
        
    }
}
