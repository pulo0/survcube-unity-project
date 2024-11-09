using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOutOfBounds : MonoBehaviour
{
    private float verticalBound = 24.3f;
    private float verticalBoundDown = 20.25f;
    private float horizontalBound = 22.2f;

    void Update()
    {
        if(transform.position.y > verticalBound)
        {
            transform.position = VerticalBoundUp();
        } 
        else if(transform.position.y < -verticalBoundDown)
        {
           transform.position = VerticalBoundDown();
        }

        if(transform.position.x > horizontalBound)
        {
            transform.position = HorizontalBoundRight();
        }
        else if(transform.position.x < -horizontalBound)
        {
            transform.position = HorizontalBoundLeft();
        }
    }

    Vector3 VerticalBoundUp()
    {
        return new Vector3(transform.position.x, verticalBound, transform.position.z);
    }

    Vector3 VerticalBoundDown()
    {
        return new Vector3(transform.position.x, -verticalBoundDown, transform.position.z); 
    }

    Vector3 HorizontalBoundRight()
    {
        return new Vector3(horizontalBound, transform.position.y, transform.position.z);
    }

    Vector3 HorizontalBoundLeft()
    {
        return new Vector3(-horizontalBound, transform.position.y, transform.position.z);
    }
}
