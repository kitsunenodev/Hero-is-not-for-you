using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionMother : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyHurtBox"))
        {
            other.gameObject.GetComponent<DemonController>().isDetected = true;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().isDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyHurtBox"))
        {
            other.gameObject.GetComponent<DemonController>().isDetected = false;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().isDetected = false;
        }
        
    }
}
