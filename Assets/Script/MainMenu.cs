using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject storyPage = null;
    public GameObject tutorial = null;
    private void Awake()
    {
        storyPage.SetActive(false);
        tutorial.SetActive(false);
    }

    public void OpenTutorial()
    {
        tutorial.SetActive(true);
    }

    public void StartMission()
    {
        SceneManager.LoadScene("3D_Scene");
    }

    public void StoryData()
    {
        storyPage.SetActive(true);
    }

    public void escStoryData()
    {
        storyPage.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
