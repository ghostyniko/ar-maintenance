using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject Target;
    public GameObject Frame;
    public GameObject ScDrw;
    public GameObject Screw;


    void Start()
    {
        int sessionId = PlayerPrefs.GetInt("session");

        var objects = Database.GetObjects(sessionId);
        Debug.Log("velicina objekata " + objects.Count);
        foreach (var o in objects)
        {
            GameObject go;

            if (o.Type.Equals("odvijac"))
            {
                go = ScDrw;
            }
            else if (o.Type.Equals("saraf"))
            {
                go = Screw;
            }
            else go = Frame;

            var gameObject = Instantiate(go, Target.transform);
            gameObject.transform.SetParent(Target.transform);

            gameObject.transform.localPosition = new Vector3((float)o.X, (float)o.Y, (float)o.Z);
            gameObject.transform.localRotation = Quaternion.Euler((float)o.A, (float)o.B, (float)o.C);

            gameObject.transform.localScale = new Vector3((float)o.SX, (float)o.SY, (float)o.SZ);
        }

    }

 
}
