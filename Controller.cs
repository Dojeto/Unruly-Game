using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Controller : MonoBehaviour
{
    void Awake()
    {
        FindObjectOfType<SAudioManager>().Play("MenuTheme");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
        FindObjectOfType<SAudioManager>().Play("Theme");
        FindObjectOfType<SAudioManager>().Stop("MenuTheme");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
