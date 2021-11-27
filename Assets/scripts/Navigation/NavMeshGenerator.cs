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
        //navPoints.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("JumpPoint")));

        //Debug.Log(Vector2.Distance(navPoints[0].transform.position, navPoints[1].transform.position));
    }

    public GameObject FindNearestMovePoint(GameObject obj)
    {
        GameObject ret = navPoints[0];
        float minDist = Vector2.Distance(obj.transform.position, navPoints[0].transform.position);
        foreach (GameObject pt in navPoints)
        {
            float dist = Vector2.Distance(obj.transform.position, pt.transform.position);

            if (minDist > dist)
            {
                ret = pt;
                minDist = dist;
            }
        }

        if (ret == null)
        {
            Debug.Log("OH SHIT WE FOUND A NULL SOMETHING IN NavMeshCapableAgent");
            return null;
        }

        return ret;
    }
}
