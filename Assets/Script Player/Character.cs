using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character Intance;
    [Header("Speed")]
    public float moveSpeed;
    private float xInput;
    private float yInput;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Quay Đầu")]
    [HideInInspector] public float fasingDir = 1;
    private bool FashingRight = true;
    [Header("Dash setting")]
    public float DashSpeed = 30f;
    
    public float dashTime;
    private  float _dashTime;
    private bool isDashing = false;
    
    public GameObject dashPrefab;
    public Transform dashcheck;

    [Header("Tăng Tốc Độ")]
    public float speedBoostAmount = 10f;
    public float speedBoostDuration = 5f;
    private bool isSpeedBoosted = false;
    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    private void Awake() 
    {
        Intance = this;    
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical"); 

        FlipControler(xInput);
        if(xInput != 0 || yInput!= 0)
        {
            anim.SetBool("move",true);
        }else anim.SetBool("move",false);


        rb.velocity = new Vector2(xInput * moveSpeed, yInput * moveSpeed);

        
    }

    public void KichHoatTanCong()
    {
        anim.SetTrigger("attack");
    }
    public void KichHoatDie()
    {
        anim.SetTrigger("die");
    }
    public void KichHoatChieuThuc()
    {
        anim.SetTrigger("attack2");
    }
    public void DeletePlayer()
    {
        Destroy(gameObject);
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _dashTime <= 0 && isDashing == false)
        {
            moveSpeed += DashSpeed;
            _dashTime = dashTime;
            isDashing = true;
            GameObject slash = Instantiate(dashPrefab,dashcheck.position,Quaternion.identity);
            slash.transform.rotation = this.transform.rotation;
            Destroy(slash,0.5f);
        }
        if (_dashTime <= 0 && isDashing == true)
        {
            moveSpeed -= DashSpeed;
            isDashing = false;
        }
        else
        {
            _dashTime -= Time.deltaTime;
        }
    }
    
    public void AttackPlayer()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            
        }
    }

    public void Flip()
    {
        fasingDir = fasingDir * -1;
        FashingRight = !FashingRight;
        transform.Rotate(0,180,0);
    }
    public void FlipControler(float x)
    {
        if(x >0 && !FashingRight)
            Flip();
        else if(x <0 && FashingRight)
            Flip();
    }

    // Phương thức để kích hoạt tăng tốc độ
    public void ActivateSpeedBoost()
    {
        if (!isSpeedBoosted)
        {
            StartCoroutine(SpeedBoostCoroutine());
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        isSpeedBoosted = true;
        moveSpeed += speedBoostAmount;

        yield return new WaitForSeconds(speedBoostDuration);

        moveSpeed -= speedBoostAmount;
        isSpeedBoosted = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("ItemSpeed"))
        {
            ActivateSpeedBoost();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("ItemHeath"))
        {
            HeathPlayer.Intance.HoiMau(20);
            Destroy(other.gameObject);
        }   
    }

    void OnDrawGizmosSelected() 
    {
        if (attackPoint == null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);    
    }
}
