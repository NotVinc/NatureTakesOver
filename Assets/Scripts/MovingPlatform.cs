using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlattform : MonoBehaviour
{
    public Transform pointA, pointB;
    public int Speed;
    Vector2 targetPos;
    public GameObject Player;

    private void Start()
    {
        targetPos = pointB.position;
        Player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, pointA.position) < .1)
        {
            targetPos = pointB.position;
        }
        if (Vector2.Distance(transform.position, pointB.position) < .1)
        {
            targetPos = pointA.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.transform.SetParent(null);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}