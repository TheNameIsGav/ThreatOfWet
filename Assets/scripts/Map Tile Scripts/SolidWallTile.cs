using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidWallTile : BasicTile
{
    Rigidbody2D rgb;
    // Start is called before the first frame update
    protected override void Start()
    {
      float xScale = transform.localScale.x;
      float yScale = transform.localScale.y;

      gameObject.transform.localScale = new Vector2(1,1);

      gameObject.GetComponent<SpriteRenderer>().size = new Vector2(xScale, yScale);
      
      gameObject.GetComponent<BoxCollider2D>().size = new Vector2(xScale, yScale);

  }
}
