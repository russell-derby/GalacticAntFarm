using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 1;

    // UI Prefabs
    public GameObject menuCamPref;
    public GameObject menuCanvasPref;
    public GameObject gameCamPref;
    public GameObject gameCanvasPref;
    public GameObject gameBGCamPref;
    public GameObject gameBGCanvasPref;
    public GameObject sysCamPref;

    // UI Instances
    public GameObject menuCam;
    public GameObject menuCanvas;
    public GameObject gameCam;
    public GameObject gameCanvas;
    public GameObject gameBGCam;
    public GameObject gameBGCanvas;
    public GameObject sysCam;

    // Variables 
    public List<Transform> active;
    System.Random random;

    // Operating Variables
    Transform sysStar;
    public GameObject db;

    // Start is called before the first frame update
    void Awake()
    {
        //initate list of active components
        active = new List<Transform>();
        
        //seed random
        random = new System.Random();
        
        //set star pointer = null
        sysStar = null;
    }

    /**************  Master Functions **************/

    public void destroyActive()
    {
        foreach (Transform t1 in active)
        {
            Destroy(t1.gameObject);
        }
        active.Clear();
    }

    /*************  End Master Functions **************/

    /**************  Main Menu Functions **************/

    // Start Main Menu Scene
    public void startMainMenu()
    {
        if (debugOut == 1) Debug.Log("[UI Master/startMainMenu]: Activating Main Menu UI");

        // Destroy Active UI Components
        destroyActive();

        // Instantiate Menu Cam
        menuCam = Instantiate(menuCamPref);
        menuCam.transform.parent = transform;
        active.Add(menuCam.transform);

        // Instantiate Menu Canvas
        menuCanvas = Instantiate (menuCanvasPref);
        menuCanvas.transform.parent = transform;
        active.Add(menuCanvas.transform);
    }

    /*********  End Main Menu Functions **********/

    /**************  Game Functions **************/

    // Start Main Game Scene
    void startGame()
    {
        if (debugOut == 1) Debug.Log("[UI Master/startGame]: Activating Game UI");

        //Destroy Active UI Components
        destroyActive();

        // Instantiate Game Cam
        gameCam = Instantiate(gameCamPref);
        gameCam.transform.parent = transform;
        active.Add(gameCam.transform);

        // Instantiate Game Canvas
        gameCanvas = Instantiate(gameCanvasPref);
        gameCanvas.transform.parent = transform;
        active.Add(gameCanvas.transform);
        // Set Game Cam as Render Cam of Game Canvas
        gameCanvas.GetComponent<Canvas>().worldCamera = gameCam.GetComponent<Camera>();

        // Instantiate Game Background Cam
        gameBGCam = Instantiate(gameBGCamPref);
        gameBGCam.transform.parent = transform;
        active.Add(gameBGCam.transform);

        // Instantiate Game Background Canvas
        gameBGCanvas = Instantiate(gameBGCanvasPref);
        gameBGCanvas.transform.parent = transform;
        active.Add(gameBGCanvas.transform);
        // Set BackgroundCam as Render Cam of Background Canvas
        gameBGCanvas.GetComponent<Canvas>().worldCamera = gameBGCam.GetComponent<Camera>();
    }

    // Start System Cam
    public void startSysCam(Transform s1)
    {
        if (debugOut == 1) Debug.Log("[UI Master/startSysCam]: Receiving Star with ID " + s1.GetComponent<Star>().id);
        if (!sysCam)
        {
            sysCam = Instantiate(sysCamPref);
            sysCam.transform.parent = transform;
            sysCam.GetComponent<Camera>().rect = new Rect(0.717F, 0.03F, 0.27F, 0.36F);
        }

        if (sysStar != null)
        {
            if (sysStar.GetComponent<Star>().id == s1.GetComponent<Star>().id)
            {
                if (debugOut == 1) Debug.Log("[UI Master/startSysCam]: Star Cam Already Active");
            }
            else
            {
                if (debugOut == 1) Debug.Log("[UI Master/startSysCam]: Deactivating System");
                sysStar.GetComponent<Star>().system.GetComponent<PlanetarySystem>().disableSystem();
            }
        }

        sysCam.GetComponent<Camera>().backgroundColor = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        sysStar = s1;
        sysStar.GetComponent<Star>().system.GetComponent<PlanetarySystem>().enableSystem();
    }

    // Destroy System Cam
    void destroySysCam()
    {
        if (sysCam)
        {
            
        }
        else
        {
            if (debugOut == 1) Debug.Log("[UIMaster/destroySysCam]: No Sys Cam Active");
        }
    }

    /*************  End Game Functions **************/
}
