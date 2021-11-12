using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshGenerator : MonoBehaviour
{
    List<GameObject> navPoints;

    [SerializeField]
    GameObject player;


    //Make it so that we build a matrix of all points and then run through and connect them to the closest points (And also the other point on their block)
    // Start is called before the first frame update
    void Start()
    {
        navPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("NavPoint"));
        //navPoints.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("JumpPoint")));

        Debug.Log(Vector2.Distance(navPoints[0].transform.position, navPoints[1].transform.position));
    }


    float h(GameObject node)
    {
        return Vector2.Distance(node.transform.position, player.transform.position);
    }

    /// <summary>
    /// Takes an incoming position (Usually the position of an enemy) and a jump range, and returns the next point to walk to, and a boolean indicating whether or not to jump
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>

    //TODO Place the enemy "On Mesh"
    //Run A* star a reclaculate path every time they get to the new point

    public Vector2 Astar(GameObject e)
    { return Vector2.zero; }



    // Update is called once per frame
    void Update()
    {
        
    }
}
