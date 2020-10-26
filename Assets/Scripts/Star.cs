using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 1;
    
    System.Random random;
    public GameObject starGraphic;
    public SpriteRenderer rend;
    public Component halo;
    public GameObject templateColl;
    public GameObject nearbyColl;
    public GameObject game;
    public GameObject sysPrefab;

    public Transform system = null;
    public PlanetarySystem sysScript = null;
    public int maxNearby = 10;

    public int id;
    public string starName;
    Collider2D[] results;
    ContactFilter2D filter;
    public List<Transform> lifeForms;
    public List<Transform> nearbyObjects;
    public List<Transform> nearbyStars;
    public List<Transform> paths;
    
    //public bool scanned;
    public bool sent;

    void Awake()
    {
        nearbyColl = Instantiate(templateColl, transform, false);
        nearbyColl.GetComponent<StarCollider>().host = GetComponent<Star>();

        random = new System.Random(GetInstanceID());

        genName();  
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = starGraphic.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    //void Update() { }

    // FixedUpdate is called once per frame after Update. Use for physics.
    //void FixedUpdate() { }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Planet has had ship collide into it!");
        if (collision.gameObject.GetComponent<Ship>().dest == transform)
        {
            //Debug.Log("Ship has reached destination.");
            //collision.transform.localScale = new Vector2(10F, 10F);
            //add lifeform to planet
            if(!lifeForms.Any()) { 
                addLife(collision.transform.parent);
                collision.transform.parent.GetComponent<LifeForm>().addStar(transform);
            }
            collision.transform.parent.GetComponent<LifeForm>().destroyShip(collision.transform);
            //add planet to lifeform
        }        
    }

    // Called when mouse hovers over object
    //void OnMouseOver() { }

    /*void OnMouseDown()
    {
        if (system == null) 
        {
            createSystem();
        }
        
        game.GetComponent<Game>().createSystemCam(transform);
        Debug.Log("Star sending: " + id);
    }*/

    void genName()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
        string n1 = new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
        starName = n1;
        if (debugOut == 1) Debug.Log("[Star/genName]: Star Name: " + starName);
        gameObject.name = n1;
    }

    public void setColor(Color c1)
    {
        rend.color = c1;
    }

    public void setSpriteScale(Vector2 scale)
    {
        starGraphic.transform.localScale = scale;
    }

    public void createSystem()
    {
        system = Instantiate(sysPrefab).transform;
        if (debugOut == 1) Debug.Log("[Star/createSystem]: New System! x:" + system.position.x + " y:" + system.position.y);

        sysScript = system.gameObject.GetComponent<PlanetarySystem>();
        system.SetParent(transform);
        sysScript.sysName = starName;
        sysScript.star = gameObject;
    }

    public void enableSystem()
    {
        sysScript.enableSystem();
    }

    public void disableSystem()
    {
        sysScript.disableSystem();
    }

    public void addLife(Transform l1)
    {
        if (!lifeForms.Any())
        {
            if (debugOut == 1) Debug.Log("[Star/addLife]: Life Added to star :-)");
            lifeForms.Add(l1);
            createSystem();
            sysScript.disableSystem();
            setColor(l1.gameObject.GetComponent<LifeForm>().color);
            setSpriteScale(new Vector2(3f,3f));
            //transform.localScale = new Vector3(2.5F, 2.5F, 0.5F);
        }
        else { if (debugOut == 1) Debug.Log("[Star/addLife]: Life rejected from star!" ); }
    }

    public List<Transform> checkNearby()
    {

        nearbyStars.Clear();
        // Runs returnNear() on longrange collider
        foreach (Transform obj in nearbyColl.gameObject.GetComponent<StarCollider>().returnNear())
        {
            // Check if object is a star
            if (obj.gameObject.GetComponent<StarCollider>() != null)
            {
                // Checks if it already has it in nearbyStars or if it is itself
                if (!nearbyStars.Contains(obj.parent) && obj.parent != transform)
                {
                    // Adds it to the list
                    nearbyStars.Add(obj.parent);
                }
            }
        }

        return nearbyStars;
    }

    public int addPath(Transform p1)
    {
        if (!paths.Contains(p1))
        {
            paths.Add(p1);
            return 1;
        }
        else { return -1; }
    }
}
