/**
 * Author:  Rakesh Kumar Vali
 * Created: 05.06.2019
 * Summary: Generates obstacles randomly within the floor space.
 **/
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public int          noOfObstacles;              // No of obstacles to be generated
    public GameObject   obstacle;                   // Prefab of the obstacle

    private Dictionary<Vector3, GameObject> obstacles;

    /// <summary>
    /// Starts the behaviour
    /// </summary>
    void Start()
    {
        GenerateObstacles();            
    }

    /// <summary>
    /// Generates Obstacles based on the floor size 
    /// </summary>
    void GenerateObstacles()
    {
        obstacles = new Dictionary<Vector3, GameObject>();

        for (int objCount = 0; objCount < noOfObstacles; objCount++)
        {
            Vector3 randPos;
            do
            {
                randPos = new Vector3(Random.Range(-60f, 60f), 1f, Random.Range(-57f, 62f));        // Random position for the obstacle
            } while (obstacles.ContainsKey(randPos));                                               // Checks if there is already an obstacle in the position generated.

            var obj = Instantiate(obstacle, randPos, Quaternion.identity);                          // Instantiates an obstacle and adds it as a child this gameobject
            obj.transform.parent = this.transform;
            obstacles.Add(randPos, obj);                                                            // Adds this position and object to dictionary.
        }
    }

}
