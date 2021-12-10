using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
本程式是為了拖曳隊員，並限制其能被拖曳的位置
(三個隊員預設需在中間)
*/

public class DragItem : MonoBehaviour
{
    //public members
    public float right_wall_x = 0;
    public float left_wall_x = 0;


    //private members
    private float offset_x = 0;
    private float originalX;
    private float originalY;
    private Vector3 dist;
    private float posX;
    private float posY;

    private void Start()
    {
        originalX = transform.position.x;   //獲取物件x座標
        offset_x = (left_wall_x - right_wall_x);    //獲取兩牆距離
    }

    void OnMouseDown()                                              //滑鼠點擊時
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);  //物體中心點座標轉為螢幕空間座標
        posX = Input.mousePosition.x - dist.x;
    }
    void OnMouseDrag()                                              //拖曳小隊員
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, dist.y, dist.z);   //滑鼠拖曳實時更改座標
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);      //螢幕座標轉回世界座標

        /*偏移導正*/
        if (worldPos.x > right_wall_x)     //left
        {
            worldPos.x = originalX + offset_x;
        }
        else if (worldPos.x < left_wall_x)        //right
        {
            worldPos.x = originalX - offset_x;
        }
        else                       //middle
        {
            worldPos.x = originalX;
        }
        transform.position = worldPos;      //物體根據worldPos進行座標改變
    }
}
