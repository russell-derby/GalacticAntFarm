using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 2;

    public GameObject GameEventMasterTemp;
    public GameObject GalaxyTemp;

    // cache
    public List<GameObject> activeSystems;

    // operational vars
    public GameObject temp;

    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        // Find and Connect to Application Master
        //_app = GameObject.Find("_app");
        
        DioBehavior.setGameMaster(transform);
        
        // Instantiate Game Event Master
        temp = Instantiate(GameEventMasterTemp);
        temp.transform.parent = transform;
        DioBehavior.setGameEventMaster(temp.transform);

        // Instantiate Galaxy
        temp = Instantiate(GalaxyTemp);
        temp.transform.parent = transform;
        DioBehavior.setGalaxyMaster(temp.transform);

        // Run Create Stars and Create Path
        DioBehavior._Galaxy.createStars();
        DioBehavior._Galaxy.Invoke("createPaths", 1f);

        if (debugOut >= 1) Debug.Log("[Game/Start]: Game Started");
    }

    // Update is called once per frame
    //void Update() { }
}
