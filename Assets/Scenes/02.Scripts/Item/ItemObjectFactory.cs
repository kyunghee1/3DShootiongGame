using System.Collections.Generic;
using UnityEngine;




//������ ������ ���� : ������ ������Ʈ�� ������ å������.
//���丮 ����
//��ü ������ ���� Ŭ������ �̿��� ĸ��ȭ ó���Ͽ� ��� "����"�ϰ� �ϴ� ������ ����
//��ü ������ �ʿ��� ������ ���ø�ȭ �س��� �ܺξּ� ���� ����Ѵ�.
//����:
//1.������ ó�� ������ �и��Ͽ� ���յ��� ���� �� �ִ�.
//2.Ȯ�� �� ���������� ���ϴ�.
//3. ��ü ���� �� �������� �� ���� �����ϵ��� ������ �� �� �ִ�.
//����:
//1. ��������� ���� �� �����ϴ�
//2. �׷��� �����ؾ� �Ѵ�.
//3. ������ ����.
public class ItemObjectFactory : MonoBehaviour
{

    public static ItemObjectFactory Instance { get; private set; }
  
   
    private int PoolCount = 10; //Ǯ ũ��
    public List<ItemObject> _ItemPool = new List<ItemObject>();
    public List<GameObject> ItemPrefabs;

    private void Awake()
    {
        Instance = this;
        _ItemPool = new List<ItemObject>();
        for (int i = 0; i < PoolCount; i++) //10��
        {
            foreach (GameObject prefab in ItemPrefabs) //3��
            {
                GameObject item = Instantiate(prefab);
                item.transform.SetParent(this.transform);

                _ItemPool.Add(gameObject.GetComponent<ItemObject>());

                item.SetActive(false);
            }
        }

    }
    private void Start()
    {
        
    }
    private ItemObject Get(ItemType itemType) //â�� ������
    {
        foreach(ItemObject itemObject in _ItemPool) //â�� ������
        {
            if(itemObject.gameObject.activeSelf == false && itemObject.ItemType == itemType)
            {
                return itemObject;
            }
        }
        return null;
    }
   
    public void MakePercent(Vector3 position)
    {
        int percentage = UnityEngine.Random.Range(0, 100);
        if (percentage <= 20) // 20%
        {
            Make(ItemType.Health, position);
        }
        else if (percentage <= 40)
        {
            Make(ItemType.Stamina, position);
        }
        else if (percentage <= 50)
        {
            Make(ItemType.Bullet, position);
        }
    }
    public void Make(ItemType itemType, Vector3 position)
    {
       ItemObject itemObject = Get(itemType);
        if (itemObject != null)
        {
            itemObject.transform.position = position;
            ///itemObject.Init();
            itemObject.gameObject.SetActive(true);
        }
     }
}
     



        
        
      
    

