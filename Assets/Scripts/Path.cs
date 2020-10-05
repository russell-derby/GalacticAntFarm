using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 0;

    public string id;
    public Transform s1, s2;
    public List<Transform> ships;


    // Start is called before the first frame update
    void Start()
    {
        if (debugOut >= 1) Debug.Log("[Path/Start]: Path Created between Plnt " + s1.gameObject.GetComponent<Star>().starName + " and " + s2.gameObject.GetComponent<Star>().starName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int addShip(Transform ship)
    {
        if (!ships.Contains(ship))
        {
            ships.Add(ship);
            return 1;
        }
        else { return -1; }
    }

    public int removeShip(Transform ship)
    {
        if (ships.Contains(ship))
        {
            ships.Remove(ship);
            return 1;
        }
        else { return -1; }
    }
}
