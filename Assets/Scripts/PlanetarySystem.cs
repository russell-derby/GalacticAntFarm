using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetarySystem : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 0;

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


        if (debugOut >= 1) Debug.Log("[PlanetarySystem/Start]: New System: " + sysName);

        generateSystem();
    }

    public void enableSystem()
    {
        foreach (Transform b1 in bodies)
        {
            b1.gameObject.SetActive(true);
        }
    }

    public void disableSystem()
    {
        foreach (Transform b1 in bodies)
        {
            b1.gameObject.SetActive(false);
        }
    }

    void generateSystem()
    {
        //create sun
        b1 = Instantiate(bodyPrefab);
        b1.GetComponent<Body>().transform.SetParent(this.gameObject.transform);
        b1.transform.localPosition = new Vector2(baseX, baseY);
        b1.transform.localScale = new Vector2(3F, 3F);
        //b1.SetActive(false);
        bodies.Add(b1.transform);

        //create planets
        for (int i = 0; i < numPlanets; i++) 
        {
            b1 = Instantiate(bodyPrefab);
            b1.GetComponent<Body>().transform.SetParent(this.gameObject.transform);

            float r = 5+(((i+1)*(sysSize/numPlanets))*(float)((random.NextDouble()*0.04)+1));
            float phi = (float)(((i+1)*(4*Math.PI/numPlanets)) * ((random.NextDouble()*0.3)+0.85));
            if (debugOut == 1) Debug.Log("[PlanetarySystem/generateSystem]: Body at r: " + r + " phi: " + phi);

            float x = (float)(r * Math.Cos(phi));
            float y = (float)(r * Math.Sin(phi));
            if (debugOut == 1) Debug.Log("[PlanetarySystem/generateSystem]: Body at x: " + x + " y: " + y);
            b1.transform.localPosition = new Vector2(baseX + x, baseY + y);
            //b1.SetActive(false);
            bodies.Add(b1.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkNearby()
    {
        if(debugOut == 1) Debug.Log("[PlanetarySystem/checkNearby]: Spreading life on " + sysName + "!");
    }
}
