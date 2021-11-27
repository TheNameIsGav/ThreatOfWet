using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{

    [SerializeField]
    List<GameObject> adj;
    public List<GameObject> Adj { get { return adj; } set { adj = value; } }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().forceRenderingOff = true;           
    }

    // Update is called once per frame
    public bool IsUnoccupied()
    {
        gameObject.AddComponent<BoxCollider2D>();
        Collider2D[] collidedColliders = Physics2D.OverlapCircleAll(
            transform.position, .25f);
        for (int i = 0; i < collidedColliders.Length; i++)
        {
            if (collidedColliders[i].gameObject.tag.Equals("Hostile"))
            {
                return false;
            }
        }
        return true;
    }
}
