using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerRb;
    private Animator playerAnim;
    public Transform orientation;
    private float jumpForce=40;
    private float gravityModifier=12;
    public bool isOnGround=true;
    public bool gameOver=false;
    public float sideSpeed=200;
    private float horizontalInput;
    public bool isOnWall=false;
    public float wallrunForce=10;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public Button restartButton;
    public TextMeshProUGUI gameText;
    public Button button;
    public bool isGameActive=false;
    private int score=0;
    public ParticleSystem coin;
    public ParticleSystem bomb;
    void Start()
    {   playerRb=GetComponent<Rigidbody>();
        playerAnim=GetComponent<Animator>();
        Physics.gravity*=gravityModifier;       
    }

    // Update is called once per frame
    void Update()
    {   scoreText.text="Score: "+score;
        if(isGameActive)
        {
        horizontalInput=Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space)&&isOnGround&&!isOnWall){
            playerAnim.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            isOnGround=false;
        }
        if(Input.GetKeyDown(KeyCode.Space)&&isOnGround&&isOnWall)
        {   playerAnim.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.back*jumpForce,ForceMode.Impulse);
            isOnGround=false;
        }
        if(isOnWall==true)
        {   playerRb.AddForce(orientation.forward*wallrunForce*Time.deltaTime);
            playerRb.AddForce(-orientation.right*wallrunForce*Time.deltaTime);
            playerRb.AddForce(Vector3.forward*23);
        }
        transform.Translate(Vector3.right*sideSpeed*horizontalInput*Time.deltaTime);
        if(transform.position.z<-20)
            transform.position=new Vector3(transform.position.x,transform.position.y,-20);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {   if(collision.gameObject.CompareTag("Ground")){
            isOnGround=true;
            GetComponent<Rigidbody>().useGravity = true;
            if(isOnWall==true)
                transform.Rotate(0, 0,90);
            isOnWall=false;
            isOnGround=true;
        }
        else if(collision.gameObject.CompareTag("Obstacle")||collision.gameObject.CompareTag("Wall")||collision.gameObject.CompareTag("Bomb")){
            if(collision.gameObject.CompareTag("Bomb")){
                //Destroy(gameObject);
                Destroy(collision.gameObject);
                bomb.Play();
            }
            isGameActive=false;
            gameOver=true;
            Debug.Log(score);
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
        else if(collision.gameObject.CompareTag("Bg")&&isOnGround==false){
            GetComponent<Rigidbody>().useGravity = false;
            //GetComponent<Rigidbody>().Constraints.FreezePosition.y = true;
            if(isOnWall==false)
                transform.Rotate(0, 0,-90);
            isOnWall=true;
            isOnGround=true;
        }
        else if(collision.gameObject.CompareTag("Money")){
            Destroy(collision.gameObject);
            coin.Play();
            score++;
        }
    }
    public void RestartGame(){
        Physics.gravity/=gravityModifier;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void startGame()
    {   gameText.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        isGameActive=true;
    }
}
