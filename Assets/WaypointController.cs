using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour {

    [SerializeField]
    private GameObject[] waypoints;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject[] WaypointForDangerousPlace(GameObject dangerousPlace)
    {
        Debug.Log(dangerousPlace.name);
        switch (dangerousPlace.name)
        {
            case "Toilet":
                return new GameObject[] { waypoints[1], waypoints[0] };
            case "Stairs":
                return new GameObject[] { waypoints[1] };
            case "Chemicals":
                return new GameObject[] { waypoints[2] };
            case "Oven":
                return new GameObject[] { waypoints[2] };
            case "Knife":
                return new GameObject[] { waypoints[2] };
            case "Plants":
                return new GameObject[] { waypoints[3] };
            default:
                break;
        }

        return null;
    }
}
