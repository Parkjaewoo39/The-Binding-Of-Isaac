using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapCam : MonoBehaviour
{
    public Transform player;

    private void Awake()
    {
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
    
    private void LateUpdate()
    {
        //Vector3 newPosition = player.position;
        //newPosition.z = transform.position.z-110f;
       

        transform.rotation = Quaternion.Euler(0f, 0f, player.eulerAngles.z);

    }
   
}
