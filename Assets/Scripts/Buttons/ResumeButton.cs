using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    //The buttons
    private Button resumeButton;

    private GameManager gameManager;
    void Start()
    {
        resumeButton = GetComponent<Button>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        resumeButton.onClick.AddListener(ResumeSetting);
    }

    void ResumeSetting()
    {
        gameManager.UISetActive();
        gameManager.AfterResume();
        Time.timeScale = 1;
        gameObject.SetActive(false);
        gameManager.canResume = false;
        gameManager.BackButtonSound();
        gameManager.camAudio.UnPause();
    }
}
