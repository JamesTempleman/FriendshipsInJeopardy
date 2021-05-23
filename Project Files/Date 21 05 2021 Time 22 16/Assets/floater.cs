using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floater : MonoBehaviour
{
    //x and y pos
    float xPos;
    float zPos;

    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
        zPos = transform.position.z;
    }

//speed
float speed = 2f;
//movement height
float height = 0.1f;
 
void Update() {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(xPos, (newY* height)+2, zPos) ;
}

}