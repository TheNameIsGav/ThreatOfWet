using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshGenerator : MonoBehaviour
{
    public List<GameObject> navPoints;

    [SerializeField]
    GameObject player;


    //Make it so that we build a matrix of all points and then run through and connect them to the closest points (And also the other point on their block)
    // Start is called before the first frame update
    void Start()
    {
        navPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("NavPoint"));
        Debug.Log(navPoints.Count);
        //navPoints.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("JumpPoint")));

        //Debug.Log(Vector2.Distance(navPoints[0].transform.position, navPoints[1].transform.position));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
