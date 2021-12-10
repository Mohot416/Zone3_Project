using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//空投程式碼
public class AirstrikeSetting : MonoBehaviour
{
    //空投速度
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter( Collider other)
    {
            Destroy(gameObject);            
    }
    // Update is called once per frame
    void Update()
    {
        //垂直向下(X,Y,Z)
        transform.Translate(0,speed*Time.deltaTime,0);

        //五秒後毀掉
        Destroy(gameObject,5);
    }
}
