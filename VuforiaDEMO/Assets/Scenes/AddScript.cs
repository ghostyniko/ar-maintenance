using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class AddScript : MonoBehaviour
{
    public GameObject tracker;

    public void Add(GameObject otherTracker)
    {
        var script = otherTracker.GetComponent<TrackedController>();
        
        GameObject gameObject = Instantiate(script.track, otherTracker.transform);
        //var comp = tracker.AddComponent(prefab.GetType());
        gameObject.transform.SetParent ( tracker.transform);
        var sessionId = PlayerPrefs.GetInt("session");
       
        Database.AddObject(gameObject, sessionId,script.type);


        /*Database.AddObject(gameObject, SessionData.sessionID);
        Objects.objects.Add(gameObject);*/
    }


    
}
