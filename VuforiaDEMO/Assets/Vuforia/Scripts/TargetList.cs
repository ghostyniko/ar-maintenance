using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;

public class TargetList : MonoBehaviour

{
    private int oldSize = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();

        
         int sum = 0;
        foreach (TrackableBehaviour tb in tbs)
        {
            string name = tb.TrackableName;
            
                ImageTarget it = tb.Trackable as ImageTarget;
                Vector2 size = it.GetSize();
            sum++;
              //  Debug.Log("Active image target:" + name + "  -size: " + size.x + ", " + size.y +" "+ tb.transform.position);

            
        }
        
        if (sum != oldSize)
        {
            Debug.Log("SSize " + sum);
            oldSize = sum;
            foreach (TrackableBehaviour tb in tbs)
            {
                string name = tb.TrackableName;

                ImageTarget it = tb.Trackable as ImageTarget;
                Vector2 size = it.GetSize();
                
                  Debug.Log("Active image target:" + name + "  -size: " + size.x + ", " + size.y +" "+ tb.transform.position);


            }
        }
    }
}
