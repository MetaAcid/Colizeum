using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlItemInInventory : MonoBehaviour
{
    public OnSelect item;
    public int id;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         GetItem();
    }

    private void GetItem()
    {
        if (item.ItemId == id)
        {
            isActive = true;
            //this.SetActive(true);
        } 
        else
        {
            isActive = false;
        }

        if(isActive == false)
        {
            //this.SetActive(false);
        }
    }
}
