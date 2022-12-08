using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{   
    public KeyCode takeButton;
    public OnSelect item;
    public ItemInHands handsItem;

    private int id1;
    private int id2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SeeObject();
    }

    private void SeeObject()
    {
        if (Input.GetKeyDown(takeButton))
        {
            Ray R = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height /2));
            RaycastHit hit;
            if (Physics.Raycast(R, out hit, 2f))
            {
                if (hit.collider.GetComponent<OnSelect>()) 
                {
                    id1 = item.GetComponent<OnSelect>().ItemId;
                    id2 = handsItem.GetComponent<ItemInHands>().ItemHandId;
                    if(id1 == id2)
                    {
                        handsItem.GetComponent<ItemInHands>().Visible = true;
                    }
                    Destroy(hit.collider.GetComponent<OnSelect>().gameObject);
                }
            }
        }
    }
}