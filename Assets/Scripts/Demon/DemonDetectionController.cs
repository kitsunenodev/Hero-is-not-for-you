using System;
using UnityEngine;

public class DemonDetectionController : MonoBehaviour
{
    public DemonController parent;

    private void Awake()
    {
        parent = transform.parent.GetComponent<DemonController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (parent.hasBeenHit)
            {
                parent.hasBeenHit = false;
            }
            parent.anim.SetBool("isTriggered", true);
            parent.target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            parent.anim.SetBool("isTriggered", false);
            if (Vector2.Distance(parent.transform.position, parent.target.transform.position) > parent.detectionDistance)
            {
                parent.target = null;
            }
        }
    }
}
