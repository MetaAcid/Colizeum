using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelect : MonoBehaviour
{
    public string Name;
    public bool Visible = true;
    public int ItemId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Visible == false)
        {
            Destroy(this.gameObject);
        }
    }
}
