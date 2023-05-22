using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text killCountTxt;
    public static int score;
    public static float survivalTime = 0f;
    public static int killCount;

    public void AddScore(int newPoint)
    {
        killCount += newPoint;
    }

    void Update()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        killCountTxt.text = "Score : " + killCount;
        survivalTime += Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
    }

    public static void CalculateScore()
    {
        score = (killCount * 10) + (int)(survivalTime * 2);
    }
    // Update is called once per frame
}
