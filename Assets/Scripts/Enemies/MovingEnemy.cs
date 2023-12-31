using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : EnemyAI
{
    [SerializeField] Transform[] PatrolPoints;
    SpriteRenderer SpriteRenderer;
    int i = 0;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (Vector2.Distance(transform.position, PatrolPoints[i].position) < .02f)
        {
            i++;
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
            if (i == PatrolPoints.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[i].position, Movespeed * Time.deltaTime);
    }
}
