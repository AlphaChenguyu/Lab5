using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �����ƶ��ٶ�

    void Update()
    {
        // ֱ���� -Z �����ƶ�
        transform.position += Vector3.back * moveSpeed * Time.deltaTime; // �� -Z �����ƶ�
    }
}
