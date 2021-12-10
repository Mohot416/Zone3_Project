using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//小隊員攻擊
public class Bot : MonoBehaviour
{
    // Start is called before the first frame update
    public float atk_rate=0.3F;

    void Start()
    {
        InvokeRepeating("Fire", 1F, atk_rate);
    }

    public GameObject Bullet;
    public GameObject Star;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) //create airstrike
        {
            //Instantiate(Star,new Vector3(transform.position.x,transform.position.y+50,(transform.position.z-30)),Quaternion.Euler (90f, 0f, 0f));
        }
    }
    private void Fire() //create bullet
    {
        //Instantiate(Bullet,new Vector3(transform.position.x,transform.position.y+2,(transform.position.z-1)),Quaternion.Euler (0f, 90f, 0f));
    }
}
