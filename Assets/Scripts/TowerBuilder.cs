using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    private enum MyEnum
    {
        QuaterTower = 0, HalfTower
    }
    Vector3 mouse;
    public GameObject QuaterTower;
    public GameObject HalfTower;
    public GameObject SpinningTower;
    public GameObject RestTower;
    GameObject currentTower;

    GameObject[,] vendingItems;

    // Start is called before the first frame update
    void Start()
    {
        //  Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetTower(int name)
    {
        switch (name)
        {
            case 0:
                currentTower = Instantiate(QuaterTower, (Vector2)mouse, Quaternion.identity);
                break;
            case 1:
                currentTower = Instantiate(HalfTower, (Vector2)mouse, Quaternion.identity);
                break;
            case 2:
                currentTower = Instantiate(SpinningTower, (Vector2)mouse, Quaternion.identity);
                break;
            case 3:
                currentTower = Instantiate(RestTower, (Vector2)mouse, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentTower != null)
        {
            currentTower.transform.position = (Vector2)mouse;
            if (Input.GetMouseButtonDown(0))
            {
                currentTower.GetComponent<Tower>().enabled = true;
                currentTower = null;
            }
        }
    }
    void FixedUpdate()
    {

    }
}
