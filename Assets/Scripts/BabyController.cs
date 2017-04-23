using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour {

    public bool babyIsGrabbed = false;

    private bool inTheDangerZone = false;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private WaypointController waypointController;
    private GameObject[] dangerousPlaces;
    private GameObject currentTarget;
    [SerializeField]
    private float fun = 100f;
    [SerializeField]
    private float funReduce = 0.5f;
    [SerializeField]
    private float funReducementTicker = 1.0f;
    private float timeAfterLastFunCheck = 0f;
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private LayerMask blockingLayer;
    private int currentWaypoint = 0;
    private GameObject[] waypoints;
    private BoxCollider2D bc2D; 

    // Use this for initialization
    void Start () {
        dangerousPlaces = gameController.dangerousPlaces;
        bc2D = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (babyIsGrabbed)
            return;

        if (inTheDangerZone)
            gameController.BabyInTheDangerZone();

        if (currentTarget != null)
        {
            if (waypoints != null)
            {
                if ((transform.position - waypoints[currentWaypoint].transform.position).sqrMagnitude < float.Epsilon)
                {
                    Debug.Log(currentWaypoint);
                    currentWaypoint++;
                }
                
            }
            MoveToCurrentTarget();

        }
        else
        {
            if (timeAfterLastFunCheck > funReducementTicker)
                CheckIfBabyWantsNewTarget();
            else
                timeAfterLastFunCheck = timeAfterLastFunCheck + Time.deltaTime;
        }

        fun = fun - funReduce / funReducementTicker * Time.deltaTime;
        if (fun < 0)
            fun = 0;
    }

    void MoveToCurrentTarget ()
    {

        Vector3 currentDestination = transform.position;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, currentTarget.transform.position, blockingLayer);

        if (hit)
        {
            if (waypoints != null)
                currentDestination = waypoints[currentWaypoint].transform.position;
        }
            
        else
            currentDestination = currentTarget.transform.position;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentDestination, step);
    }

    void CheckIfBabyWantsNewTarget ()
    {
        timeAfterLastFunCheck = 0f;
        if (Random.Range(0f, 100f) > fun)
        {
            int randomInt = (int)Random.Range(0, dangerousPlaces.Length);
            waypoints = waypointController.WaypointForDangerousPlace(dangerousPlaces[randomInt]);
            currentTarget = dangerousPlaces[randomInt];
            return;
        }
        
    }

    public void BabyHasBeenGrabbed ()
    {
        currentTarget = null;
        currentWaypoint = 0;
        babyIsGrabbed = true;
        bc2D.enabled = false;
    }

    public void BabyHasBeenDropped (Vector2 dropPosition)
    {
        babyIsGrabbed = false;
        bc2D.enabled = true;
        transform.position = dropPosition;
        fun = 100f;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "danger")
            inTheDangerZone = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "danger")
            inTheDangerZone = false;
    }
}
