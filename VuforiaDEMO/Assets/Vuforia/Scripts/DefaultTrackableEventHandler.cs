/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{

   
    public InputField InputField;

    private List<Entry> rendered = new List<Entry>();
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void Update()
    {
        string text;
        if (isActive())
        {
            text = "Marker aktivan";
        }
        else
        {
            text = "Marker nije aktivan";
        }

        InputField.text = text;
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;
        
        Debug.Log("Trackable Changed " + mTrackableBehaviour.TrackableName + 
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

      //  text.text += mTrackableBehaviour.TrackableName + "\n";

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Poziva se OnTrackingFound");
            ImageTargetBehaviour mTrackableBehaviour = GetComponent<ImageTargetBehaviour>();
            
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Poziva se OnTrackingLost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            Debug.Log("Poziva se OnTrackingLost, trebala bi");
          //  OnTrackingLost();
        }
        Debug.Log("Metoda zavrsava");
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {

        Debug.Log("Trackable Found " + mTrackableBehaviour.TrackableName +
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);
       
        if (mTrackableBehaviour)
        {
            Debug.Log("Broj " + GetNumberOfTrackables());
            if (GetNumberOfTrackables() != 0)
            {
               
                 return;
            }
            if (GetNumberOfTrackables() !=0 )
            {
              /*  var pdt = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
                pdt.ResetAnchors();
                pdt.Reset();*/
            }

            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
            {
                Debug.Log("Ime " + component.gameObject.name);
               // foreach (string s in rendered) Debug.Log("Prikazano " + s);
             //   Debug.Log("Prikazano "+rendered.Contains(component.gameObject.name));
                if (rendered.Where(e => e.ObjectName.Equals(component.gameObject.name)).ToList().Count>0)
                {
                    Debug.Log("Stanje liste: sadrzi");
                    return;
                }
                else
                {
                    Debug.Log("Stanje liste: ne sadrzi");
                    Debug.Log("Stanje liste: originalni objekt " + component.gameObject.name);
                    foreach (Entry e in rendered) Debug.Log("Stanje liste: " + e.ObjectName);

                }

                component.enabled = true;
                rendered.Add(new Entry { ObjectName = component.gameObject.name, TrackableName = mTrackableBehaviour.TrackableName});
            }

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;
        }
    }


    protected virtual void OnTrackingLost()
    {
        Debug.Log("Trackable Lost " + mTrackableBehaviour.TrackableName +
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            if (GetNumberOfTrackables() != 0)
            {
               /* var pdt = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
                pdt.ResetAnchors();
                pdt.Reset();*/
                
            }
            // Disable rendering:
            foreach (var component in rendererComponents)
            {
                component.enabled = false;
                //  rendered.RemoveAll(e => e.TrackableName == mTrackableBehaviour.TrackableName && e.ObjectName == component.gameObject.name);
                Debug.Log("Stanje liste: mièem");
                for (int i = 0; i < rendered.Count; i++)
                {
                    if (rendered[i].TrackableName.Equals(mTrackableBehaviour.TrackableName) && rendered[i].ObjectName.Equals(component.gameObject.name))
                    {
                        Debug.Log("Stanje liste, render " + rendered[i].TrackableName + " " + rendered[i].ObjectName);
                        rendered.RemoveAt(i);
                    }
                }
               
            }

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;
        }
    }

    #endregion // PROTECTED_METHODS

    #region Private Methods
    private int GetNumberOfTrackables()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
       
        IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();
        int sum = 0;
        foreach (TrackableBehaviour tb in tbs) sum++;
        return sum;
        
    }

    private class Entry
    {
        public string ObjectName { get; set; }
        public string TrackableName { get; set; }
    }

    private bool isActive()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();

        IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();
        foreach (var tb in tbs)
        {
            Debug.Log("Ime markera " + tb.TrackableName);
            if (tb.TrackableName.Equals("qr")) return true;
        }
        return false;
    }
    #endregion
}
