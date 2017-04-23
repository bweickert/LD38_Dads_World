using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
     [SerializeField]
    private float speed = 2f;
    private bool inGrabRangeForBaby = false;
    private bool inDropRange = false;
    private BabyController babyInRange;
    private Vector2 dropZone;
    private DaddyTask daddyTask;

    // Use this for initialization
    void Start () {
		
	}


    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * speed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (inGrabRangeForBaby && babyInRange != null)
            {
                babyInRange.BabyHasBeenGrabbed();
                return;
            }
                
            if (inDropRange && babyInRange != null && babyInRange.babyIsGrabbed)
            {
                Debug.Log("Drop the Baby");
                babyInRange.BabyHasBeenDropped(dropZone + Vector2.up*0.5f);
                return;
            }            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (daddyTask != null)
            {
                Debug.Log("Do the task");
                daddyTask.DoTheTask();
            }
        }
        if (babyInRange != null && babyInRange.babyIsGrabbed)
        {
            babyInRange.transform.position = transform.position + Vector3.up*1.5f;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log(coll.gameObject.name);
        if (coll.gameObject.tag == "Baby")
        {
            babyInRange = coll.gameObject.GetComponent<BabyController>();
            inGrabRangeForBaby = true;
            
        }
        if (coll.gameObject.tag == "PlayZone")
        {
            inDropRange = true;
            dropZone = coll.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Enter: " + coll.gameObject.name);
        DaddyTask aDaddyTask = coll.GetComponent<DaddyTask>();
        if (aDaddyTask != null)
            daddyTask = aDaddyTask;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("Exit: "+coll.gameObject.name);
        DaddyTask aDaddyTask = coll.GetComponent<DaddyTask>();
        if (aDaddyTask != null && aDaddyTask == daddyTask)
            daddyTask = null;
    }

        void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Baby")
            inGrabRangeForBaby = false;

        if (coll.gameObject.tag == "PlayZone")
            inDropRange = false;
    }
}
