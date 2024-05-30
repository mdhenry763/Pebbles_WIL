using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : EnemyHealth

{
    public AudioSource Enemy_Hit;
    public GameObject scoreKeeper;
    public EnemyHit(int MaxHP, int Damage, int moveSpeed) 
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = maxHealth;
      //  Enemy_Hit = GetComponent<AudioSource>();
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
             EnemySpawner.spawnedObjects.Remove(this.gameObject);

            //gameObject.GetComponent<EnemyHealth>();
            int temp = gameObject.GetComponent<EnemyHealth>().getCurrentHealth();
            if (temp == 0)
            {
                
                Debug.Log("Hir");
                Destroy(collision.gameObject);
               // scoreKeeper.GetComponent<Score>().IncreaseScore();
                Destroy(gameObject);
                
            }
            else
            {
                gameObject.GetComponent<EnemyHealth>().takeDamage(1);

                Destroy(collision.gameObject);
            }
            //SoundManager.Instance.playsound(Enemy_Hit.clip,Enemy_Hit);
            //Removes the destroyed object from the ObjectSpawners() spawnedObjects list;

        }
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
