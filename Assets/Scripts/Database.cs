using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 2;

    // data
    public List<string> eventLog = new List<string>();

    public int maxLines = 10;

    // Start is called before the first frame update
    void Start()
    {
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
        eventLog.Add("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
