using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerRb;
    public Transform orientation;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround=true;
    public bool gameOver=false;
    public float sideSpeed=200;
    private float horizontalInput;
    public bool isOnWall=false;
    public float wallrunForce=10;
    void Start()
    {   playerRb=GetComponent<Rigidbody>();
        Physics.gravity*=gravityModifier;       
    }

    // Update is called once per frame
    void Update()
    {   horizontalInput=Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space)&&isOnGround&&!isOnWall){
            playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            isOnGround=false;
        }
        if(Input.GetKeyDown(KeyCode.Space)&&isOnGround&&isOnWall)
        {   playerRb.AddForce(Vector3.back*jumpForce,ForceMode.Impulse);
            isOnGround=false;
        }
        if(isOnWall==true)
        {   playerRb.AddForce(orientation.forward*wallrunForce*Time.deltaTime);
            playerRb.AddForce(-orientation.right*wallrunForce*Time.deltaTime);
            playerRb.AddForce(Vector3.forward*10);
        }
        transform.Translate(Vector3.right*sideSpeed*horizontalInput*Time.deltaTime);
        if(transform.position.z<-20)
            transform.position=new Vector3(transform.position.x,transform.position.y,-20);
    }
    private void OnCollisionEnter(Collision collision)
    {   if(collision.gameObject.CompareTag("Ground")){
            isOnGround=true;
        }
        else if(collision.gameObject.CompareTag("Obstacle")){
            gameOver=true;
            Debug.Log("Game Over!");
        }
        else if(collision.gameObject.CompareTag("Bg")&&isOnGround==false){
            GetComponent<Rigidbody>().useGravity = false;
            //GetComponent<Rigidbody>().Constraints.FreezePosition.y = true;
            transform.Rotate(0, 0,-90);
            isOnWall=true;
            isOnGround=true;
        }
    }
}
