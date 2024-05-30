using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public  GameObject enemyType1;
    public  GameObject enemyType2;
    public GameObject enemyType3;
    public GameObject[] enemies;
    public GameObject[] spawnPositions = new GameObject[6];

    public GameObject Player;
   // public enum lanes { First, Second, Third };
    public Vector3 spawnPosition;
    public static List<GameObject> spawnedObjects = new List<GameObject>();

    // Update is called once per frame
    public void Start()
    {
        
        spawnedObjects = new List<GameObject>();
        StartCoroutine(SpawnObject());
        spawnPosition = spawnPositions[1].transform.position;
    }

    void Update()
    {

    }

    public IEnumerator SpawnObject()
    {
        int i = 0;
        while (true)
        {


            int objToSpwn = Random.Range(0, enemies.Length);
            int spawnRate = Random.Range(1, 4);

            int spawnPoint = Random.Range(0, spawnPositions.Length);

            //Generates apropriate spawn position based on randomly selected lane and object prefab
            if (spawnPoint == 0 && objToSpwn == 0)
            {

                spawnPosition = spawnPositions[0].transform.position;
            }
            else if (spawnPoint == 1 && objToSpwn == 0)
            {
                spawnPosition = spawnPositions[1].transform.position;
            }
            else if (spawnPoint == 2 && objToSpwn == 0)
            {
                spawnPosition = spawnPositions[2].transform.position;
            }
            else if (spawnPoint == 3 && objToSpwn == 0)
            {
                spawnPosition = spawnPositions[3].transform.position;
            }

            //Checks spawn location against the list of spawned objects position and generates a new spawn position if there would be a conflict (i.e spawning on or too close to an exhisting object)
            foreach (var obj in spawnedObjects)
            {
                if (obj == null)
                {

                    int temp = Random.Range(0, 6);
                    spawnPosition = spawnPositions[temp].transform.position;
                  //  EnemyHealth newSceneObject = Instantiate(enemies[objToSpwn], spawnPosition, Quaternion.identity);
                  //  spawnedObjects.Add(newSceneObject);
                }
              

            }
            
                //Instantiates a new object at the final spawn position
                if(objToSpwn == 0)
                {
                    GameObject newObject = enemyType1;
                    newObject.GetComponent<EnemyHealth>();//lightEnemy.Instantiate(enemies[objToSpwn], spawnPosition, Quaternion.identity);
                    
                    newObject.GetComponent<EnemyHealth>().setMaxHealth(1);
                    newObject.GetComponent<EnemyHealth>().setDamage(1);
                    newObject.GetComponent<EnemyHealth>().setMoveSpeed(15);
                    Instantiate(newObject, spawnPosition, Quaternion.identity);



                    spawnedObjects.Add(newObject);
                }
                
                if (objToSpwn == 1)
                {
                    GameObject newObject = enemyType2;
                   
                    newObject.GetComponent<EnemyHealth>().setMaxHealth(2);
                    newObject.GetComponent<EnemyHealth>().setDamage(1);
                    newObject.GetComponent<EnemyHealth>().setMoveSpeed(10);
                    Instantiate(newObject, spawnPosition, Quaternion.identity);
                    spawnedObjects.Add(newObject);
                }
                
                if (objToSpwn == 2)
                {
                    GameObject newObject = enemyType3;
                    
                    
                    newObject.GetComponent<EnemyHealth>().setMaxHealth(3);
                    newObject.GetComponent<EnemyHealth>().setDamage(1);
                    newObject.GetComponent<EnemyHealth>().setMoveSpeed(7);
                    Instantiate(newObject, spawnPosition, Quaternion.identity);
                    spawnedObjects.Add(newObject);
                }

            i++;
            yield return new WaitForSeconds(spawnRate);

        }
    }
}
