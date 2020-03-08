using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomTrackable : Trackable
{
    // Start is called before the first frame update
    public string Name => "Custom";

    public int ID => 9999;
}
