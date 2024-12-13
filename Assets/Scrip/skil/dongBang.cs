using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dongBang : MonoBehaviour
{
    private bool isFrozen = false;
    private float originalSpeed;
    public float speed = 2f;

    private Animator animator; // Animator để quản lý animation

    private void Start()
    {
        originalSpeed = speed;
        animator = GetComponent<Animator>(); // Lấy Animator trên đối tượng
    }

    void Update()
    {
        if (!isFrozen)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    public void Freeze(float duration)
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeCoroutine(duration));
        }
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        speed = 0f;

        // Kích hoạt animation đóng băng
        if (animator != null)
        {
            animator.SetTrigger("Freeze");
        }

        yield return new WaitForSeconds(duration);

        isFrozen = false;
        speed = originalSpeed;

        // Nếu cần, thêm logic phục hồi trạng thái
    }
}
