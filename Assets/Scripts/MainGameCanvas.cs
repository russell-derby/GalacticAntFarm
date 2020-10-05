using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameCanvas : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 1;

    public GameObject logPanel;
    public GameObject logButton;
    public GameObject gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        //logPanel.SetActive(false);
        gameMenu.SetActive(false);
    }

    // Toggle open/close the Life Log
    void toggleLog()
    {
        if (debugOut == 1) Debug.Log("[MainGameCanvas/toggleLog]: Toggle Game Log");
        if (logPanel.activeInHierarchy) { logPanel.SetActive(false); }
        else { logPanel.SetActive(true); }
    }

    // Toggle the In-Game Menu
    void toggleGameMenu()
    {
        if (debugOut == 1) Debug.Log("[MainGameCanvas/toggleGameMenu]: Toggle In-Game Menu");
        if (gameMenu.activeInHierarchy) { gameMenu.SetActive(false); }
        else { gameMenu.SetActive(true); }
    }
}
