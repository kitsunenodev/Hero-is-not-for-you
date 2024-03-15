using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private PlayerStats _playerStats;
    
    public Rigidbody2D rigidBody;
    
    public Animator anim;
    
    public bool isFlipped;
    
    public StateEnum state;

    public bool isDetected;

    public bool animPlaying = true;

    public bool isJumping;

    private Vector3 _velocityRef = Vector3.zero;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.isRunning)
        {
           Move();
           Jump();
           FlipSprite();
           CheckMoves(); 
           StateUpdate();
           ResumeAnimation();
           
        }
        else
        {
            StopAnimation();
        }
    }

    //Function To move the player
    void Move()
    {
        if (GameController.Instance.isRunning)
        {
            Vector3 targetVelocity = new Vector3(0,rigidBody.velocity.y);
            if (Input.GetKey(KeyCode.Q))
            {
                targetVelocity.x -= _playerStats.walkSpeed.x;
            }

            if (Input.GetKey(KeyCode.D))
            {
                targetVelocity.x += _playerStats.walkSpeed.x;
                
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (_playerStats.nbJump > 0)
                {
                    _playerStats.nbJump--;
                    isJumping = true;
                }
            }

            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity,
                ref _velocityRef, 0.01f);
            ChangeAnim(targetVelocity);
        }
    }

    void ChangeAnim(Vector3 direction)
    {
        anim.SetBool("isWalking", direction.x != 0 );
    }
    void Jump()
    {
        if (isJumping)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(_playerStats.jumpSpeed);
            isJumping = false;
        }
    }

    void CheckMoves()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("isAttacking");
        }

        if (Input.GetMouseButtonDown(1))
        {
            Dodge();
        }
        
        foreach (var move in _playerStats.moveList)
        {
            {
                if (Input.GetKeyDown(move.attackKey))
                {
                    anim.SetTrigger(move.triggerName);
                }
            }
        }
    }
   

    void FlipSprite()
    {
        isFlipped = Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x;
        CheckFlipped();
    }

    void Dodge()
    {
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetTrigger(transform.localScale.x < 0 ? "isDodgingb" : "isDodgingf");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetTrigger(transform.localScale.x < 0 ? "isDodgingf" : "isDodgingb");
        }
        
    }

    void StateUpdate()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "playerAttack")
        {
            state = StateEnum.Attacking;
        }
        else
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "playerDodgeBackward" || 
                anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "playerDogeForward")
            {
                state = StateEnum.Dodging;
            }
            else
            {
                state = StateEnum.Idle;
            }
            
        }
    }

    void CheckFlipped()
    {
        var localScale = transform.localScale;
        if (isFlipped && localScale.x > 0 || !isFlipped && localScale.x < 0)
        {
            
            localScale = new Vector3(-localScale.x,
                localScale.y,
                localScale.z);
            transform.localScale = localScale;
        }
        
    }

    public void HitMonster(DemonController demon)
    {
        GameController.Instance.PlayerHitDemon(demon, this);
    }

    void StopAnimation()
    {
        if (animPlaying)
        {
            animPlaying = false;
            anim.enabled = false;
        }
    }

    void ResumeAnimation()
    {
        if (!animPlaying)
        {
            animPlaying = true;
            anim.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foundation"))
        {
            _playerStats.ResetJumps();
        }
    }
}
