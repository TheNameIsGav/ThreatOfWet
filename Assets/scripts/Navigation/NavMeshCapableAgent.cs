using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshCapableAgent : MonoBehaviour{

    public bool onMesh = false;
    public List<GameObject> path;

    public void AStar()
    {

    }

    public GameObject GetOnMesh(GameObject enemy, List<GameObject> pts)
    {
        GameObject ret = null;
        float minDist = Vector2.Distance(enemy.transform.position, pts[0].transform.position);
        foreach (GameObject pt in pts)
        {
            float dist = Vector2.Distance(enemy.transform.position, pt.transform.position);
            if(minDist > dist)
            {
                ret = pt;
                minDist = dist;
            }
        }

        if(ret == null)
        {
            Debug.Log("OH SHIT WE FOUND A NULL SOMETHING IN NavMeshCapableAgent");
            return enemy;
        }

        onMesh = true;
        return ret;
    }

        /*//Get rectangle of walking points
        Vector2 rectA = new Vector2(enemy.transform.position.x + 10, enemy.transform.position.y + 2);
        Vector2 rectB = new Vector2(enemy.transform.position.x - 10, enemy.transform.position.y - 2);
        Collider2D[] inRangePts = Physics2D.OverlapAreaAll(rectA, rectB);
        List<GameObject> validPts = new List<GameObject>();

        foreach(Collider2D c in inRangePts)
        {
            RaycastHit2D cast = Physics2D.Raycast(transform.position, c.transform.position);
            if(cast.collider == null) //No Hit so we can count this as a valid move point
            {
                validPts.Add(c.gameObject);
            }
        }

        if(validPts.Count > 0) 
        {
            GameObject ret = validPts[0];
            float dist = Vector2.Distance(ret.transform.position, player.transform.position) + Vector2.Distance(ret.transform.position, enemy.transform.position);
            foreach(GameObject g in validPts)
            {
                float checkDist = Vector2.Distance(g.transform.position, player.transform.position) + Vector2.Distance(g.transform.position, enemy.transform.position);
                if (dist > checkDist)
                {
                    ret = g;
                    dist = checkDist;
                }
            }
        }

        //If here, then there are no points in range that are also reachable without collisions
        //Get circle of potential jump points



        //Teleport to the nearest Point straightup
        return null;*/
   
}
