using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{

    [SerializeField]
    List<MovePoint> adj;
    public List<MovePoint> Adj { get { return adj; } set { adj = value; } }

    // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
