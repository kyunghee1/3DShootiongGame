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

        //비트 단위 연산
        // 비트 연산자는 : 변수 내의 비트(bit)를 조작한다.
       /* bool result1= true; // false 내부적으로 8비트 사용 (true, false 2가지)
        bool result2 = true; // false 내부적으로 8비트 사용 (true, false 2가지)
        bool result3 = false; // false 내부적으로 8비트 사용 (true, false 2가지)
        bool result4= false; // false 내부적으로 8비트 사용 (true, false 2가지)
        bool result5 = true; // false 내부적으로 8비트 사용 (true, false 2가지)*/

       // short a = 1;// 8비트를 사용 _____1101
                    // ___
    }               //극한의 메모리 용량 최적화를 하는 흑마법코드

    //사용하기 어렵고 까다롭지만 알아 둘 필요는 있다.

    
    void Update()
    {
        
    }
}
