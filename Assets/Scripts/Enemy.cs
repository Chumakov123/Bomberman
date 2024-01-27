using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructible
{
    public float moveSpeed = 8f;
    public float turnSpeed = 1000f;
    private Rigidbody rb;

    private List<Vector3> waypoints = new List<Vector3>();
    private int currentPoint = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdatePath();
        EventManager.Instance.OnPlayerMoved.AddListener(UpdatePath);
        EventManager.Instance.OnBrickDestroy.AddListener(UpdatePath);
    }
    private void Update()
    {
        if (currentPoint < waypoints.Count)
        {
            if (Vector3.Distance(transform.position, waypoints[currentPoint]) > 1f)
            {
                var movement = (waypoints[currentPoint] - transform.position).normalized;
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * turnSpeed);
                rb.velocity = movement * moveSpeed;
            }
            else
            {
                ++currentPoint;
            }
        }
    }
    private void UpdatePath()
    {
        var path = Pathfinding.FindPath(LevelManager.Instance.grid,
            Mathf.RoundToInt(transform.position.x / 4),
            Mathf.RoundToInt(-transform.position.z / 4),
            Mathf.RoundToInt(LevelManager.Instance.player.transform.position.x / 4),
            Mathf.RoundToInt(-LevelManager.Instance.player.transform.position.z / 4));
        if (path != null)
        {
            waypoints.Clear();
            currentPoint = 0;
            for (int i = 0; i < path.Count; i++)
            {
                waypoints.Add(new Vector3(path[i].X * 4, 0, -path[i].Y * 4));
            }
        }
    }
}
