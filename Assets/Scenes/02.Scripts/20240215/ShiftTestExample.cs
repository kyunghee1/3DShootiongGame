using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTestExample : MonoBehaviour
{
  
    void Start()
    {
        //시프트 연산자: 데이터의 갑싱 한 비트씩 왼쪽/오른쪽으로 이동하는 연산
        //장점: 연산 속도가 빠르다.
        //단점: 머리로 계산이 잘 안된다.

        int number = 3;         //...0000 0000 0000 0011
                                // 0000 0011
        Debug.Log(number);  // 0000 0011 :3
        //<<: 왼쪽 시프트 연산자: 비트를 주어진 수만큼 왼쪽으로 이동
        Debug.Log(number << 1); // 0000 0110 : 6

        //<<: 오른쪽 시프트 연산자: 비트를 주어진 수만큼 오른쪽으로 이동
        Debug.Log(number >> 1); // 0000 0001:1
    }

  
    void Update()
    {
        
    }
}
