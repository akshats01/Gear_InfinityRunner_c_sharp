using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obstaclePrefab;
    public GameObject obstaclePrefab2;
    public GameObject moneyPrefab;
    public GameObject bombPrefab;
    private float startDelay=2;
    private float repeatRate=2;
    private Vector3 spawnPos=new Vector3(25,0,-10);
    private Vector3 spawnPos2;
    public int counter=0;
    private int ground;
    private PlayerController playerControllerScript;
    void Start()
    {   ground=0;
        InvokeRepeating("SpawnObstacle",startDelay,repeatRate);
        playerControllerScript=GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
    void SpawnObstacle()
    {   if(playerControllerScript.isGameActive){
        int choice=Random.Range(0,5);
        if(playerControllerScript.gameOver==false&&ground==0)
        {   spawnPos=new Vector3(190,0,-Random.Range(0,3)*10);
            Instantiate(obstaclePrefab,spawnPos,obstaclePrefab.transform.rotation);
            if(choice!=0)
            {
                spawnPos+=new Vector3(0,5,0);
                Instantiate(moneyPrefab,spawnPos,moneyPrefab.transform.rotation);
            }
            else
            {   spawnPos+=new Vector3(0,7,0);
                Instantiate(bombPrefab,spawnPos,bombPrefab.transform.rotation);
            }
        }
        else if(playerControllerScript.gameOver==false&&ground==2)
        {   spawnPos=new Vector3(190,Random.Range(0,3)*10+4,4);
            Instantiate(obstaclePrefab,spawnPos,obstaclePrefab.transform.rotation*  Quaternion.Euler(-90, 0, 0));
            if(choice!=0)
            {
                spawnPos+=new Vector3(0,0,-8);
                Instantiate(moneyPrefab,spawnPos,moneyPrefab.transform.rotation*  Quaternion.Euler(0, 0, -90));
            }
            else{
                spawnPos+=new Vector3(0,0,-8);
                Instantiate(bombPrefab,spawnPos,bombPrefab.transform.rotation*  Quaternion.Euler(0, 0, 0));
            }
            
        }
        else if(playerControllerScript.gameOver==false&&ground==3)
        {   spawnPos2=new Vector3(190,0,-Random.Range(0,2)*16-3);
            Instantiate(obstaclePrefab2,spawnPos2,obstaclePrefab2.transform.rotation);
        }
        else if(playerControllerScript.gameOver==false&&ground==1)
        {   spawnPos2=new Vector3(190,Random.Range(0,2)*16+7,4);
            Instantiate(obstaclePrefab2,spawnPos2,obstaclePrefab2.transform.rotation*Quaternion.Euler(-90, 0, 0));
        }
        counter++;
        if(counter>=5)
        {   counter=0;
            if(ground==3)
                ground=0;    
            else
                ground++;
        }
        }
    }
}
