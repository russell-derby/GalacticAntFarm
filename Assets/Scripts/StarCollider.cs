using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCollider : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits, 3 - interactives)
    public int debugOut = 3;

    // Game Event Master
    public GameObject _gameEventMaster;

    // Properties
    public Star host;
    public Collider2D nearbyCollider;

    // Active Variables
    public List<Transform> nearbyObjects;
    public int numNearby;

    // Operational Variables
    List<Collider2D> results = new List<Collider2D>();
    ContactFilter2D filter = new ContactFilter2D();
    List<object> funcParams1 = new List<object>();
    List<object> funcParams2 = new List<object>();
    List<object> funcParams3 = new List<object>();
    List<List<object>> funcs1 = new List<List<object>>();
    List<List<object>> funcs2 = new List<List<object>>();
    List<List<object>> funcs3 = new List<List<object>>();

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0,0,-5f);
        host = transform.parent.gameObject.GetComponent<Star>();
    }

    // Update is called once per frame
    //void Update() {  }

    /*void OnMouseEnter()
    {
        if (debug) Debug.Log("Touching star collider of " + transform.parent.GetComponent<Star>().starName);
        Star touchedStar = transform.parent.gameObject.GetComponent<Star>();
        Text text = GameObject.Find("Text").GetComponent<Text>();
        text.text = (touchedStar.starName);
        if (touchedStar.lifeForms.Count > 0)
        {
            text.text += ("\n\nLifeForms: " );
        }
        foreach (Transform life in touchedStar.lifeForms)
        {
            LifeForm lifeScript = life.gameObject.GetComponent<LifeForm>();
            text.text += ("\n - " + lifeScript.sName);
            text.text += ("\n   Occupied Systems: " + lifeScript.occSystems.Count);
            text.text += ("\n");
        }
        text.text += "\n\nEnd Info";
    }*/

    void OnMouseDown()
    {
        StartCoroutine("SystemCamera");
        /*

        
        //clear event lists
        funcParams1.Clear();
        funcs1.Clear();
        
        //create param list with parent star's transform
        funcParams1.Add((object)"SystemCamera");
        funcParams1.Add((object)host.system);
        //adds list to function list
        funcs1.Add(funcParams1);

        _gameEventMaster.GetComponent<GameEventMaster>().execEvent("ActivateSystemCamera", funcs1);
        if (debugOut == 3) Debug.Log("[StarCollider/OnMouseDown]: Star sending: " + host.starName);
        */
    }

    IEnumerator SystemCamera()
    {
        if (host.system == null) 
        {
            Debug.Log("[StarCollider]: Calling Create System");
            host.createSystem();
            yield return null;
        }
        host.disableSystem();

        //clear event lists
        funcParams1.Clear();
        funcs1.Clear();
        
        //create param list with parent star's transform
        funcParams1.Add((object)"SystemCamera");
        funcParams1.Add((object)host.system);

        //adds list to function list
        funcs1.Add(funcParams1);

        DioBehavior._GameEventMaster.execEvent("ActivateSystemCamera",funcs1);
        Debug.Log("Test: " + host.id);
    }

    public List<Transform> returnNear()
    {
        nearbyObjects.Clear();
        numNearby = nearbyCollider.OverlapCollider(filter.NoFilter(), results);
        if (debugOut == 1) Debug.Log("[StarCollider/returnNear]: Collider Position: X " + nearbyCollider.transform.position.x + " Y " + nearbyCollider.transform.position.y);
        //Debug.Log(numNearby + " nearby");
        foreach (Collider2D coll in results)
        {
            if (debugOut == 1) Debug.Log("[StarCollider/returnNear]: Collision Position: X " + coll.transform.position.x + " Y " + coll.transform.position.y);
            if (!nearbyObjects.Contains(coll.transform))
            {
                nearbyObjects.Add(coll.transform);
            }
        }

        return nearbyObjects;
    }
}
