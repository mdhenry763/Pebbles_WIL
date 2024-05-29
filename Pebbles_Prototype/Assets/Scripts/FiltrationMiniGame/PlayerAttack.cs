using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Aim;
    public GameObject Gun;
    public GameObject CarootShooter;
    public GameObject CarootIdle;
    public AudioSource Shoot_Sound;
    Vector3 mousePos;
    Vector2 treeaim;
    Transform treeRotate;
    List<GameObject> magazine = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
       // Shoot_Sound = GetComponent<AudioSource>();
       // treeRotate = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //MouseTracker();

        StartCoroutine(RangerRecharge());
        
    }
    IEnumerator RangerRecharge()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (CarootIdle.active == true)
            {

                GameObject bullet = Instantiate(CarootShooter, CarootIdle.transform.position, CarootIdle.transform.rotation);
                magazine.Add(bullet);
               // SoundManager.Instance.playsound(Shoot_Sound.clip, Shoot_Sound) ;

            }
        }
        int counter = magazine.Count;

        if(magazine.Count < counter)
        {

            for (int i = 0; i < magazine.Count; i++)
            {
                Destroy(magazine[i]);

            }
        }



        yield return null;
    }
 
   /* public void MouseTracker()
    {
       
        mousePos = Input.mousePosition;
        Aim.transform.position = mousePos;

        treeaim.y += Input.GetAxis("Mouse Y") * 16 ;
   

        if(transform.localEulerAngles.y-360 <= (-125) && transform.localEulerAngles.y - 360 >= (170) )
        {
            transform.localRotation = Quaternion.Euler(0, (-treeaim.y + 171) , 0);

        }

        
        if( transform.localEulerAngles.y - 360 < (-260))
        {
                transform.localRotation = Quaternion.Euler(0, -259, 0);
        }
        else if(transform.localEulerAngles.y - 360 > (-125))
        {
                transform.localRotation = Quaternion.Euler(0, -125, 0);

        }
        

    }*/
}
