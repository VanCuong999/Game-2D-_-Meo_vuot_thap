using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float patrolRadius = 5f;
    public float speed = 2f;
    public float pauseTime = 2f;

    [Header("Combat Settings")]
    public float detectionRadius = 5f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public LayerMask playerLayer;
    public Transform attackPoint;
    //[SerializeField] private float damageMin = 5f;
    // [SerializeField] private float damageMax = 15f;
    private float attackTimer;
    private bool isPlayerDetected = false;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool isPatrolling = true;
    private bool facingRight = true;
    private Transform player;

    public NPC npc;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement; // Thêm vector di chuyển

    [Header("Respawn Settings")]
    public float respawnTime = 3f;
    private bool isDead = false;
    private bool isRespawning = false;

    [Header("Heath Enemy")]
    [SerializeField] private Image _heathBarFill;
    [SerializeField] private Transform _healthBarTranForm;
    public float HeathEnemy = 100;
    private float currentHeath;
    private Camera _camera;
    private Color fullHealthColor = Color.green;
    private Color lowHealthColor = Color.yellow;
    private Color menimumHealthColor = Color.red;


    private void Start()
    {
        currentHeath = HeathEnemy;
        startPosition = transform.position;
        ChooseRandomDirection();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {

        _camera = Camera.main;
    }
    private void Update()
    {
        if (isRespawning || isDead) return;

        movement = Vector2.zero; 

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(player.position, transform.position);
            isPlayerDetected = distanceToPlayer <= detectionRadius;

            if (isPlayerDetected && distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else if (isPlayerDetected)
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
            }
        }

        attackTimer -= Time.deltaTime;

        // Set animation parameters
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
        anim.SetBool("moVing", movement != Vector2.zero);
    }

    private void Patrol()
    {
        if (isPatrolling)
        {
            Vector2 direction = targetPosition - (Vector2)transform.position;
            movement = direction.normalized;
            MoveEnemy(movement);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                isPatrolling = false;
                StartCoroutine(PauseBeforeMoving());
            }
        }
    }

    private IEnumerator PauseBeforeMoving()
    {
        yield return new WaitForSeconds(pauseTime);
        ChooseRandomDirection();
        isPatrolling = true;
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        movement = direction;
        MoveEnemy(movement);
    }

    private void AttackPlayer()
    {
        if (attackTimer <= 0)
        {
            Vector2 attackDirection = (player.position - transform.position).normalized;
            anim.SetFloat("attackX", attackDirection.x);
            anim.SetFloat("attackY", attackDirection.y);

            anim.SetTrigger("Attack"); 

            attackTimer = attackCooldown; 
        }
    }

    public void OnAttackComplete()
    {
        FacePlayer(); 
    }



    private void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // Cập nhật Animator
        anim.SetFloat("moveX", Mathf.Abs(direction.x) > 0 ? Mathf.Sign(direction.x) : 0);
        anim.SetFloat("moveY", direction.y);
    }



    private void ChooseRandomDirection()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 randomDirection = directions[Random.Range(0, directions.Length)];
        targetPosition = startPosition + randomDirection * patrolRadius * 0.8f;
        movement = randomDirection;
    }
    private void FacePlayer()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            anim.SetFloat("moveX", Mathf.Round(directionToPlayer.x));
            anim.SetFloat("moveY", Mathf.Round(directionToPlayer.y));
        }
    }


    public void UpdateHealth(float health)
    {
        HeathEnemy = health;
        _heathBarFill.fillAmount = currentHeath / HeathEnemy;
        UpdateHealthColor();
    }

    public virtual void UpdateHealthColor()
    {
        float healthPercentage = currentHeath / HeathEnemy;

        if (healthPercentage <= 0.3f) // Dưới 30%
        {
            _heathBarFill.color = menimumHealthColor;
        }
        else if (healthPercentage <= 0.7f) // Dưới 70%
        {
            _heathBarFill.color = lowHealthColor;
        }
        else // Trên 70%
        {
            _heathBarFill.color = fullHealthColor;
        }
    }

    public void TakeDangage(float damage)
    {
        if (gameObject != null)
        {
            //entityFX.StartCoroutine("FlashFX");
        }
        currentHeath -= damage;

        if (currentHeath <= 0)
        {
            Die();
            //Exp.Intance.TakeExp(Exp.Intance.exp);
            //expPlayer.Intancs.GainExperience(20);
        }
        UpdateHealth(HeathEnemy);
    }
    private void Die()
    {
        if (isDead) return;
        isDead = true;
      
           
            if (npc != null)
            {
                npc.UpdateKills(1); 
                Debug.Log("1");
            }
        
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;        
        if (_healthBarTranForm != null)
        {
            _healthBarTranForm.gameObject.SetActive(false); 
        }
        
        StartCoroutine(Respawn());
    }


    private IEnumerator Respawn()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnTime);
        isDead = false;
        currentHeath = HeathEnemy;
        UpdateHealth(HeathEnemy);
        transform.position = startPosition;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
        if (_healthBarTranForm != null)
        {
            _healthBarTranForm.gameObject.SetActive(true); 
        }
        yield return new WaitForSeconds(1f);
        isRespawning = false; 
    }

}
