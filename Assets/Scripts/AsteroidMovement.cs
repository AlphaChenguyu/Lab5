using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 调整移动速度

    void Update()
    {
        // 直接向 -Z 方向移动
        transform.position += Vector3.back * moveSpeed * Time.deltaTime; // 向 -Z 方向移动
    }
}
