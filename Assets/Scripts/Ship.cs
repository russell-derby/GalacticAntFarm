using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 0;

    System.Random random;
    public PolygonCollider2D polyColl;
    public Sprite ship1;
    public Sprite ship2;

    public Transform path;
    public Transform dest;
    public Transform lifeForm;
    float dest_x, dest_y, angle;
    Vector3 pathVector;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();

        if (debugOut >= 1) Debug.Log("[Ship/Start]: Ship Created");

        //setPath();
        //transform.localScale = new Vector2(5.0F, 10.0F);
    }

    // Update is called once per frame
    void Update()
    {      
        transform.localPosition += pathVector;
    }

    /* void destReached()
    {

    }*/

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ship has collided!");
        if (collision.gameObject.transform == dest.transform)
        {
            Debug.Log("Ship has reached destination.");
            GameObject destPlanet = collision.gameObject;
            Star destScript = collision.gameObject.GetComponent<Star>();

            //add lifeform to planet
            destScript.addLife(transform.parent);
            lifeForm.gameObject.GetComponent<LifeForm>().addStar(transform);
            //add planet to lifeform
        }
    }*/

    public void setPath()
    {
        // choose random direction
        dest_x = (float)(random.NextDouble() * (0.1F + 0.1F) - 0.1F);
        dest_y = (float)(random.NextDouble() * (0.1F + 0.1F) - 0.1F);

        // set new path
        pathVector = new Vector3(0.25F*dest_x, 0.25F*dest_y, 0F);
        //Debug.Log("New Path - x: " + dest_x + " y: " + dest_y);

        // rotate ship to face point
        angle = (Mathf.Atan2(dest_y,dest_x) * Mathf.Rad2Deg) - 90F;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (debugOut == 1) Debug.Log("[Ship/setPath]: Random Path Set for Ship");
    }

    public void setPath(Transform start, Transform path)
    {   
        this.path = path;
        Path pathScript = path.gameObject.GetComponent<Path>();
        Vector3 dir;

        if (pathScript.s1 == start)
        {
            dir = new Vector3((pathScript.s2.position.x - pathScript.s1.position.x), (pathScript.s2.position.y - pathScript.s1.position.y), 0);
            dest = pathScript.s2;
        }
        else
        {
            dir = new Vector3((pathScript.s1.position.x - pathScript.s2.position.x), (pathScript.s1.position.y - pathScript.s2.position.y), 0);
            dest = pathScript.s1;
        }

        dir.Normalize();

        pathVector = new Vector3(0.025F*dir.x, 0.025F*dir.y, 0F);
        //Debug.Log("New Path - x: " + path.x + " y: " + path.y);

        angle = (Mathf.Atan2(pathVector.y,pathVector.x) * Mathf.Rad2Deg) - 90F;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (debugOut == 1) Debug.Log("[Ship/setPath]: Headed towards planet " + dest.gameObject.GetComponent<Star>().name);
    }
}
