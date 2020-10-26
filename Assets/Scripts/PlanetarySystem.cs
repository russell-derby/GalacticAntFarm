using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetarySystem : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 1;

    System.Random random;
    public GameObject bodyPrefab;
    //public GameObject game;

    public List<Transform> bodies;
    public List<GameObject> lifeForms;
    public GameObject star;
    public string sysName = "";
    public int sysSize;
    public int numPlanets;

    GameObject b1;

    float baseX;
    float baseY;
    float baseZ;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random(GetInstanceID());

        sysSize = 26;
        numPlanets = random.Next(9)+2;
        baseX = 0F;
        baseY = 0F;
        baseZ = 0F;


        if (debugOut >= 1) Debug.Log("[PlanetarySystem" + sysName + "/Start]: New System: " + sysName);

        transform.position += new Vector3(1000F,0F,0F);
        generateSystem();
    }

    // System Functions 

    public void enableSystem()
    {
        foreach (Transform b1 in bodies)
        {
            b1.gameObject.SetActive(true);
        }

        Debug.Log("[PlanetarySystem" + sysName + "]: System Enabled");
    }

    public void disableSystem()
    {
        foreach (Transform b1 in bodies)
        {
            b1.gameObject.SetActive(false);
        }

        Debug.Log("[PlanetarySystem" + sysName + "]: System Disabled");
    }

    void generateSystem()
    {
        /*
        //create sun
        b1 = Instantiate(bodyPrefab);
        b1.GetComponent<Body>().transform.SetParent(this.gameObject.transform);
        b1.transform.localPosition = new Vector2(baseX, baseY);
        b1.transform.localScale = new Vector2(3F, 3F);
        //b1.SetActive(false);
        bodies.Add(b1.transform);
        */

        //create planets
        for (int i = 0; i < numPlanets; i++) 
        {
            b1 = Instantiate(bodyPrefab);
            b1.transform.SetParent(transform);

            float r = 5+(((i+1)*(sysSize/numPlanets))*(float)((random.NextDouble()*0.04)+1));
            float phi = (float)(((i+1)*(4*Math.PI/numPlanets)) * ((random.NextDouble()*0.3)+0.85));
            if (debugOut == 1) Debug.Log("[PlanetarySystem" + sysName + "/generateSystem]: Body at r: " + r + " phi: " + phi);

            float x = (float)(r * Math.Cos(phi));
            float y = (float)(r * Math.Sin(phi));
            if (debugOut == 1) Debug.Log("[PlanetarySystem" + sysName + "/generateSystem]: Body at x: " + x + " y: " + y);
            b1.transform.localPosition = new Vector3(baseX + x, baseY + y, 0f);
            //b1.SetActive(false);
            bodies.Add(b1.transform);
        }

        Debug.Log("[PlanetarySystem" + sysName + "]: System Generated");
    }

    void checkNearby()
    {
        if(debugOut == 1) Debug.Log("[PlanetarySystem" + sysName + "/checkNearby]: Spreading life on " + sysName + "!");
    }

    // Life Functions
    public int createLifeOnPlanet()
    {
        int wasLifeCreated = 0;
        foreach (Transform p1 in bodies)
        {
            if (b1.GetComponent<Body>().isHabitable)
            {
                wasLifeCreated = 1;
            }
        }
        return (int)wasLifeCreated;
    }
}
