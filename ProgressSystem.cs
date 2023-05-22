using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressSystem : MonoBehaviour
{
    public static int level;
    public static float progression;
    public GameObject pauseMenuUi;
    private float fillAmount;
    public Text CurrLevel;
    public Text NextLevel;
    private float timer = 0.0f;
    private bool isDone = false;
    private float remainingFillAmount = 0f;

    [SerializeField]
    private Image bar_fill;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUi.SetActive(false);
        level = PlayerPrefs.GetInt("Level", 0);
        progression = PlayerPrefs.GetFloat("progress", 0.0f);
        bar_fill.fillAmount = fillAmount;
        Debug.Log(bar_fill.fillAmount);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Enemy.IsDead)
        {
            pauseMenuUi.SetActive(true);
            CurrLevel.text = PlayerPrefs.GetInt("Level", 0).ToString();
            NextLevel.text = (PlayerPrefs.GetInt("Level", 0) + 1).ToString();
            // calculate the total fill amount based on the score
            //Debug.Log("Progression : " + progression);
            if (!isDone)
            {
                StartCoroutine(FillProgressBar(Mathf.Clamp(Score.score / 1000f, 0f, 1f)));
            }
        }
    }

    IEnumerator FillProgressBar(float fillAmountToAdd)
    {
        float fillTime = 0.5f; // time it takes to fill the progress bar
        float elapsedTime = 0.0f; // elapsed time
        float startFillAmount = (float)(
            (
                (progression - Mathf.Clamp(Score.score / 1000f, 0f, 1f))
                - Math.Floor(progression - Mathf.Clamp(Score.score / 1000f, 0f, 1f))
            )
        ); // initial fill amount
        double run = Math.Floor(startFillAmount + fillAmountToAdd);
        Debug.Log(run);
        UpdateLevel((int)run);
        Debug.Log("fillAmountToAdd :" + fillAmountToAdd);
        Debug.Log("StartFill : " + startFillAmount);
        while (elapsedTime < fillTime)
        {
            elapsedTime += Time.deltaTime; // increment elapsed time
            float newFillAmount = Mathf.Lerp(
                startFillAmount,
                startFillAmount + fillAmountToAdd,
                elapsedTime / fillTime
            ); // calculate the new fill amount using Lerp
            bar_fill.fillAmount = newFillAmount;
            isDone = true;
            yield return null;
        } // update the fill amount
    }

    void LateUpdate() { }

    static void UpdateLevel(int add)
    {
        level += add;
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
    }

    public void Retry()
    {
        Enemy.IsDead = false;
        pauseMenuUi.SetActive(false);
        SceneManager.LoadScene("MainGame");
    }

    public void ExitMenu()
    {
        Enemy.IsDead = false;
        pauseMenuUi.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
