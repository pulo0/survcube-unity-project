using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsButton : MonoBehaviour
{
    private Button creditsButton;

    private GameManager gameManager;

    void Start()
    {
        creditsButton = GetComponent<Button>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        creditsButton.onClick.AddListener(CreditsSetting);
    }

    void CreditsSetting()
    {
        gameManager.ChangeToCredits();
        gameManager.ButtonSound();
    }
}
