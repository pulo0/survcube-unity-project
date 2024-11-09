using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; 
    
    void LateUpdate()
    {
       if (gameObject.CompareTag("MainCamera"))
       {
           transform.position = CameraFollowsPlayer();
       } 
    }

    Vector3 CameraFollowsPlayer()
    {
        float offsetValue = -10;
        Vector3 offset = new Vector3 (offsetValue, offsetValue, offsetValue);
        return new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + offset.z);
    }
}
