using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEnemy : MonoBehaviour
{
    public static GolemEnemy Intance;
    public float HeathGolem;
    public float fasingDir = 1;
    private bool FashingRight = true;
    [SerializeField] private float Speed;
    [SerializeField] private float lineOfSize;
    [SerializeField] private float shootingRange;
    private Animator anim;
    private Transform player;
    private float timer;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    public EntityFX entityFX;

    private void Awake() 
    {
        Intance = this;    
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        if (player == null) return;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        timer += Time.deltaTime;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSize && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
            FacePlayer();
            anim.SetBool("wall",true);
        }
        else if (distanceFromPlayer <= shootingRange && timer > 2)
        {
            timer = 0;
            Debug.Log("da cham player");
            if (player == null) return;
            anim.SetTrigger("attack");
        }else
        {
            anim.SetBool("wall",false);
        }
        
    }
    public void EnemyAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,playerLayer);

        foreach (Collider2D playerr in hitPlayer)
        {
            Debug.Log("We hit " + player);
            HeathPlayer.Intance .TakeHeath(20);

        }
    }

    public void TakeDangage(float damgae)
    {
        if (gameObject != null)
        {
            entityFX.StartCoroutine("FlashFX");
        }
        HeathGolem -= damgae;

        if(HeathGolem <= 0)
        {
            Destroy(gameObject);
            Exp.Intance.TakeExp(Exp.Intance.exp);
            expPlayer.Intancs.GainExperience(20);
        }
        UpdateHeath(HeathGolem);
    }


    private void FacePlayer()
    {
        // Xác định hướng mà kẻ địch nên quay đầu
        if (player.position.x > transform.position.x && !FashingRight)
        {
            Flip();
        }

        else if (player.position.x < transform.position.x && FashingRight)
        {
            Flip();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSize);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void Flip()
    {
        fasingDir = fasingDir * -1;
        FashingRight = !FashingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipControler(float x)
    {
        if (x > 0 && !FashingRight)
            Flip();
        else if (x < 0 && FashingRight)
            Flip();
    }

    public void UpdateHeath(float health)
    {
        HeathGolem = health;
    }
}
