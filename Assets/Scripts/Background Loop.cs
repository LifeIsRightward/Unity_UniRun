using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    float width;

    void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -width)
        {
            Reposition();
        }
    }

    void Reposition()
    {
        Vector2 offset = new Vector2(width * 2f, 0f);
        transform.position = (Vector2)transform.position + offset;
    }
}
