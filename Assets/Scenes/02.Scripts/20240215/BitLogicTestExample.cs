using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitLogicTestExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //비트 놀리 연산자: 변수 내의 비트(bit)를 조작하는 연산자
        int a = 10; // 0000 1010
        int b = 6; // 0000 0110 =6

        // 0000 1010 = 10
        // 0000 0110 = 6
        //논리곱(&): 두 비트가 모두 1일때만 1
        // 0000 0010 =2
        Debug.Log(a & b); // 2

        // 0000 1010 = 10
        // 0000 0110 =6
        //논리합(|): 두 비트중 하나만 1 이어도 1
        // 0000 1112 = 14
        Debug.Log(a |b);

        // 0000 1010 = 10
        // 0000 0110 =6
        //배타적 논리합(^): 두 비트가 달라야 1
        // 0000 1100 =12
        Debug.Log(a ^b);

        // 0000 1010 = 10

        //보수합(~): 반대로
        // 1111 0101 = -11
        Debug.Log(~a);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
