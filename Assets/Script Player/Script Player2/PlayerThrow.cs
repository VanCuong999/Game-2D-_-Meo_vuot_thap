using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [Header("Thông tin bom")]
    [SerializeField] private GameObject bombPrefab; // Prefab của bom
    [SerializeField] private float throwForce = 10f; // Lực ném
    [SerializeField] private float throwAngle = 45f; // Góc ném

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Thay đổi theo nút bạn muốn
        {
            StartCoroutine(ThrowBomb());
        }
    }

    private IEnumerator ThrowBomb()
    {
        // Tạo bom tại vị trí của nhân vật
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);

        // Tính toán hướng ném theo hướng đối diện của nhân vật
        Vector2 throwDirection = (Vector2)transform.right; // Sử dụng hướng của nhân vật (thường là trục X)

        // Tính toán trọng số cho góc ném
        float radians = throwAngle * Mathf.Deg2Rad;
        Vector2 initialVelocity = new Vector2(throwDirection.x * Mathf.Cos(radians), throwDirection.y * Mathf.Sin(radians)) * throwForce;

        // Thời gian ném
        float flightDuration = 2.0f; // Thay đổi thời gian bay nếu cần
        float elapsedTime = 0;

        while (elapsedTime < flightDuration)
        {
            // Cập nhật vị trí theo hình vòng cung
            float t = elapsedTime / flightDuration; // Tỉ lệ thời gian
            float yOffset = Mathf.Sin(t * Mathf.PI) * throwForce * 0.5f; // Hệ số cho hình vòng cung

            bomb.transform.position = (Vector2)transform.position + initialVelocity * t + new Vector2(0, yOffset);
            elapsedTime += Time.deltaTime;
            yield return null; // Chờ đến khung hình tiếp theo
        }

        // Hủy bom sau khi ném
        Destroy(bomb, 5f); // Hủy bom sau 5 giây nếu không nổ
    }
}