using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour, IHitable
{
    [Range(0, 100)]
    public int Health;
    public int MaxHealth = 100;

    public Slider HealthSliderUI;
    // Start is called before the first frame update


    private void ItemDrop()
    {
        
     
        
    }
    private void Die()
    {
        ItemObjectFactory.Instance.MakePercent(transform.position);
        Destroy(gameObject);
    }
    
    public void Hit(int damage)
    {
        Health -= damage;
       if(Health<0)
        {
            Die();
           Destroy(gameObject);
        }
    }

   
    
    void Start()
    {
        Init();
    }
    public void Init()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthSliderUI.value = (float)Health / (float)MaxHealth; //0~1
    }
}
