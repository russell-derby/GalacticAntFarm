using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Gas 
{

}

public class Body : MonoBehaviour
{
    //General Traits
    public bool isHabitable;
    public float habitability;

    //Atmosphere Composition
    public int numGasses;
    float maxGasses;
    float[] atmosphere;
    string[] gasses = new string[] { "Carbon Dioxide", "Hydrogen", "Nitrogen", "Oxygen", "Helium", "Methane", "Argon", "Carbon Monoxide"};
    
    //Body Composition
    public int numMinerals;
    float maxMinerals;
    float[] body;
    string[] crustMinerals = new string[] { "silicon", "aluminum", "iron", "calcium", "sodium", "potassium", "magnesium", "carbon" };
    string[] mantleMinerals = new string[] { "Silicon", "Magnesium", "iron" };
    string[] coreMinerals = new string[] { "Iron", "Nickel" };

    //Surface Liquid
    public int numLiquids;
    float maxLiquids;
    float[] oceans;
    string[] liquids = new string[] { "water", "sulfuric acid" };

    //Life
    List<Transform> populations;
    List<Transform> lifeforms;

    //Prefabs
    public GameObject pref_population;
    public GameObject pref_lifeform;

    //Operational Vars
    System.Random random;
    Transform p1, p2, l1, l2;
    double lifeTick;

    void Start()
    {
        random = new System.Random(GetInstanceID());

        populations = new List<Transform>();
        lifeforms = new List<Transform>();

        if ((habitability = (float)random.NextDouble()) < 0.5f)
        {
            isHabitable = false;
        }
        
        atmosphere = new float[numGasses];
    }

    // Update is called once per frame
    //void Update() {    }

    /*void FixedUpdate()
    {
        foreach ( Transform pop in populations )
        {
            pop.GetComponent<Population>().updatePopSizes();
        }
    }*/

    void createPopulation()
    {
        p1 = Instantiate(pref_population).transform;
    }

    void createLifeform()
    {
        l1 = Instantiate(pref_lifeform).transform;
    }
}
