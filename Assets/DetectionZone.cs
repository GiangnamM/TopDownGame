using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    public Collider2D col;

    // Detect when  object enters range
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            detectedObjs.Add(collider);
        }
    }

    // Detect when object leaves range

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            detectedObjs.Remove(collider);
        }
    }


}
