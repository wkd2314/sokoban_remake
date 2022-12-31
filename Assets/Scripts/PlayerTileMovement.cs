using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;

    private GameObject[] pushableObj;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        pushableObj = GameObject.FindGameObjectsWithTag("Pushable");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        float virticalDir = Input.GetAxisRaw("Vertical");
        float horizontalDir = 0f;
        if (virticalDir == 0)
        {
            horizontalDir = Input.GetAxisRaw("Horizontal");
        }
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {

            if (Mathf.Abs(horizontalDir) == 1f)
            {
                if (!Physics.Raycast(movePoint.position, new Vector3(horizontalDir, 0f, 0f), 1.2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(horizontalDir, 0f, 0f);
                }
                else
                {
                    CheckPush(new Vector3(horizontalDir, 0f, virticalDir));
                }
            }
            else if (Mathf.Abs(virticalDir) == 1f)
            {
                if (!Physics.Raycast(movePoint.position, new Vector3(0f, 0f, virticalDir), 1.2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, 0f, virticalDir);
                }
                else
                {
                    CheckPush(new Vector3(horizontalDir, 0f, virticalDir));
                }
            }
        }


    }

    void CheckPush(Vector3 direction)
    {
        foreach (var obj in pushableObj)
        {
            if (Vector3.Distance(obj.transform.position, movePoint.position + direction) == 0f)
            {
                // obj.transform.position += 0.001f * direction;
                // push
                BoxMovement moveBox = obj.GetComponent<BoxMovement>();
                moveBox.Push(direction);
            }
        }
    }
}
