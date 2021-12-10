using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * 暫停時間
 * 教學:開始遊戲按鈕
 * 播放戰鬥音樂
 * 倒數3分鐘
 * 滑鼠隱藏、鎖定
 * 暫停:滑鼠出現與解鎖、暫停戰鬥音樂、暫停按鈕、解除按鈕
 * 勝利:滑鼠出現與解鎖、停止戰鬥音樂、開啟任務完成UI、播放成功音樂、TryAgain、Exit
 * 失敗:滑鼠出現與解鎖、停止戰鬥音樂、開啟任務失敗UI、播放失敗音樂、TryAgain、Exit
 */
//倒數3分鐘

public class GameControl : MonoBehaviour
{
    //canvas
    public GameObject victory, defeated, pause;

    //music
    public AudioSource mBattle, mVictory, mDefeated;

    //count down timer
    public Text timer;
    public int sec = 0;     //input sec
    public int min = 3;     //input min
    private int seconds;    //for countdown

    public DefenseLife defense = null;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        victory.SetActive(false);
        defeated.SetActive(false);
        pause.SetActive(false);
        StartCoroutine(CountDown());
        Cursor.visible = false;//隱藏滑鼠
        Cursor.lockState = CursorLockMode.Locked;//把滑鼠鎖定到螢幕中間
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cursor.visible = true;//顯示滑鼠
            Cursor.lockState = CursorLockMode.None;//取消滑鼠鎖定
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cursor.visible = false;//隱藏滑鼠
            Cursor.lockState = CursorLockMode.Locked;//把滑鼠鎖定到螢幕中間
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseGame();
        }
        
        
    }

    private void FixedUpdate()
    {
        if (defense.CurrentHealth <= 0)
            DefeatMission();
    }

    IEnumerator CountDown()
    {
        timer.text = string.Format("{0}:{1}", min.ToString("00"), sec.ToString("00"));
        seconds = (min * 60) + sec;       //將時間換算為秒數

        while (seconds > 0)                   //如果時間尚未結束
        {
            yield return new WaitForSeconds(1); //等候一秒再次執行

            seconds--;                        //總秒數減 1
            sec--;                            //將秒數減 1

            if (sec < 0 && min > 0)         //如果秒數為 0 且分鐘大於 0
            {
                min -= 1;                     //先將分鐘減去 1
                sec = 59;                     //再將秒數設為 59
            }
            else if (sec < 0 && min == 0)   //如果秒數為 0 且分鐘大於 0
            {
                sec = 0;                      //設定秒數等於 0
            }
            timer.text = string.Format("{0}:{1}", min.ToString("00"), sec.ToString("00"));
        }

        CompleteMission();
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("3D_Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;//顯示滑鼠
            Cursor.lockState = CursorLockMode.None;//取消滑鼠鎖定
            AudioListener.pause = true;
            pause.SetActive(true);
        }
        else
        {
            AudioListener.pause = false;
            Cursor.visible = false;//隱藏滑鼠
            Cursor.lockState = CursorLockMode.Locked;//把滑鼠鎖定到螢幕中間
            Time.timeScale = 1;
            pause.SetActive(false);
        }
        //暫停倒數
    }

    public void ContinueGame()
    {
        Cursor.visible = false;//隱藏滑鼠
        Cursor.lockState = CursorLockMode.Locked;//把滑鼠鎖定到螢幕中間
        Time.timeScale = 1;
        pause.SetActive(false);
        mBattle.Play();
        //繼續倒數
    }

    private void CompleteMission()
    {
        Time.timeScale = 0;
        mBattle.Stop();
        mVictory.Play();
        Cursor.visible = true;//顯示滑鼠
        Cursor.lockState = CursorLockMode.None;//取消滑鼠鎖定
        victory.SetActive(true);
    }

    private void DefeatMission()
    {
        Time.timeScale = 0;
        mBattle.Stop();
        mDefeated.Play();
        Cursor.visible = true;//顯示滑鼠
        Cursor.lockState = CursorLockMode.None;//取消滑鼠鎖定
        defeated.SetActive(true);
    }
}
