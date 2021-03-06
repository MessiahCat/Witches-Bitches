﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public List<Transform> players;
    public float moveSpd = 1f;
    public float stoppingDistance;
    public bool isWandering = false;
    public bool canShoot = false;
    public float fireRate = 5f;
    public Timer shootTimer;
    public GameObject projectilePrefab;
    public Animator animator;
    public Vector3 moveSpot;
    private float wait;
    public float waitTime = 2f;
    public float minX = -9.5f;
    public float maxX = 9.5f;
    public float minY = -12.5f;
    public float maxY = 6.5f;


    private void Start()
    {
        wait = waitTime;
        moveSpot = CreateMoveSpot();
        Transform player1 = GameObject.Find("Player1").transform;
        Transform player2 = GameObject.Find("Player2").transform;
        players[0] = player1;
        players[1] = player2;
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (isWandering)
        {
            Wander();
        } else if (players.Count == 0) return;
        else Move();
    }

    void Wander()
    {
        if (transform.position.x > moveSpot.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < moveSpot.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, moveSpot, moveSpd * Time.deltaTime);
        if (canShoot && shootTimer.Done)
        {
            GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Transform target = GetClosestTarget(players[0], players[1]);
            Vector2 targetDirection = (target.transform.position - bullet.transform.position).normalized * 5;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y);
            shootTimer.SetTime(fireRate, "");
        }
        if (Vector2.Distance(transform.position, moveSpot) < 0.2f)
        {
            if (wait <= 0)
            {
                moveSpot = CreateMoveSpot();
                wait = waitTime;
                animator.SetFloat("Speed", 1f);
            } else
            {
                wait -= Time.deltaTime;
                animator.SetFloat("Speed", 0f);
            }
        }

    }

    void Move()
    {
        Transform target = GetClosestTarget(players[0], players[1]);
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpd * Time.deltaTime);
        } //else shoot projectile or something
    }

    Transform GetClosestTarget(Transform object1, Transform object2)
    {
        float target1 = GetDistance(gameObject.transform, object1);
        float target2 = GetDistance(gameObject.transform, object2);
        if (target1 <= target2) return object1;
        else return object2;
    }

    float GetDistance(Transform object1, Transform object2)
    {
        return Vector2.Distance(object1.transform.position, object2.transform.position); 
    }

    Vector3 CreateMoveSpot()
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
    }


}
