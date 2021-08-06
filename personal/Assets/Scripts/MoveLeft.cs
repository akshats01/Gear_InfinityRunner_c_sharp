using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{   private float speed=30;
    private PlayerController playerControllerScript;
    private float leftBound=-15;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript=GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {   if(playerControllerScript.isGameActive)
        {
        if(playerControllerScript.gameOver==false)
        {   if(!gameObject.CompareTag("Money"))
                transform.Translate(Vector3.left*Time.deltaTime*speed);
            else
                transform.Translate(Vector3.forward*Time.deltaTime*speed);  
        }
        if(transform.position.x<leftBound&&(gameObject.CompareTag("Obstacle")||gameObject.CompareTag("Wall")||gameObject.CompareTag("Money"))){
            Destroy(gameObject);
        }
        }
    }
}
