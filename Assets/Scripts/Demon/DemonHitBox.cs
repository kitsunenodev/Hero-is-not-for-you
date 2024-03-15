using System;
using UnityEngine;

public class DemonHitBox : MonoBehaviour
{
    public DemonController parent;

    private void Awake()
    {
        parent = transform.parent.GetComponent<DemonController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player") && parent.anim.GetBool("isAttacking"))
        {
            Debug.Log("Hit");
            parent.Hit(other.GetComponent<PlayerController>());
        }
    }
}
