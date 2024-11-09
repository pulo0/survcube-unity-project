using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    
    public string screenshotName = "screenshot.png";
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ScreenCapture.CaptureScreenshot(screenshotName);
        }
    }
}
