using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    public float scrollSpeed;
    public Vector3 startPos;
    public Vector3 endPos;
    public Text scrollingText;
    Vector3 adjustedPos;
    
    // Start is called before the first frame update
    void Start()
    {
        adjustedPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        //moves text to adjusted Position
        scrollingText.gameObject.transform.position = new Vector3(scrollingText.gameObject.transform.position.x, adjustedPos.y, scrollingText.gameObject.transform.position.z);

        //if not at the end position move otherwise put it back to the start
        if(adjustedPos.y >= endPos.y)
        {
            adjustedPos = startPos;
        }
        else
        {
            adjustedPos.y += scrollSpeed;
            Debug.Log(adjustedPos.y);
        }
    }

}
