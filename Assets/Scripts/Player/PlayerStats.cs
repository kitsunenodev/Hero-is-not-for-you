using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public int strength = 10;
    
    public int maxHealth = 10;

    public int currentHealth = 10;
    public int nbJump;

    private readonly int _totalJump = 1;
    
    public List<Move> moveList;
    
    public Vector3 jumpSpeed = new Vector3(0,10);
    
    public Vector3 walkSpeed = new Vector3(20.0f,0);
    
    public void TakeDamage(int damage)
    {
        if (damage > currentHealth)
        {
            GameController.Instance.GameOver();
        }
        else
        {
            currentHealth -= damage;
            GameController.Instance.uiManager.UpdateHealthBar();
        }
    }

    public void ResetJumps()
    {
        nbJump = _totalJump;
    }
}
