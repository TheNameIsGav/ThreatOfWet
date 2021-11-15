using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshCapableAgent : MonoBehaviour{

    public bool onMesh = false;
    public List<MovePoint> path;

    public Vector2 rawGoal;

    //This method is called by the enemy's hunt state and walks towards the first point in path
    public (MovePoint, int) AStar(GameObject s, GameObject g, List<MovePoint> nodes)
    {

        //TODO test the bitch
        MovePoint start = FindNearestMovePoint(s, nodes);
        MovePoint goal = FindNearestMovePoint(g, nodes);
        rawGoal = g.transform.position;
        if (!onMesh)
        {
            onMesh = true;
            return (start, 1); //Returns the first point of the path to move to, as well as teleporting to that location so that we can snap to the map;
        }

        if(start == goal)
        {
            onMesh = false;
            return (null, 2); //The enemy and the player are as close as they could be (They share the same closest navpoint), so we should just move to the player
        }


        List<MovePoint> openSet = new List<MovePoint>();
        openSet.Add(start);

        List<MovePoint> cameFrom = new List<MovePoint>();

        Dictionary<MovePoint, float> gScore = new Dictionary<MovePoint, float>();
        gScore[start] = 0;

        Dictionary<MovePoint, float> fScore = new Dictionary<MovePoint, float>();
        fScore[start] = H(start);

        while(openSet.Count > 0)
        {
            MovePoint current = openSet[0];
            foreach(MovePoint test in openSet)
            {
                if(fScore[test] < fScore[current])
                {
                    current = test;
                }
            }

            if(current == goal)
            {
                return (cameFrom[0], 0);
            }

            openSet.Remove(current);
            List<MovePoint> adjs = current.GetComponent<MovePoint>().Adj;
            for(int i = 0; i < adjs.Count; i++)
            {
                float tentativeGScore = gScore[current] + 1;
                if(tentativeGScore < gScore[adjs[i]])
                {
                    cameFrom[i] = current;
                    gScore[adjs[i]] = tentativeGScore;
                    fScore[adjs[i]] = gScore[adjs[i]] + H(adjs[i]);

                    if (!openSet.Contains(adjs[i]))
                    {
                        openSet.Add(adjs[i]);
                    }

                }
            }
        }
        return (null, 3);
    }

    private float H(MovePoint a)
    {
        return Vector2.Distance(a.transform.position, rawGoal);
    }

    public MovePoint FindNearestMovePoint(GameObject enemy, List<MovePoint> pts)
    {
        MovePoint ret = null;
        float minDist = Vector2.Distance(enemy.transform.position, pts[0].transform.position);
        foreach (MovePoint pt in pts)
        {
            float dist = Vector2.Distance(enemy.transform.position, pt.transform.position);

            if (minDist > dist)
            {
                ret = pt;
                minDist = dist;
            }
        }

        if(ret == null)
        {
            Debug.Log("OH SHIT WE FOUND A NULL SOMETHING IN NavMeshCapableAgent");
            return null;
        }

        return ret;
    }   
}
