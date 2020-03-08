using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedObjectSelection : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject tracker;

    private GameObject selected;
    private TrackedController controller;
    private Transform spawn;
    private void Start()
    {
        controller = tracker.GetComponent<TrackedController>();
        spawn = tracker.transform.Find("Spawn");
    }
    public void SetScrew()
    {
        selected = objects[0];
        Debug.Log("promjena objekta pritisnuto");
       // Transform transform = controller.track.transform;

        Destroy(controller.track);
        var newObject = Instantiate(objects[0], tracker.transform);

        var pos = newObject.transform.position;
        newObject.transform.localPosition = spawn.localPosition;
      //  newObject.transform.rotation = transform.rotation;
        controller.track = newObject;
        controller.type = "saraf";

     /*   Debug.Log("promjena objekta " + obj.name);
        Destroy(obj);*/
    }

    public void SetFrame()
    {
        selected = objects[1];
     
        Debug.Log("promjena objekta pritisnuto");
        //Transform transform = controller.track.transform;

        Destroy(controller.track);
        var newObject = Instantiate(objects[1], tracker.transform);
       /* newObject.transform.position = transform.position;
        newObject.transform.rotation = transform.rotation;*/
        controller.track = newObject;
        controller.type = "okvir";
    }

    public void SetScrewDriver()
    {
        selected = objects[2];

        Debug.Log("promjena objekta pritisnuto");
      //  Transform transform = controller.track.transform;

        Destroy(controller.track);
        var newObject = Instantiate(objects[2], tracker.transform);
        newObject.transform.localPosition = spawn.localPosition;
        // newObject.transform.position = transform.position;
      //  newObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        controller.track = newObject;
        controller.type = "odvijac";
    }
}
