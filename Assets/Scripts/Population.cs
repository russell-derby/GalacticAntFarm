using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    public List<Transform> lifeforms;
    public List<long> popSizes;

    public List<long> growthFactor;

    //void Start() { }

    //void Update() { }

    public int addLifeform(Transform life, long initPop, long initGrowth)
    {
        lifeforms.Add(life);
        popSizes.Add(initPop);
        growthFactor.Add(initGrowth);

        return 1;
    }

    public int updatePopSizes()
    {
        for (int i = 0; i < lifeforms.Count; i++)
        {
            popSizes[i] *= growthFactor[i]; 
        }

        return 1;
    }
}
