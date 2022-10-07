using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoManager : MonoBehaviour
{
    public GameObject selectedTower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ForwardModes()
    {
        selectedTower.GetComponent<Tower>().mode++;
        GetComponent<TMP_Text>().text = selectedTower.GetComponent<Tower>().ReturnMode();
    }

    public void BackwardModes()
    {
        selectedTower.GetComponent<Tower>().mode--;
        GetComponent<TMP_Text>().text = selectedTower.GetComponent<Tower>().ReturnMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
