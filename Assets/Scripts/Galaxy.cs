using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Galaxy : MonoBehaviour
{
    // debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 2;

    // parent objects
    public GameObject _pathsParent;
    public GameObject _lifeformsParent;
    public GameObject _starsParent;

    System.Random random;
    public List<GameObject> stars;
    public GameObject star;
    public GameObject path;
    public List<GameObject> lifeForms;
    public int maxLife;
    public GameObject life;
    public GameObject mainCam;
    public GalaxyCam camScript;
    public int numStars;
    public float maxPath;
    public int numBarStars;
    public int lifeScale;
    public double baseLifeChance;

    GameObject s1;
    GameObject l1;
    GameObject p1;
    public List<Transform> paths;

    int tick;
    int ranInt;
    double ranDub;

    // Start is called before the first frame update
    void Awake()
    {
        // initiate galaxy vars
        numStars = 2000;
        numBarStars = 125;
        maxLife = 5;
        maxPath = 7f;
        //starPos = new List<Transform>();
        paths = new List<Transform>();

        // life vars
        lifeScale = 1;
        baseLifeChance = 250;

        // initialize rng
        random = new System.Random(GetInstanceID());

        // Connect Main Cam
        camScript = mainCam.GetComponent<GalaxyCam>();

        if (debugOut >= 1) Debug.Log("[Galaxy/Awake]: Galaxy Awoken");

        // create stars for galaxy
        //createStars();
        // create paths for galaxy
        //createPaths();
    }

    // Update is called once per frame
    void Update()
    {
        // random life tick
        tick = (int)(random.NextDouble() * baseLifeChance * lifeScale);
        
        // 1 / (baseLifeChance * lifeScale) of spawning life per tick
        if (tick == 1 && lifeForms.Count < maxLife)
        {
            // randomly chooses a planet to spawn life on
            ranInt = random.Next(numStars + numBarStars);
            // checks if life is on the planet, if not spawns life
            if (stars[ranInt].GetComponent<Star>().lifeForms.Count == 0)
            {
                s1 = stars[ranInt];
                if (debugOut == 1) Debug.Log("[Galaxy/Update]: New Life on Planet " + s1.GetComponent<Star>().name);

                l1 = Instantiate(life);
                l1.GetComponent<LifeForm>().addStar(s1.transform);
                s1.GetComponent<Star>().addLife(l1.transform);
                lifeForms.Add(l1);
            }
        }

    }

    // FixedUpdate is called once per frame after Update. Use for physics.
    // void FixedUpdate() { }

    // Called once per frame after FixedUpdate.
    //void LateUpdate() {  }

    void setLifeScale(int scale)
    {
        if (scale == 1) { lifeScale = 2; }
        else if (scale == 2) { lifeScale = 10; }
        else if (scale == 3) { lifeScale = 25; }
    }

    float checkDist(Transform s1, Transform s2)
    {
        // distance formula
        return((float)Math.Sqrt(Math.Pow(s2.position.x,s1.position.x) + Math.Pow(s2.position.y,s1.position.y)));
    }

    public void createStars() 
    {
        // operational vars
        double x1 = 0, y1 = 0, x2 = 0, y2 = 0;

        // galaxy formula vars
        double A = 70;
        double B = 0.63;
        double N = 3.5;

        // position vars
        double step = (2 * Math.PI) / numStars;
        double r;
        float x;
        float y;

        // noise vars
        double xNoiseMax = 10;
        double xNoiseMin = 1;
        double yNoiseMax = 10;
        double yNoiseMin = 1;
        float barNoiseX = 0.15F;
        float barNoiseY = 0.55F;
        float bulgeNoise = 3.5f;
        double extraNoise;
        double exNoiseScale = 2.5;
        int exNoiseFreq = 2; // lower number means more noise

        // Each Star
          // Add Position to List - X
        // Sort List by X pos

        // Create Arm Stars
        for (int i = 0; i < numStars; i++)
        {
            // creates a star
            s1 = Instantiate(star);
            if (debugOut == 1) Debug.Log("[Galaxy/createStars]: Instantiated s1: " + s1.GetComponent<Star>().starName);

            // set star ID #
            s1.GetComponent<Star>().id = i;

            // sets the star as a child of the starsParent
            s1.transform.parent = _starsParent.transform;

            // stores star it in the stars array
            stars.Add(s1);

            // calculates the polar position of the star based on galaxy formula
            // every other time, gets the negative position
            if (i % 2 == 0)
            {
                r = (A / Math.Log(B * Math.Tan((step * (i + 1)) / (2 * N))));
            }
            else
            {
                r = -(A / Math.Log(B * Math.Tan((step * (i + 1)) / (2 * N))));
            }
            
            // convert polar coordinates to cartesian coordinates
            x = (float) (r * Math.Cos(step * (i+1)));
            y = (float) (r * Math.Sin(step * (i + 1)));

            // store 1st and 2nd star positions
            if ( i == 0 ) { x1 = x; y1 = y; }
            if ( i == 1 ) { x2 = x; y2 = y; }


            // randomly decides to amplify noise
            ranInt = random.Next(exNoiseFreq + 1);
            if (ranInt == exNoiseFreq)
            {
                extraNoise = exNoiseScale;
            }
            else
            {
                extraNoise = 1;
            }

            // randomly introduces noise in +/- x and y
            ranInt = random.Next(2);
            if (ranInt == 1)
            {
                x += (float)((random.NextDouble() * (xNoiseMax - xNoiseMin) + xNoiseMin) * extraNoise);
            }
            else
            {
                x -= (float)((random.NextDouble() * (xNoiseMax - xNoiseMin) + xNoiseMin) * extraNoise);
            }
            ranInt = random.Next(2);
            if (ranInt == 1)
            {
                y += (float)((random.NextDouble() * (yNoiseMax - yNoiseMin) + yNoiseMin) * extraNoise);
            }
            else
            {
                y -= (float)((random.NextDouble() * (yNoiseMax - yNoiseMin) + yNoiseMin) * extraNoise);

            }

            // sets the stars position based on formula and calculating sta
            stars[i].transform.position = new Vector3(x, y, 0);
        }


        // Create Bar Stars
        int j = numBarStars-1;
        float bulge = 1;
        for (int i = 0; i < numBarStars; i++)
        {
            // creates a star
            s1 = Instantiate(star);

            // set star id #
            s1.GetComponent<Star>().id = (i + numStars);

            // set star as child of starParent
            s1.transform.parent = _starsParent.transform;

            // add star to stars list
            stars.Add(s1);

            // sets position of star incrementally along bar of galaxy
            x = (float)((((x2-x1)/numBarStars)*i) + x1);
            y = (float)((((y2-y1)/numBarStars)*i) + y1);

            int dist = Math.Abs((numBarStars / 2) - i);
            //Debug.Log("Dist is: " + dist);
            bulge = 1 + (bulgeNoise * ((float)((numBarStars / 2) - dist) / (float)(numBarStars / 2)));
            //Debug.Log("Bulge is: " + bulge);

            j--;

            // introduces noise into the position of the star
            ranInt = random.Next(2);
            if (ranInt == 1)
            {
                x += (float)((random.NextDouble() * (xNoiseMax - xNoiseMin) + xNoiseMin)) * barNoiseX;
            }
            else
            {
                x -= (float)((random.NextDouble() * (xNoiseMax - xNoiseMin) + xNoiseMin)) * barNoiseX;
            }
            ranInt = random.Next(2);
            if (ranInt == 1)
            {
                y += (float)((random.NextDouble() * (yNoiseMax - yNoiseMin) + yNoiseMin)) * barNoiseY * bulge;
            }
            else
            {
                y -= (float)((random.NextDouble() * (yNoiseMax - yNoiseMin) + yNoiseMin)) * barNoiseY * bulge;

            }

            // set's star's position
            stars[i + numStars].transform.position = new Vector3(x, y, 0);
        }
    }

    public void createPaths()
    {
        if (debugOut == 1) Debug.Log("[Galaxy/createPaths]: Creating Paths");
        int i = 0;
        string pname;
        // Goes through Each Star
        foreach (GameObject s1 in stars)
            {
            // Runs checkNearby on it and goes through each resulting star
            foreach (Transform n1 in s1.GetComponent<Star>().checkNearby())
            {
                // Prepares to create path between star and nearby star
                bool create = true;
                
                // Always orders id by lower id star
                if (s1.GetComponent<Star>().id < n1.GetComponent<Star>().id)
                {
                    // Creates path name in the form "S1Name<->S2Name"
                    pname = s1.GetComponent<Star>().starName + "<->" + n1.GetComponent<Star>().starName;

                    // Makes sure path doesn't already exist
                    foreach (Transform p2 in s1.GetComponent<Star>().paths)
                    {
                        if (p2.GetComponent<Path>().id == pname)
                        {
                            create = false;
                        }
                    }
                    
                    // Creates path if it doesn't exist
                    if (create)
                    {
                        p1 = Instantiate(path);
                        p1.GetComponent<Path>().id = pname;
                        p1.GetComponent<Path>().name = pname;
                        p1.GetComponent<Path>().s1 = s1.transform;
                        p1.GetComponent<Path>().s2 = n1.transform;
                        if (s1.GetComponent<Star>().addPath(p1.transform) == -1) { if (debugOut == 1) Debug.Log("[Galaxy/createPaths]: Path already added"); }
                        if (n1.gameObject.GetComponent<Star>().addPath(p1.transform) == -1) { if (debugOut == 1) Debug.Log("[Galaxy/createPaths]: Path already added"); }
                        paths.Add(p1.transform);
                        //p1.SetActive(false);

                        i++;
                    }
                }
                else
                {
                    pname = n1.GetComponent<Star>().starName + "<->" + s1.GetComponent<Star>().starName;
                    foreach (Transform p2 in s1.GetComponent<Star>().paths)
                    {
                        if (p2.GetComponent<Path>().id != pname)
                        {
                            create = false;
                        }
                    }

                    if (create)
                    {
                        p1 = Instantiate(path);
                        p1.GetComponent<Path>().id = pname;
                        p1.GetComponent<Path>().name = pname;
                        p1.GetComponent<Path>().s1 = n1.transform;
                        p1.GetComponent<Path>().s2 = s1.transform;
                        s1.GetComponent<Star>().addPath(p1.transform);
                        n1.gameObject.GetComponent<Star>().addPath(p1.transform);
                        paths.Add(p1.transform);
                        //p1.SetActive(false);

                        i++;
                    }
                }   
            }
        }
        if (debugOut >= 1) Debug.Log("[Galaxy/createPaths]: " + i + " Paths Created");
            
        // Sort List by X pos
        //starPos.Sort(xsort);

        // Iterate backwards until x_dist is > maxDist
            // Check if dist < maxDist, if so
            // Create Path
            // Iterate forwards until x_dist is > maxDist
            // Check if dist < maxDist, if so
            // Create Path 
    }

    // compare function comparing x component of a transform
    private static int xsort(Transform x1, Transform x2)
    {
        if (x1.position.x > x2.position.x)
        {
            return 1;
        }
        else return -1;
    }
}
