using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    //UI variables
    private Button button;

    //Scripts variables
    private GameManager gameManager;
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        button.onClick.AddListener(PlaySetting);
    }
    
    public void PlaySetting()
    {
        gameManager.StartGame();
        gameManager.ButtonSound();
    }
}
