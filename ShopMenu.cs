using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ShopMenu : MonoBehaviour
{
    [SerializeField]
    private Text LevelUi;

    [SerializeField]
    private Text[] UnlockedOrNot;
    private GameManager temp;

    // Start is called before the first frame update
    void Start()
    {
        temp = FindObjectOfType<GameManager>();
    }

    public void Select()
    {
        //FindObjectOfType<AudioManager>().Stop("MenuTheme");
        //FindObjectOfType<AudioManager>().Play("Theme");
        int clickObj = int.Parse(
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name
        );
        Debug.Log(clickObj);
        //GameManager.instance.CharIndex = clickObj;
        Debug.Log(temp.ShowLevel);
        Debug.Log(ProgressSystem.level);
        GameManager.instance.SelectiveInd = clickObj;
        if (temp.IsUnlocked)
        {
            Debug.Log("Test");
            GameManager.instance.CharIndex = clickObj;
        }
        Debug.Log("Character :" + GameManager.instance.CharIndex);
    }

    void Update()
    {
        LevelUi.text = "Level : " + ProgressSystem.level;
        foreach (var ele in temp.players)
        {
            int index = Array.IndexOf(temp.players, ele);
            if (ele.isUnlocked)
            {
                UnlockedOrNot[index].gameObject.SetActive(false);
            }
        }
    }

    public void PlayGame()
    {
        //FindObjectOfType<AudioManager>().Stop("MenuTheme");
        //FindObjectOfType<AudioManager>().Play("Theme");
        SceneManager.LoadScene("MainGame");
    }
}
