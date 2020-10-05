using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 0;

    //List<Body> planets;
    List<GameObject> lifeforms;


    // Start is called before the first frame update
    void Start()
    {
        if (debugOut >= 1) Debug.Log("[Body/Start]: New Body!");
    }

    // Update is called once per frame
    //void Update() {  }
}
