using System;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public PlayerController parent;

    private bool _hasAlreadyHit;
    // Start is called before the first frame update
    private void Start()
    {
        parent = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyHurtBox"))
        {
            if (!_hasAlreadyHit)
            {
                _hasAlreadyHit = true;
                parent.HitMonster(other.gameObject.GetComponent<DemonController>());
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _hasAlreadyHit = false;
    }
}
