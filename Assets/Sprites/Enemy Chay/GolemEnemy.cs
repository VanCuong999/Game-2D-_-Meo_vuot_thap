using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemEnemy : MonoBehaviour
{
    public static GolemEnemy Intance;
    public float HeathGolem;
    private float currentHeath;
    public float fasingDir = 1;
    private bool FashingRight = true;
    [SerializeField] private float Speed;
    [SerializeField] private float lineOfSize;
    [SerializeField] private float shootingRange;
    private Animator anim;
    private Transform player;
    private Transform npc; // Biến lưu trữ NPC
    private float timer;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    public LayerMask npcLayer; // Layer cho NPC
    public EntityFX entityFX;

    [SerializeField] private Image _heathBarFill;
    [SerializeField] private Transform _healthBarTranForm;
    private Camera _camera;
    private Color fullHealthColor = Color.green;
    private Color lowHealthColor = Color.yellow;
    private Color menimumHealthColor = Color.red;

    private bool isFrozen = false; // Trạng thái đóng băng
    private Rigidbody2D rb; // Rigidbody2D của enemy (Thêm vào để dừng chuyển động khi đóng băng)
    public GameObject freezeEffectPrefab; // Prefab hiệu ứng băng
    private GameObject currentFreezeEffect; // Hiệu ứng băng đang được sử dụng

    private void Awake()
    {
        Intance = this;
        _camera = Camera.main;
    }

    void Start()
    {
        currentHeath = HeathGolem;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        npc = GameObject.FindGameObjectWithTag("NPC")?.transform; // Tìm NPC, nếu có
        anim = GetComponent<Animator>();
        if (player == null) return;
    }

    void Update()
    {
        if (isFrozen) return;
        _healthBarTranForm.rotation = _camera.transform.rotation;
        if (player == null && npc == null) return;
        timer += Time.deltaTime;

        float distanceFromPlayer = player != null ? Vector2.Distance(player.position, transform.position) : float.MaxValue;
        float distanceFromNPC = npc != null ? Vector2.Distance(npc.position, transform.position) : float.MaxValue;

        Transform target = null;

        // Xác định mục tiêu gần nhất
        if (distanceFromPlayer < lineOfSize && distanceFromPlayer <= distanceFromNPC)
        {
            target = player; // Tấn công người chơi nếu gần hơn hoặc bằng
        }
        else if (distanceFromNPC < lineOfSize && distanceFromNPC < distanceFromPlayer)
        {
            target = npc; // Tấn công NPC nếu gần hơn
        }

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, Speed * Time.deltaTime);
            FaceTarget(target);
            anim.SetBool("wall", true);

            if (Vector2.Distance(target.position, transform.position) <= shootingRange && timer > 2)
            {
                timer = 0;
                anim.SetTrigger("attack");
            }

        }
        else
        {
            anim.SetBool("wall", false);
        }
    }

    public void EnemyAttack()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer | npcLayer);

        foreach (Collider2D target in hitTargets)
        {
            if (target.CompareTag("Player"))
            {
                Debug.Log("Đánh trúng người chơi");
                HeathPlayer.Intance.TakeHeath(10);
            }
            else if (target.CompareTag("NPC"))
            {
                Debug.Log("Đánh trúng NPC");
                HeathNPC_Follow.Instance.TakeDamage(10); // Thay thế bằng phương thức sát thương của NPC
            }
        }
    }

    public void TakeDangage(float damage)
    {
        if (gameObject != null)
        {
            entityFX.StartCoroutine("FlashFX");
        }
        currentHeath -= damage;

        if (currentHeath <= 0)
        {
            Destroy(gameObject);
            Exp.Intance.TakeExp(Exp.Intance.exp);
            expPlayer.Intancs.GainExperience(20);
        }
        UpdateHealth(HeathGolem);
    }

    private void FaceTarget(Transform target)
    {
        if (target.position.x > transform.position.x && !FashingRight)
        {
            Flip();
        }
        else if (target.position.x < transform.position.x && FashingRight)
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
        fasingDir *= -1;
        FashingRight = !FashingRight;
        transform.Rotate(0, 180, 0);
    }

    public void UpdateHealth(float health)
    {
        HeathGolem = health;
        _heathBarFill.fillAmount = currentHeath / HeathGolem;
        UpdateHealthColor();
    }

    public virtual void UpdateHealthColor()
    {
        float healthPercentage = currentHeath / HeathGolem;

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

    public void Freeze(float duration)
    {
        if (isFrozen)
        {
            Debug.Log("Enemy đã bị đóng băng!"); // Kiểm tra trạng thái đóng băng
            return;
        }

        isFrozen = true; // Đóng băng enemy
        Debug.Log("Enemy bắt đầu bị đóng băng!");

        // Dừng chuyển động
        if (rb != null) rb.velocity = Vector2.zero;

        // Tắt animation
        if (anim != null) anim.enabled = false;

        // Tạo hiệu ứng băng dưới chân enemy
        if (freezeEffectPrefab != null)
        {
            currentFreezeEffect = Instantiate(freezeEffectPrefab, transform.position, Quaternion.identity);
            currentFreezeEffect.transform.SetParent(transform); // Gắn hiệu ứng vào enemy
        }

        StartCoroutine(ThawOut(duration)); // Đặt thời gian chờ để đóng băng
    }

    private IEnumerator ThawOut(float duration)
    {
        yield return new WaitForSeconds(duration); // Chờ hết thời gian đóng băng
        isFrozen = false; // Khôi phục trạng thái bình thường

        // Bật lại animation
        if (anim != null) anim.enabled = true;

        // Xóa hiệu ứng băng
        if (currentFreezeEffect != null)
        {
            Destroy(currentFreezeEffect);
            currentFreezeEffect = null;
        }
    }
}