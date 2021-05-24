using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class someoneThere : MonoBehaviour
{
   
    public bool SomeonesHere;

    private void OnTriggerEnter(Collider other)
    {
        SomeonesHere = true;
        Debug.Log("Player Entered Game");
    }
}
