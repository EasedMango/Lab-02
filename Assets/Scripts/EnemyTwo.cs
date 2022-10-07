using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyTwo : Enemy
{
    public float shootEveryXBeats = 1.0f;
    private float prevPositionInBeats = 3;

    protected override void Movement()
    {
       
        if ( (RhythmManager.instance.loopPositionInBeats % shootEveryXBeats) <= 0.08f && Mathf.Floor( RhythmManager.instance.songPositionInBeats )!= prevPositionInBeats)
        {
            prevPositionInBeats = Mathf.Floor(RhythmManager.instance.songPositionInBeats);
            canMove = !canMove;
        }


        if (canMove == true)
        {

            Vector3 amount = ((points[currentPoint].position - transform.position).normalized*Time.deltaTime * RhythmManager.instance.secPerBeat)*speed;
            transform.position += amount;
            distOnTrack += amount.magnitude;
        }
    }

}
