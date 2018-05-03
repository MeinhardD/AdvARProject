using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingModels : MonoBehaviour {

    public ImageTargetBehaviour part1;
    public ImageTargetBehaviour part2;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        bool tracked1 = part1.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;
        bool tracked2 = part2.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;

        if (tracked1)
        {
            if (tracked2)
                Debug.Log("BOTH ARE TRACKED!");
            else
                Debug.Log("Can't find part 2");
        }
        else
        {
            if (tracked2)
                Debug.Log("Can't find part 1");
            else
                Debug.Log("Can't find either");
        }
    }
}
