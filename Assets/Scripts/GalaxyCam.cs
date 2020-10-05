using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyCam : MonoBehaviour
{
    //debug - (0 : none, 1 - all, 2 - inits)
    public int debugOut = 2;

    public float fieldOfView;
    public float rotatespeed;
    // Start is called before the first frame update
    void Start()
    {
        //rotatespeed = 0.0F;
        rotatespeed = 0.28F;
        fieldOfView = 80.0F;

        Camera.main.orthographicSize = fieldOfView;

        if (debugOut >= 1) Debug.Log("[GalaxyCam/Start]: GalaxyCam Started");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, rotatespeed) * Time.deltaTime);
    }
}
