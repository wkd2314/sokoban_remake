using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject prefab;
    private GameObject movePointObj;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    private Vector3 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        movePointObj = Instantiate(prefab, transform.position, Quaternion.identity);
        movePoint = movePointObj.transform;
        prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    RaycastHit[] hits;
    public bool Push(Vector3 direction)
    {
        hits = Physics.RaycastAll(movePoint.position, direction, 1.2f, whatStopsMovement);
        bool flag = true;

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.colliderInstanceID != GetInstanceID())
            {
                flag = false;
                break;
            }
        }
        if (flag)
        {
            movePoint.position += direction;
        }
        return flag;
    }
}
