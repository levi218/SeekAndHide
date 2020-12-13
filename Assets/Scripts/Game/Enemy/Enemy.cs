using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : MonoBehaviour
{
    public EnemyInformation info;

    public float visionAngle = 45;

    public float visionSize = 2f;

    public float speed = 3f;

    [HideInInspector]
    public Vector2 direction;

    Transform target;

    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    public float updateInterval = 2f;
    float lastUpdate;
    Vector3 lastPos;

    TargetChooser targetChooser;
    private void Awake()
    {
        GetComponent<CircleCollider2D>().radius = visionSize;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        targetChooser = GetComponent<TargetChooser>();
        lastPos = transform.position;
    }
    private void Start()
    {
        Invoke("StartAction", 1f);
    }
    private void StartAction()
    {
        UpdatePath(targetChooser.GetTarget());
    }
    public void UpdatePath(Transform target)
    {
        this.target = target;
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathGenerationCompleted);
    }
    void OnPathGenerationCompleted(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }


        if (Time.fixedTime - lastUpdate > 5f)
        {
            if (Vector3.Distance(lastPos, transform.position) < 1f)
                UpdatePath(targetChooser.GetTarget());
            lastUpdate = Time.fixedTime;
            lastPos = transform.position;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            UpdatePath(targetChooser.GetTarget());
            return;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Vector2 force = direction * speed*100 * Time.fixedDeltaTime;

        rb.AddForce(force);
        //rb.velocity = Vector3.Lerp(rb.velocity, direction * speed,0.1f);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) currentWaypoint++;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameController.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
            && !info.encoutered)
        {
            GameController.Instance.ShowInstruction(info);
            info.encoutered = true;
        }
    }
}
