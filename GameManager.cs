using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Characters
{
    public bool isUnlocked;
    public GameObject player;
    public int minLevel;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // [SerializeField]
    // private GameObject[] characters;

    [SerializeField]
    private Characters[] character;

    private int _charIndex = 0;

    private int _selectiveind = 0;

    public int ShowLevel
    {
        get { return character[_selectiveind].minLevel; }
    }
    public Characters[] players
    {
        get { return character; }
    }
    public bool IsUnlocked
    {
        get { return character[_selectiveind].isUnlocked; }
    }
    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }

    public int SelectiveInd
    {
        get { return _selectiveind; }
        set { _selectiveind = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        foreach (var element in character)
        {
            if (ProgressSystem.level >= element.minLevel)
            {
                element.isUnlocked = true;
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            FindObjectOfType<AudioManager>().Stop("MenuTheme");
            FindObjectOfType<AudioManager>().Play("Theme");
            Debug.Log(character[0].player.gameObject.name);
            Instantiate(character[_charIndex].player, Vector3.zero, Quaternion.identity);
        }
    }
}
