using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 1;

    public GameObject _app;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void play()
    {
        if (debugOut == 1) Debug.Log("[Play Button/play]: Play Button Clicked!");
    }
}
