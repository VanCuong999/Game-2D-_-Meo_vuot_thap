using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dongBang : MonoBehaviour
{
    private bool isFrozen = false; // Trạng thái đóng băng
    private float originalSpeed;
    private Animator animator; // Animator để quản lý animation

    private void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator của kẻ thù
    }

    void Update()
    {
        
    }

    // Hàm đóng băng kẻ thù trong thời gian cho trước
    public void Freeze(float duration)
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeCoroutine(duration));
        }
    }

    // Coroutine đóng băng kẻ thù
    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;


        // Kích hoạt animation đóng băng (nếu có)
        if (animator != null)
        {
            animator.SetTrigger("Freeze");
        }

        // Chờ thời gian đóng băng
        yield return new WaitForSeconds(duration);

        // Phục hồi trạng thái ban đầu
        isFrozen = false;
    }
}
