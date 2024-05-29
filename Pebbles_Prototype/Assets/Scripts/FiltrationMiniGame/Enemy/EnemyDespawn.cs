using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Delete" /*|| other.tag == "Bullet"*/)
        {
           // EnemySpawner.spawnedObjects.Remove(this.gameObject);
            Destroy(this.gameObject);

            //Removes the destroyed object from the ObjectSpawners() spawnedObjects list;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
