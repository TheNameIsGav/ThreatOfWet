using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshGenerator : MonoBehaviour
{
    List<GameObject> navPoints;
    GameObject player;

    //Make it so that we build a matrix of all points and then run through and connect them to the closest points (And also the other point on their block)
    // Start is called before the first frame update
    void Start()
    {
        navPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("JumpPoint"));
        //navPoints.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("JumpPoint")));
        player = GameObject.Find("player");

        // Debug.Log(Vector2.Distance(navPoints[0].transform.position, navPoints[1].transform.position));
        // Debug.Log(FindNextPointAlongPath(GameObject.Find("EnemyBase").transform.position, 20f));
    }

    /// <summary>
    /// Takes an incoming position (Usually the position of an enemy) and a jump range, and returns the next point to walk to, and a boolean indicating whether or not to jump
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public (Vector2, bool) FindNextPointAlongPath(Vector2 pos, float range) 
    {
        List<GameObject> inRange = new List<GameObject>();
        GameObject standingPoint = navPoints[0];

        foreach (GameObject g in navPoints)
        {
            if(Vector2.Distance(g.transform.position, pos) <= range) //Find pt's within range
            {
                inRange.Add(g);
            }

            if(Vector2.Distance(g.transform.position, pos) <= Vector2.Distance(standingPoint.transform.position, pos)) //Find the closest point to pos currently
            {
                standingPoint = g;
            }
        }

        if(inRange.Count == 0)
        {
            return (Vector2.zero, false);
        }

        GameObject closestPt = inRange[0];
        foreach(GameObject g in inRange)
        {
            //Debug.Log(g.transform.position);
            if(Vector2.Distance(player.transform.position, closestPt.transform.position) > Vector2.Distance(player.transform.position, g.transform.position))
            {
                closestPt = g;
            }
        }

        //Debug.Log("Found target pt at " + closestPt.transform.position);

        if(Vector2.Distance(pos, player.transform.position) < Vector2.Distance(closestPt.transform.position, player.transform.position))
        {
            return (player.transform.position, false);
        }

        if(closestPt.transform.position.y == standingPoint.transform.position.y)
        {
            return (closestPt.transform.position, false);
        } else
        {
            return (closestPt.transform.position, true);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
