using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class Wave
// {
//     public string waveName;
// }

public class WaveSystem : MonoBehaviour
{
    // private int pointShouldbe = 10;
    // public static int waveName = 0;
    // public static float enemySpeed = 4.0f;

    // // Start is called before the first frame update
    // void Start() { }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (Score.killCount == pointShouldbe)
    //     {
    //         enemySpeed++;
    //         pointShouldbe *= 2;
    //         waveName++;
    //         Debug.Log(waveName);
    //     }
    // }

    public static int waveCount = 0;
    private int pointThreshold = 10;
    static public float enemySpeed = 4.0f;
    private int pointIncreasePerWave = 10;
    private float enemySpeedIncreasePerWave = 1.0f;

    void Update()
    {
        if (Score.killCount >= pointThreshold)
        {
            pointThreshold += pointIncreasePerWave;
            enemySpeed += enemySpeedIncreasePerWave;
            waveCount++;

            Debug.Log("Wave " + waveCount + " started!");

            // Spawn enemies here...
        }
    }
}
