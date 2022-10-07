using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class Enemy : MonoBehaviour
{
    public int currentPoint = 0;
    public bool pointFlag = true;
    public List<Transform> points = new();
    public float distOnTrack;
    public float maxHealth = 2;
    public float health = 2;
    public float speed=2;
    protected SpriteRenderer spriteRenderer;
    public Sprite stun;
    protected Sprite original;
    protected bool canMove = true;
    protected float waitTime = 0;
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        original = spriteRenderer.sprite;
    }

    public void StartRest(float restTime)
    {
        StartCoroutine(RestTime(restTime));
    }
    public IEnumerator RestTime(float restTime)
    {

        canMove = false;
        float time = Time.time;



        spriteRenderer.sprite = stun;
        waitTime++;
        yield return new WaitForSeconds(restTime);
        waitTime--;
        print(waitTime);
        if (waitTime <= 0)
        {
            spriteRenderer.sprite = original;
            canMove = true;
        }

        // StopCoroutine((RestTime(1)));
    }
    private void Update()
    {
        if (PointCheck())
            Movement();


        HealthCheck();


    }

    protected virtual bool PointCheck()
    {
        if (currentPoint < points.Count)
        {
            if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.2 && pointFlag)
            {
                pointFlag = false;
                currentPoint++;

            }
            else if (Vector3.Distance(transform.position, points[currentPoint].position) > 0.2)
            {
                pointFlag = true;
            }
            return true;
        }
        Destroy(gameObject);
        return false;
    }


    abstract protected void Movement();

    void HealthCheck()
    {
        if (health < (maxHealth * 0.25f))
        {
            spriteRenderer.color = new Color(255, 0, 0);
        }
        else if (health < (maxHealth * 0.5f))
        {
            spriteRenderer.color = new Color(220, 0, 0);
        }
        else if (health < (maxHealth * 0.75f))
        {
            spriteRenderer.color = new Color(195, 0, 0);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        Projectile p = collision.GetComponent<Projectile>();
        if (p.stunTime != 0)
        {
            float stunTime = RhythmManager.instance.secPerBeat * p.stunTime;
            Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, p.radius, p.layer);
            foreach (Collider2D r in results)
            {
                r.GetComponent<Enemy>().StartRest(stunTime);
            }
            results = null;
            print("wait for " + stunTime);

            StartCoroutine(RestTime(RhythmManager.instance.secPerBeat * p.stunTime));
        }
        else
        {
            health -= p.damage;
        }
        print("hit");
        p.hits--;
    }



}


