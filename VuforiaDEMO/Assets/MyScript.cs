
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    private GameObject ob;

    //public GameObject obj;

    // UnityEngine.SpatialTracking.TrackedPoseDriver t;


    // Start is called before the first frame update
    void Start()
    {
        ob = GameObject.Find("CubeA");
        //  t = this.GetComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>();

        //  obj =(GameObject) Instantiate(obj);

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.touchCount > 0)
        {


            Vector3 pomak = new Vector3(0, 0, 1);
            ob.transform.position += pomak;
            // transform.position += pomak;

            //  t.UseRelativeTransform = false;



        }
    }
}
