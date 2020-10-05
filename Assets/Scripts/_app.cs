using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class _app : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 1; 

    //master objects
    public GameObject uiMaster;
    public GameObject database;
    public GameObject eventSystem;

    //public Scene mainMenu;
    //public Scene game;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //load game scene
        startMainMenu();
    }

    public void startMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        uiMaster.GetComponent<UIMaster>().startMainMenu();
        if (debugOut == 1) Debug.Log("[App/startMainMenu]: Menu Loaded");
    }

    public void startGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        if (debugOut == 1) Debug.Log("[App/startGame]: Game Loaded");
    }

    public void startOption()
    {
        if (debugOut == 1) Debug.Log("[App/startOption]: Options Loaded");
    }

    public void exitGame()
    {
        if (debugOut == 1) Debug.Log("[App/exitGame]: Ending Game");
        Application.Quit();
    }
}
