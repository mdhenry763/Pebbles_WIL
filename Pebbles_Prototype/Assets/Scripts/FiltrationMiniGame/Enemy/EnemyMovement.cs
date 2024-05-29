using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _RigBodyComp;
    public AudioSource GnomeSound;
    // Start is called before the first frame update
    void Start()
    {
        _RigBodyComp = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(enemyMovement());
    }
    IEnumerator enemyMovement()
    {
        _RigBodyComp.velocity = new Vector3(_RigBodyComp.velocity.x, _RigBodyComp.velocity.y, -10);

        if (!GnomeSound.isPlaying)
        {
            StartCoroutine(enemysound());
        }

        yield return null;      

    }


    IEnumerator enemysound()
    {

       
        //SoundManager.Instance.playsound(GnomeSound.clip, GnomeSound);
        
        yield return null;
        
    }
}
