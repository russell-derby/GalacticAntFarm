/*
* GameEventMaster.cs
* 
* Event Master is triggered by other game components. Creates game events by interacting with various UX/Game components.
*
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEventMaster : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int printDebug = 1;
    public int delay = 0;

    //common objects
    public Transform activeSys;

    /* EventLibrary Dictionary
     * key: string - describes name of event
     * val: List<Tuple(number of params, delegate pointing to master func)> - list of function calls to carry out for event */
    Dictionary<string, List<(int, System.Delegate)>> EventLibrary = new Dictionary<string, List<(int, System.Delegate)>>();

    // Delegates for loading into Dictionary
    delegate int del0();
    delegate int del1(object p1);
    delegate int del2(object p1, object p2);
    delegate int del3(object p1, object p2, object p3);
    delegate int del4(object p1, object p2, object p3, object p4);
    delegate int del5(object p1, object p2, object p3, object p4, object p5);


    // Start is called before the first frame update
    void Start()
    {
        // debug
        if (printDebug >= 1) Debug.Log("[GameEventMaster/Start]: GameEventMaster Started");

        activeSys = null;
        DioBehavior.setGameEventMaster(transform);

        del0 tdel = test;
        tdel();

        
        List<(int, System.Delegate)> temp = new List<(int, System.Delegate)>();
        del1 d1 = createGUI;
        del2 d2 = modifyGUI; 
        del1 d3 = destroyGUI;
        temp.Add((1, d1));
        temp.Add((2, d2));
        temp.Add((1, d3));

        EventLibrary.Add("test", temp);

        List<(int, System.Delegate)> sysCam = new List<(int, System.Delegate)>();
        del2 d4 = modifyGUI;
        sysCam.Add((2, d4));

        EventLibrary.Add("ActivateSystemCamera", sysCam);
    }

    int test() { Debug.Log("[GameEventMaster/Test]: Test0"); return 1; }

    // Update is called once per frame
    void Update() 
    {
        //this is all a test, but is a good example for how to call a Game Event
        if (delay > 1500)
        {
            Debug.Log("[GameEventMaster/Update]: delay = " + delay);
            List<object> tmp1 = new List<object>();
            List<object> tmp2 = new List<object>(){ 5.0f, "modifyGUI_param_received" };
            List<object> tmp3 = new List<object>(){ transform };

            if (execEvent("test", new List<List<object>>(){ tmp1, tmp2, tmp3 }) == -1) Debug.Log("[GameEventMaster/Update]: Test Call Failed");
            Debug.Log("[GameEventMaster/Update]: Call Successful?");
            delay = -1;
        } 
        if (delay != -1) delay++;
    }

    public int execEvent(string funcKey, List<List<object>> eventParams)
    {
        Debug.Log("[GameEventMaster/execEvent]: Executing Event!");
        //make sure func key is in func dictionary
        if (!EventLibrary.ContainsKey(funcKey)) 
        {
            if (printDebug == 2) Debug.Log("[GameEventMaster/execEvent]: funcKey - " + funcKey + " not found in Event Library");
            return -1;
        }

        //verify correct number of params supplied for each delegate
        if (eventParams.Count != EventLibrary[funcKey].Count)
        {
            if (printDebug == 2) Debug.Log("[GameEventMaster/execEvent]: Number of Param Sets Given does not match expected");
            return -1;
        }
        for (int i = 0; i < eventParams.Count; i++)
        {
            if (eventParams[i].Count != EventLibrary[funcKey][i].Item1)
            {
                if (printDebug == 2) Debug.Log("[GameEventMaster/execEvent]: Number of Params Given for Param Set " + i + " does not match expected");
                return -1;
            }
            else Debug.Log("funcKey: " + funcKey + " - Param Set " + i + " Valid");
        }

        //execute functions assigned to funcKey in Event Library
        //go through each tuple in this event's function list
        for (int x = 0; x < EventLibrary[funcKey].Count; x++)
        {
            //runs the delegate (tuple Item2) with the correct num params (based on tuple Item1)
            switch (EventLibrary[funcKey][x].Item1)
            {
                case 0:
                    EventLibrary[funcKey][x].Item2.DynamicInvoke();
                    break;
                case 1:
                    EventLibrary[funcKey][x].Item2.DynamicInvoke(eventParams[x][0]);
                    break;
                case 2:
                    EventLibrary[funcKey][x].Item2.DynamicInvoke(eventParams[x][0], eventParams[x][1]);
                    break;
                case 3:
                    EventLibrary[funcKey][x].Item2.DynamicInvoke(eventParams[x][0], eventParams[x][1], eventParams[x][2]);
                    break;
                case 4:
                    EventLibrary[funcKey][x].Item2.DynamicInvoke(eventParams[x][0], eventParams[x][1], eventParams[x][2], eventParams[x][3]);
                    break;
                case 5:
                    EventLibrary[funcKey][x].Item2.DynamicInvoke(eventParams[x][0], eventParams[x][1], eventParams[x][2], eventParams[x][3], eventParams[x][4]);
                    break;
            }
        }

        return 1;
    }

    /********** Event Master Functions **********/

    /*** GUI Functions ***/ 
    int createGUI(object keyword)
    {
        // GUI Template // Modifiers
        Debug.Log("[GameEventMaster/createGui]: hi - 1");

        switch ((string)keyword)
        {
            case "SystemCamera":
                
                break;
            default:
                break;
        }

        return 1;
    }

    int createGUI(object keyword, object position)
    {
        // GUI Template // Modifiers
        Debug.Log("[GameEventMaster/createGui]: hi - 1");

        return 1;
    }

    int modifyGUI(object guiName, object system)
    {
        //Debug.Log("[GameEventMaster/modifyGui]: hi - 2 xscale (" + (float) xscale +") - name (" + (string) name + ")");
        if ((string)guiName == "SystemCamera") { Debug.Log("Nice! :-)"); }

        if (activeSys != null)
        {
            activeSys.GetComponent<PlanetarySystem>().disableSystem();
        }
        
        activeSys = (Transform)system;
        activeSys.GetComponent<PlanetarySystem>().enableSystem();

        return 1;
    }
    int destroyGUI(object test)
    {
        Debug.Log("[GameEventMast/destroyGUI]: hi - 3 GameEventMaster.printDebug = " + ((Transform)test).GetComponent<GameEventMaster>().printDebug);
        return 1;
    }

    /*** Camera Functions ***/

    //int createCamera(Cam Template // Modifiers)
    //int modifyCamera()
    //int destroyCamera(Transform)

    /*** Sound Functions ***/

    //int playSound()
    //int stopSound()
    //int modifySound()

    /*** DB Functions ***/

    //int createDBRecord()
    //int modifyDBRecord()
    //int destroyDBRecord()



    /**** Entities should be created and managed by their parent ****/
    //int createEntity()
    //int modifyEntity()
    //int destroyEntity()
}
