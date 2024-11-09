using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button backButton;

    private GameManager gameManager;

    void Start()
    {
        backButton = GetComponent<Button>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        backButton.onClick.AddListener(BackSetting);
    }

    void BackSetting()
    {
        gameManager.ChangeToTitleScreen();
        gameManager.BackButtonSound();
    }
}
