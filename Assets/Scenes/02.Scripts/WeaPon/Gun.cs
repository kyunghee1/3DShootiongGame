using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public enum GunType
{
    Rifle, //������
    Sniper,// ������
    Pistol, //����
}
public class Gun : MonoBehaviour
{
    public GunType Gtype;

    public int Damage = 10; //���ݷ�

    public float FireCooltime = 0.2f; //�߻���Ÿ��
    //-�Ѿ� ����
    public int BulletRemainCount;
    public int BulletMaxCount = 30;

    public float ReloadTime = 1.5f;

    //��ǥ�̹���
    public Sprite ProfileImage;
   
    
   
    void Start()
    {
        BulletRemainCount = BulletMaxCount;
    }

 
  
  
}
