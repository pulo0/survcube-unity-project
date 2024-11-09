using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button moreButton;
    private Image buttonImage;
    private float timeToChange = 0.3f;
    private float timeSinceChange;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        moreButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }

    void Start() 
    {
        moreButton.onClick.AddListener(GoToLink);
    }

    void Update()
    {
        timeSinceChange += Time.deltaTime;

        if(timeSinceChange >= timeToChange)
        {
            Color newColor = new Color(Random.value, Random.value, Random.value);

            buttonImage.color = newColor;
            timeSinceChange = 0f;
        }

    }

    void GoToLink()
    {
        gameManager.BackButtonSound();

        Application.OpenURL("https://bartexwhy.itch.io");
    }
}
