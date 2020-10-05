using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeForm : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 2;
    
    System.Random random;

    /******** Species Traits ********/
    public string sName;            //species' name
    public Color color;             //species' color
    public string bodyType;         //species' body type
    public string socStruct;        //species' social structure

    public int d_firstEvolved;      //date first evolved
    public int d_firstFlight;       //date of first space flight
    public int d_firstStarFlight;   //date of first interstellar flight

    int numTraits;
    public double d_intel;          //intelligence mod
    public double d_relig;          //religiousness mod
    public double d_adven;          //adventurousness mod
    public double d_empath;         //empathy mod
    public double d_commerc;        //commercialness mod
    public double d_imper;          //imperialism mod
    public double d_aggro;          //aggressiveness mod

    public int[] uniTraits;         //id's of unique traits

    public GameObject ship;

    public List<Transform> occSystems;

    public List<Transform> fleet;
    public List<GameObject> nearbySpecies;
    public List<Transform> nearbySys;
    public List<Transform> intPaths;
    public List<Transform> extPaths;
    public float spreadChance;

    GameObject s1;

    int ranInt;
    double ranDub;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random(GetInstanceID());

        color = new Color(0F, 0F, 0F, 1F);

        spreadChance = 50F;

        numTraits = 7;

        genName();
        rollStats();
        setColor();
        //explore();

        if (debugOut >= 1) Debug.Log("[LifeForm/Start]: Lifeform Started");
    }

    // Update is called once per frame
    void Update()
    {
        //random tick
        if (Random.Range(0.0F, 1.0F) < (1F / spreadChance))
        {
                if (debugOut == 1) Debug.Log("[LifeForm/Update]: Calling Spread");
                spread();
        }     

        //random action:
            //evolve();
            //explore();
            //colonize();
            //migrate();
            //terraform();
            //research();
            //war();
            //ally();
            //annex();
            //trade();
            //blockade();
            //sanctify();
            //crusade();
            //mission();
            //revolution();
            //organize();
    }

    void genName()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string n1 = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        sName = n1;
    }

    void rollStats()
    {
        d_intel = random.NextDouble();
        d_relig = random.NextDouble();
        d_adven = random.NextDouble();
        d_empath = random.NextDouble();
        d_commerc = random.NextDouble();
        d_imper = random.NextDouble();
        d_aggro = random.NextDouble();
        return;
    }

    void setColor()
    {
        if (color == Color.black) {
            color = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1F);
        }


        foreach (Transform g in occSystems)
        {
            g.gameObject.GetComponent<Star>().setColor(color);
            //g.GetComponent<Star>().halo.color = color;
            //g.GetComponent<Star>().halo.FindProperty("m_Color").colorValue = color;
        }
    }

    public void addStar(Transform p1)
    {
        occSystems.Add(p1);
        foreach (Transform path in p1.gameObject.GetComponent<Star>().paths)
        {
            Path pathScript = path.gameObject.GetComponent<Path>();
            if (pathScript.s1 == p1)
            {
                if (pathScript.s2.gameObject.GetComponent<Star>().lifeForms.Contains(transform))
                {
                    intPaths.Add(path);
                    if (extPaths.Contains(path))
                    {
                        extPaths.Remove(path);
                    }
                }
                else 
                {
                    extPaths.Add(path);
                }
            }
            else
            {
                if (pathScript.s1.gameObject.GetComponent<Star>().lifeForms.Contains(transform))
                {
                    intPaths.Add(path);
                    if (extPaths.Contains(path))
                    {
                        extPaths.Remove(path);
                    }
                }
                else
                {
                    extPaths.Add(path);
                }
            }
        }
        //nearbySys.Remove(p1);
        return;
    }

    void checkNearbyAll()
    {
        // for each occupied system
        foreach (Transform occSys in occSystems)
        {
            // go through the systems near it
            Star sysScript = occSys.gameObject.GetComponent<Star>();
            foreach (Transform nearSys in sysScript.checkNearby())
            {
                // and if that system is unoccuppied and not already listed
                if (!occSystems.Contains(nearSys) && !nearbySys.Contains(nearSys))
                {
                    // add to nearby
                    nearbySys.Add(nearSys);
                }
            }

        }
    }


    /****************** Actions ******************/

    /***** Action Handlers *****/
    //Advance the species to the next evolution stage
    int evolve()
    {
        return 0;
    }

    int spread() 
    {
        // Pick a random exterior path
        int ind1 = random.Next(extPaths.Count);
        if (ind1 > 0)
        {
            Transform spreadPath = extPaths[ind1];
            Path pathScript = spreadPath.gameObject.GetComponent<Path>(); 
            if (debugOut == 1) Debug.Log("[Lifeform/spread]: Spreading along extPaths[" + ind1 + "]: " + pathScript.id);

            if (occSystems.Contains(pathScript.s1))
            {
                // send ship from s1
                if (debugOut == 1) Debug.Log("[Lifeform/spread]: Sending from " + pathScript.s1.gameObject.GetComponent<Star>().starName);

                // sends ship if planet is not already occuped ** TEMPORARY **
                if (!pathScript.s2.gameObject.GetComponent<Star>().lifeForms.Any())
                {
                    // returns 1 if successful
                    return createShip(pathScript.s1, spreadPath);
                }
                else 
                {
                    // returns 0 if planet is already occupied
                    return 0;
                }
            }
            else
            {
                // send ship from s2
                if (debugOut == 1) Debug.Log("[Lifeform/spread]: Sending from " + pathScript.s2.gameObject.GetComponent<Star>().starName);

                // sends ship if planet is not already occuped ** TEMPORARY **
                if (!pathScript.s1.gameObject.GetComponent<Star>().lifeForms.Any())
                {
                    // returns 1 if succesful
                    return createShip(pathScript.s2, spreadPath);
                }
                else
                { 
                    // returns 0 if planet is already occupied
                    return 0;
                }
            }
        }
        else 
        {
            // returns 0 if there are no exterior planets
            return 0;
        }
    }

    //Collect information on an external planet or system.
    int explore() { return 0; }

    //Send a colony ship to an unsettled/semi-settled external planet
    int colonize() { return 0; }

    //Send a migratory ship to a settled external planet
    int migrate() { return 0; }

    //Initiate a terraforming mission to a connected external planet
    int terraform() { return 0; }

    //Research science?
    int research() { return 0; }

    //Declare war on a known species
    int war() { return 0; }

    //Attempt to ally with a known species
    int ally() { return 0; }

    //Take over an external planet that's controlled by another species
    int annex() { return 0; }

    //Set up a trade agreement with a nearby species
    int trade() { return 0; }

    //Prevent two species/planets/systems from exchanging resources
    int blockade() { return 0; } 

    // Species vs Populations - Dig in!

    //sanctify();
    //crusade();
    //mission();
    //revolution();
    //organize();

    /***** Action Functions *****/
    GameObject createShip(Transform spawnStar)
    {
        // create a ship at spawnStar with no destination
        s1 = Instantiate(ship);
        s1.GetComponent<Ship>().transform.SetParent(this.gameObject.transform);
        s1.GetComponent<Ship>().lifeForm = transform;
        s1.GetComponent<Ship>().transform.position = spawnStar.gameObject.transform.position;
        fleet.Add(s1.transform);

        return s1;
    }

    int createShip(Transform spawnStar, Transform path)
    {
        // create a ship at spawnStar headed along path
        if (!path.gameObject.GetComponent<Path>().ships.Any())
        {
            if (debugOut == 1) Debug.Log("[Lifeform/createShip]: Ship Created on Planet " + spawnStar.gameObject.GetComponent<Star>().name);
            s1 = Instantiate(ship);
            s1.GetComponent<Ship>().transform.SetParent(transform);
            s1.GetComponent<Ship>().transform.position = spawnStar.position;
            fleet.Add(s1.transform);

            s1.GetComponent<Ship>().setPath(spawnStar, path);

            return 1;
        }
        else {
            if (debugOut == 1) Debug.Log("[Lifeform/createShip]: Ship already exists.");
            return 0;
        }
    }

    public int destroyShip(Transform toKill)
    {
        if (debugOut == 1) Debug.Log("[Lifeform/destroyShip]: Destroying Ship.");
        // Remove ship from path
        toKill.gameObject.GetComponent<Ship>().path.gameObject.GetComponent<Path>().removeShip(toKill);
        // Remove ship from fleet
        fleet.Remove(toKill);
        // Destroy ship
        Destroy(toKill.gameObject);
        return 1;
    }
}
