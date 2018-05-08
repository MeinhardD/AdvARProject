using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingModels : MonoBehaviour {

    public ImageTargetBehaviour currentPart;
    public ImageTargetBehaviour nextPart;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        bool tracked1 = currentPart.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;
        bool tracked2 = nextPart.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;

        if (tracked1)
        {
            if (tracked2)
                Debug.Log("Put the part with a sphere on the part with a cube");
            else
                Debug.Log("Can't find the next part");
        }
        else
        {
            if (tracked2)
                Debug.Log("Can't find the current part");
            else
                Debug.Log("Can't find either part");
        }
    }
}
