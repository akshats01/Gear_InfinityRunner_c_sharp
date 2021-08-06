using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startPos;
    public float size=2.05f;
    private float repeatWidth;
    void Start()
    {   startPos=transform.position;
        repeatWidth=GetComponent<BoxCollider>().size.x*size;
    }

    // Update is called once per frame
    void Update()
    {   if(transform.position.x<startPos.x-repeatWidth){
        transform.position=startPos;
        }
    }
}
