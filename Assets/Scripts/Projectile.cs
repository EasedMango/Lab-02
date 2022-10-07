using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Projectile : MonoBehaviour
{
    public Vector3 dir;
    public float speed = 3;
    public int hits = 1;
    public float damage = 1;
    public float stunTime;
    public float radius;
    [SerializeField]
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        dir = (dir - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (radius != 0)
        {
            
            
        }
        if(transform.position.magnitude >= 10)
        {
            Destroy(gameObject);
        }

        transform.position += dir * Time.deltaTime * speed;

        if (hits <= 0)
            Destroy(gameObject);
    }



}
