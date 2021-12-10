using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 目的:切換射擊視角與上帝視角攝影機
 */

public class CamController : MonoBehaviour
{
    public Camera gun_cam, god_cam; //兩個不同的攝影機



    /* 放在Awake內，在物件執行之前就先封鎖住，避免物件的出現，直到使用者切換攝影機後再創造該物件，若是放在Start內則會造成全部的物件都已經產生且初始化後再封鎖住。 */
    void Awake()
    {

        //預設先開啟第一部攝影機


        //一定要先暫停不使用的攝影機後，再開啟要使用的攝影機！
        god_cam.enabled = false;
        gun_cam.enabled = true;

        //沒有要顯示出來的物件則先暫時關閉，同時開啟要顯示的物件，避免背景執行浪費效能。若多部攝影機都拍著同個物件，則不需要關閉該物件，只需關閉攝影機即可

        //被關閉的物件和其子物件都會被隱藏(其身上的script都會一起被暫停)
    }

    void Update()
    {
        if (Input.GetKeyDown("2") == true)
        {
            //若是按下鍵盤的2則切換成上帝視角攝影機
            gun_cam.enabled = false;
            Time.timeScale = 0.2f;  //減慢遊戲時間
            god_cam.enabled = true;
        }
        else if (Input.GetKeyDown("1") == true)
        {
            Time.timeScale = 1f;    //恢復遊戲時間
            //若是按下鍵盤的1則切換成射擊視角攝影機
            god_cam.enabled = false;
            gun_cam.enabled = true;
        }
    }
}
