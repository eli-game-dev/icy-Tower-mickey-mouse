using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Transform target;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, target.position.y, -10);//move only in y position
    }

}
