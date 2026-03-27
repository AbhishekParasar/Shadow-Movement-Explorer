using System;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{

    [NonSerialized] GameObject arrowGO;

    public Vector3 moveDirection = Vector3.right;
    public float distance = 0.5f;
    public float speed = 0.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void LateUpdate()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPos + moveDirection.normalized * pingPong;
    }
}
