using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCollider : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits, 3 - interactives)
    public int debugOut = 3;

    public List<Transform> nearbyObjects;
    public Star host;
    public int numNearby;
    public Collider2D collider;

    List<Collider2D> results = new List<Collider2D>();
    ContactFilter2D filter = new ContactFilter2D();

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
        if (host.system == null) 
        {
            host.createSystem();
        }
        
        host.game.GetComponent<Game>().createSystemCam(transform.parent);
        if (debugOut == 3) Debug.Log("[StarCollider/OnMouseDown]: Star sending: " + host.starName);
    }

    public List<Transform> returnNear()
    {
        nearbyObjects.Clear();
        numNearby = collider.OverlapCollider(filter.NoFilter(), results);
        if (debugOut == 1) Debug.Log("[StarCollider/returnNear]: Collider Position: X " + collider.transform.position.x + " Y " + collider.transform.position.y);
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
