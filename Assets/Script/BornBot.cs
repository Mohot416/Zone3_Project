using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//怪物生成程式碼
/*
 *按固定時間不停地生成敵人
 *每條路每次生成時間有50%機率會生成敵人
 *確定會生成敵人後再依照各種敵人的正規化後的機率隨機決定會生成哪種敵人
 */

[System.Serializable]
public class Enemy
{
    public int enemyID;                 //ID
    public GameObject body;             //敵人本體
    public float appearPercentage;     //出現機率
    public Enemy(int id, GameObject body, float x)
    {
        this.enemyID = id;
        this.body = body;
        this.appearPercentage = x;
    }
}

public class BornBot : MonoBehaviour {
    
    //重生點
    public GameObject LRespawnPoint;    //左
    public GameObject MRespawnPoint;    //中
    public GameObject RRespawnPoint;    //右
    //敵人總數量
    public int enemyTotalNum = 10;
    //升成敵人的間隔
    public float intervalTime = 3;
    //生成的敵人
    public Enemy normalEnemy;

    private int possibilityNum = 0; 

    // Use this for initialization
    void Start () {
        //重複生成敵人
        InvokeRepeating("CreatEnemy", 3.0F, intervalTime);
	}
    //方法，生成敵人
    private void CreatEnemy()
    {
        possibilityNum = Random.Range(1, 100);
        if(possibilityNum <= 50)
        {
            Instantiate(normalEnemy.body, MRespawnPoint.transform.position, Quaternion.identity);
        }
        possibilityNum = Random.Range(1, 100);
        if (possibilityNum <= 50)
        {
            Instantiate(normalEnemy.body, LRespawnPoint.transform.position, Quaternion.identity);
        }
        possibilityNum = Random.Range(1, 100);
        if (possibilityNum <= 50)
        {
            Instantiate(normalEnemy.body, RRespawnPoint.transform.position, Quaternion.identity);
        }
    }
}
