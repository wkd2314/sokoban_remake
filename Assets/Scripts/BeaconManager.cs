using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconManager : MonoBehaviour
{
    private GameObject[] beaconObj;

    public LayerMask whatStopsMovement;

    private int beaconNum;

    private int arrivedNum;

    RaycastHit[] hits;
    // Start is called before the first frame update
    void Start()
    {
        beaconObj = GameObject.FindGameObjectsWithTag("Beacon");
        beaconNum = beaconObj.Length;
    }

    // Update is called once per frame
    void Update()
    {

        arrivedNum = 0;
        foreach (var obj in beaconObj)
        {
            Debug.DrawRay(obj.transform.position + 1.0f * Vector3.up, Vector3.down, Color.red, Time.deltaTime);
            if (Physics.Raycast(obj.transform.position + 1.0f * Vector3.up, Vector3.down, 1.0f, whatStopsMovement))
            {
                arrivedNum += 1;
            }
        }

        if (arrivedNum == beaconNum)
        {
            //clear
            Debug.Log("clear");
        }
    }
}
