using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerr : MonoBehaviour
{

    public static ItemManagerr _instance = null;
    public List<Item> inventory = new List<Item>();
 
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;

        inventory.Add(new Item(ItemType.Health,3));
        inventory.Add(new Item(ItemType.Stamina,10));
        inventory.Add(new Item(ItemType.Bullet,7));
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
