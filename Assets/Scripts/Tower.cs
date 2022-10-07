using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{

    public float Radius = 4f;
    public Vector3 target = Vector3.zero;
    public bool canShoot = true;
    public GameObject ammoPrefab;
    public float reloadTime = 1.5f;
    [SerializeField]
    public LayerMask layer;
    public int mode = 0;
    public float shootEveryXBeats = 1;
    public bool targets = false;

    /// <summary>
    /// Gets enemy furthest along track
    /// </summary>
    /// <returns></returns>
    Vector3 GetFirst()
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, Radius, layer);
        if (results.Length > 0)

            targets = true;
        else
            targets = false;
        float highDist = 0;
        Vector3 target_ = Vector3.one;
        foreach (Collider2D c in results)
        {
            float enemyDist = c.GetComponent<Enemy>().distOnTrack;
            if (enemyDist >= highDist)
            {
                highDist = enemyDist;
                target_ = c.transform.position;
            }
        }

        return target_;


    }

    //get's enemy last on track
    Vector3 GetLast()
    {

        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, Radius, layer);
        if (results.Length > 0)

            targets = true;
        else
            targets = false;
        float lowDist = 1000;
        Vector3 target_ = Vector3.one;
        foreach (Collider2D c in results)
        {
            float enemyDist = c.GetComponent<Enemy>().distOnTrack;
            if (enemyDist <= lowDist)
            {
                lowDist = enemyDist;
                target_ = c.transform.position;
            }
        }

        return target_;


    }

    //get's enemy physicly closest to the tower
    Vector3 GetClose()
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, Radius, layer);
        if (results.Length > 0)

            targets = true;
        else
            targets = false;
        Vector3 close = Vector3.forward * 100;
        float dist = 100;
        foreach (Collider2D c in results)
        {

            float distCheck = Vector3.Distance(transform.position, c.transform.position);
            print(distCheck.ToString() + " < " + dist.ToString());
            if (distCheck < dist)
            {
                dist = distCheck;
                close = c.transform.position;
            }

        }

        return close;



    }

    //return current mode
    public string ReturnMode() => mode switch
    {
        0 => "First",
        1 => "Last",
        2 => "Close",
        _ => "Broken",
    };
    private void Update()
    {
       UpdateParameters();
    }
    IEnumerator ShootTime()
    {

        canShoot = false;
        GameObject clone = Instantiate(ammoPrefab);
        clone.GetComponent<Projectile>().dir = transform.right + transform.position;
        clone.transform.position = transform.position;

        yield return new WaitForSeconds(RhythmManager.instance.secPerBeat);
        canShoot = true;
    }
    protected virtual void Shoot()
    {
        GameObject clone = Instantiate(ammoPrefab);
        clone.GetComponent<Projectile>().dir = transform.right + transform.position;
        clone.transform.position = transform.position;
    }
    protected virtual void AimAt()
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.rotation = (Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));

        //transform.RotateAround(transform.position, transform.forward, Mathf.Atan2(target.y, target.x));
        switch (mode)
        {
            case 0:
                target = GetFirst();
                break;
            case 1:
                target = GetLast();
                break;
            case 2:
                target = GetClose();
                break;
            default:
                break;
        }
    }
    protected void ShootOnBeat()
    {
        if ((RhythmManager.instance.loopPositionInBeats % shootEveryXBeats) <= 0.05f)
        {
            // print("shooting");
            if (canShoot == true)
            {
                canShoot = false;
                Shoot();
            }
        }
        else { canShoot = true; }// print("not shooting"); }
    }

    protected bool CheckForTargets()
    {
        if (targets)
        {
            return true;
        }
        else
        {
            //transform.rotation = Quaternion.identity;
            return false;
        }
    }

    protected void UpdateParameters()
    {
        if (mode > 2)
        {
            mode = 0;
        }
        else if (mode < 0)
        {
            mode = 2;
        }
        TowerUpdate();
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(transform.position, mouse) < 0.5f && Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Info").GetComponent<InfoManager>().selectedTower = this.gameObject;
        }
    }

    protected virtual void TowerUpdate()
    {
        AimAt();
        if (CheckForTargets())
        {
           
            ShootOnBeat();
        }
    }


    // Update is called once per frame


    private void OnDrawGizmos()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, Radius);
        Handles.DrawLine(transform.position, mouse);
        Handles.color = Color.green;
        Handles.DrawLine(transform.position, transform.position + transform.up);
        Handles.color = Color.red;
        Handles.DrawLine(transform.position, transform.position + transform.right);
    }
}
