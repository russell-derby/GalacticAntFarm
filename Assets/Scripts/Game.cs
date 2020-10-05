using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 2;

    // master
    public GameObject _app;
    public GameObject uiMaster;
    public GameObject soundMaster;
    public GameObject db;
    public GameObject gameEventMaster;
    public GameObject galaxy;

    // cache
    public List<GameObject> activeSystems;

    // operational vars
    public Transform cam1;
    public Transform cam_star1;
    public Transform cam_sys1;

    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        // Find and Connect to Application Master
        //_app = GameObject.Find("_app");

        // Instantiate Game Event Master
        gameEventMaster = Instantiate(gameEventMaster);

        // Instantiate Galaxy
        galaxy = Instantiate(galaxy);

        // Run Create Stars and Create Path
        galaxy.GetComponent<Galaxy>().createStars();
        galaxy.GetComponent<Galaxy>().Invoke("createPaths", 1f);
        cam1 = null;

        if (debugOut >= 1) Debug.Log("[Game/Start]: Game Started");
    }

    // Update is called once per frame
    //void Update() { }


    // Create system cam creates mini planet cam. Called by clicked-on stars.
    public void createSystemCam(Transform star1)
    {
        if (debugOut == 1) Debug.Log("[Game/createSystemCam]: Starting Sys Cam");
        // Game Calls _app to create System Cam through UI Master
        _app.GetComponent<_app>().uiMaster.GetComponent<UIMaster>().startSysCam(star1);
    }
}
