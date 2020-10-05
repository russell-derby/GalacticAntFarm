using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnClick()
    {
        if (debugOut == 1) Debug.Log("[Option Button/OnClick]: Option Button Clicked!");
    }
}
