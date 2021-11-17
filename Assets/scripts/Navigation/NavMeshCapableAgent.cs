using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshCapableAgent : MonoBehaviour{

    public bool onMesh = false;
    public List<GameObject> path;

    public Vector2 rawGoal;

    //This method is called by the enemy's hunt state and walks towards the first point in path
    public (GameObject, int) AStar(GameObject s, GameObject g, List<GameObject> nodes)
    {

        //TODO test the bitch
        GameObject start = FindNearestMovePoint(s, nodes);
        GameObject goal = FindNearestMovePoint(g, nodes);

        //Debug.Log("start: " + start);
        //Debug.Log("Goal: " + goal);
        rawGoal = g.transform.position;

        if (!onMesh)
        {
            onMesh = true;
            return (start, 1); //Returns the first point of the path to move to, as well as teleporting to that location so that we can snap to the map;
        }

        List<GameObject> openSet = new List<GameObject>();
        openSet.Add(start);

        Dictionary<GameObject, GameObject> cameFrom = new Dictionary<GameObject, GameObject>();
        foreach (GameObject obj in nodes)
        {
            cameFrom.Add(obj, null);
        }

        Dictionary<GameObject, float> gScore = new Dictionary<GameObject, float>();
        foreach(GameObject obj in nodes){
            gScore.Add(obj, int.MaxValue);
        }
        gScore[start] = 0;

        Dictionary<GameObject, float> fScore = new Dictionary<GameObject, float>();
        foreach (GameObject obj in nodes)
        {
            fScore.Add(obj, int.MaxValue);
        }
        fScore[start] = H(start);

        while(openSet.Count > 0)
        {
            GameObject current = openSet[0];
            foreach(GameObject test in openSet)
            {
                if(fScore[test] < fScore[current])
                {
                    current = test;
                }
            }
            

            if(current == goal)
            {
                while (cameFrom[current] != start) {
                    Debug.Log(current);
                    if(cameFrom[current] == null)
                    {
                        cameFrom[current] = start;
                    }
                    current = cameFrom[current];
                }
                
                return (current, 0);
            }

            openSet.Remove(current);
            List<GameObject> adjs = current.GetComponent<MovePoint>().Adj;
            //Something here is wrong because of where the came from is actually doing
            for(int i = 0; i < adjs.Count; i++)
            {
                float tentativeGScore = gScore[current] + 1;
                if(tentativeGScore < gScore[adjs[i]])
                {
                    cameFrom[adjs[i]] = current;
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

    private float H(GameObject a)
    {
        return Vector2.Distance(a.transform.position, rawGoal);
    }

    public GameObject FindNearestMovePoint(GameObject enemy, List<GameObject> pts)
    {
        GameObject ret = pts[0];
        float minDist = Vector2.Distance(enemy.transform.position, pts[0].transform.position);
        foreach (GameObject pt in pts)
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
