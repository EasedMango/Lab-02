using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyOne : Enemy
{
    protected override void Movement()
    {
        if (canMove)
        {
            Vector3 amount = ((points[currentPoint].position - transform.position).normalized * Time.deltaTime) * RhythmManager.instance.secPerBeat * speed;
            transform.position += amount;
            distOnTrack += amount.magnitude;

        }
    }


}
