using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInHands : MonoBehaviour
{
    public string ItemName;
    public bool Visible = false;
    public int ItemHandId;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Visible == true)
        {
            gameObject.SetActive(true);
        }
    }
}
