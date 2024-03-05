using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public enum GunType
{
    Rifle, //따발총
    Sniper,// 저격총
    Pistol, //권총
}
public class Gun : MonoBehaviour
{
    public GunType Gtype;

    public int Damage = 10; //공격력

    public float FireCooltime = 0.2f; //발사쿨타임
    //-총알 개수
    public int BulletRemainCount;
    public int BulletMaxCount = 30;

    public float ReloadTime = 1.5f;

    //대표이미지
    public Sprite ProfileImage;
   
    
   
    void Start()
    {
        BulletRemainCount = BulletMaxCount;
    }

 
  
  
}
