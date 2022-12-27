using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconEffectHandler : MonoBehaviour
{
    public LayerMask whatStopsMovement;

    // Start is called before the first frame update
    void Start()
    {
        whatStopsMovement = 7;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Physics.Raycast(transform.position, new Vector3(0f, 1f, 0f), 1.2f, whatStopsMovement))
        // {
        //     // 상자가 올라왔을때 이펙트라던지 처리.
        // }
        // else
        // {
        // }
    }
}