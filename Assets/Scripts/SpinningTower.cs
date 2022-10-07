using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTower :  Tower
{
    // Start is called before the first frame update
    override protected void AimAt()
    {
       // Vector3 dir = (target - transform.position).normalized;
        transform.rotation = (Quaternion.Euler(0, 0, Mathf.Atan2(Mathf.Sin(Time.time), Mathf.Cos(Time.time)) * Mathf.Rad2Deg));
    }

    override protected void TowerUpdate() 
    {
        AimAt();
       ShootOnBeat();
    }
    // Update is called once per frame

    private void Update()
    {
        UpdateParameters();
    }

}
