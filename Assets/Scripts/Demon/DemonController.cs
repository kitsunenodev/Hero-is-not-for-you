using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DemonController : MonoBehaviour
{
    public int maxLife;

    private int _currentLife;

    public Transform target;

    public int maxWalkSpeed;

    public float walkSpeed;

    public float triggerSpeed;

    public int damage;

    public Animator anim;

    public bool walkRight = true;

    public bool triggered;

    public bool hasBeenHit = true;

    public bool isDetected;

    public bool animPlaying = true;

    private Rigidbody2D _rigidBody;
    
    private Vector3 _refVelocity = Vector3.zero;

    private Vector3 _localScale;

    public float attackDistance = 2f;

    public float detectionDistance = 10f;

    public Collider2D hurtBox;

    private Canvas _canvasUI;

    private Vector3 _canvasScale;

    private TextMeshProUGUI _healthText;

    private Slider _healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        _localScale = transform.localScale;
        walkRight = true;
        anim = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        walkSpeed = maxWalkSpeed;
        _canvasUI = GetComponentInChildren<Canvas>();
        _canvasScale = _canvasUI.transform.localScale;
        _healthSlider = _canvasUI.GetComponentInChildren<Slider>();
        _healthText = _canvasUI.GetComponentInChildren<TextMeshProUGUI>();
        _currentLife = maxLife;
        _healthSlider.value = _healthSlider.maxValue = maxLife;
        _healthText.text = $"{_currentLife}/{maxLife}";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.isRunning)
        {
            ResumeAnimation();
            MoveBehavior();
            triggered = anim.GetBool("isTriggered");
        }
        else
        {
            StopAnimation();
        }
        
    }

    void ChangeDirection()
    {
        walkRight = !walkRight;
        _localScale = new Vector3(-_localScale.x, _localScale.y, _localScale.z);
        transform.localScale = _localScale;
        _canvasScale = new Vector3(-_canvasScale.x, _canvasScale.y, _canvasScale.z);
        _canvasUI.transform.localScale = _canvasScale;
    }

    void Move()
    {
        Vector3 direction = Vector3.zero;
        direction.x = walkRight ? walkSpeed : -walkSpeed;

        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, direction, ref _refVelocity,
            0.01f);
    }

    void MoveBehavior()
    {
        if (target != null)
        {
            if (_localScale.x < 0 && target.position.x > transform.position.x ||
                _localScale.x > 0 && target.position.x < transform.position.x)
            {
                ChangeDirection();
            }

            if (Vector2.Distance(transform.position, target.position) < attackDistance)
            {
                anim.SetBool("isAttacking", true);
                _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity,Vector3.zero,
                    ref _refVelocity, 0.1f);
            }
            else
            {
                anim.SetBool("isAttacking", false);
                Move();
            }
        }
        else
        {
            Move();
        }
    }

    public void Hit(PlayerController player)
    {
        GameController.Instance.DemonHitPlayer(this,player);
    }

    public void TakeDamage(int damageAmount, PlayerController player)
    {
        if (damageAmount >= maxLife)
        {
            GameController.Instance.KilledMonster();
            Destroy(gameObject);
        }
        else
        {
             hasBeenHit = true;
             _currentLife -= damage;
             UpdateHealth();
             target = player.transform;
             StartCoroutine(RecoveryTime());
        }
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

    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            ChangeDirection();
        }
    }

    private IEnumerator RecoveryTime()
    {
        hurtBox.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        hurtBox.gameObject.SetActive(false);
    }

    void UpdateHealth()
    {
        _healthSlider.value = _currentLife;
        _healthText.text = $"{_currentLife}/{maxLife}";
    }
}
