using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Note:
This script is designed for placing mines purpose.
The script need to be attached to Huster.
*/

public class Huster : MonoBehaviour
{

    public Camera topDownCam = null;
    public float skillCD = 20.0f;  //技能冷卻時間長短
    
    //private class members
    [SerializeField] private GameObject mine = null;     //地雷物件
    private float skillCDTimer = 0;     //計算技能冷卻時間
    private Vector3 targetPosition;

    private void Awake()
    {
        skillCDTimer = -skillCD;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))      //按4
        {
            placingMine();
        }
    }

    private void placingMine()
    {
        targetPosition = new Vector3(gameObject.transform.position.x + 1, 0.1f, gameObject.transform.position.z - 12);
        if (Time.time > skillCD + skillCDTimer)    //可以使用技能時
        {
            skillCDTimer = Time.time;    //設置timer，值為現在時間
            Instantiate(mine, targetPosition, Quaternion.identity);     //於角色前的指定距離生成地雷
            Debug.Log("Mine placed");
        }
        else     //技能冷卻中
        {
            Debug.Log("Mine Skill is cooling down");
        }
    }
}
