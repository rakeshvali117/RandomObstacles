/**
 * Author:  Rakesh Kumar Vali
 * Created: 05.06.2019
 * Summary: Bob's functionality will be taken care by this class.
 **/
using System.Collections.Generic;
using UnityEngine;

public class BobBehaviour : MonoBehaviour
{
    public Transform    waypointObject;                     // Waypoint parent object reference 
    public GameObject   damageEffect, climaxEffect;         // Bob's Particle effects 
    public AudioClip    damageAudio, climaxAudio;           // Bob's Audio clips
    public AudioSource  audioSource;                        // Audio source to be used by BoB

    private int             wayPointIndex = 1;              // Waypoint index to access the waypoints            
    private List<Vector3>   wayPoints;                      // List of waypoints to be used by Bob


    // Start is called before the first frame update
    void Start()
    {
        GenerateWayPoints();
    }

    /// <summary>
    /// Populates waypoints from referenced waypoint parent 
    /// </summary>
    void GenerateWayPoints()
    {
        wayPoints = new List<Vector3>();

        foreach(Transform wpObj in waypointObject)
        {
            wayPoints.Add(wpObj.position);                  // Populates waypoints position from childs of waypoint parent
        }

        this.transform.position = wayPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();
    }

    /// <summary>
    /// Moves Bob to respective waypoints in the scene
    /// </summary>
    void MoveToWaypoint()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, wayPoints[wayPointIndex], Time.deltaTime * 10);      

        if(Vector3.Distance(this.transform.position, wayPoints[wayPointIndex]) <= 0f)       // Checks if Bob reached the target waypoint
        {
            wayPointIndex++;                                                                // Access next waypoint
            if(wayPointIndex > (wayPoints.Count - 1))
            {
                //Particle effects and sound effects
                EndEffect(false);
            }
        }
    }

    /// <summary>
    /// Effects and audio that needs to be played based on whether Bob hits obstacle or finishes waypoints
    /// </summary>
    /// <param name="damaged"> Whether bob is damaged by obstacle</param>
    void EndEffect(bool damaged)
    {
        if(damaged)
        {
            Instantiate(damageEffect, this.transform.position, Quaternion.identity);
            audioSource.clip = damageAudio;
        }
        else
        {
            Instantiate(climaxEffect, this.transform.position, Quaternion.identity);
            audioSource.clip = climaxAudio;
        }
        audioSource.Play();
        Destroy(this.gameObject);
    }

    //Collision check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "block")                // If Bob collides with obstacle
        {
            EndEffect(true);
        }
    }

}
