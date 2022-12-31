using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    public float rotationSpeed = 720f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;

    private GameObject[] pushableObj;

    private Animator animator;
    private float runExtraTime = 0f;

    private Vector3 movementDirection = Vector3.zero;

    private bool pushingBoxFlag = false;
    private float boxMovingSpeed = 0f;
    private Vector3 prevDirection;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        pushableObj = GameObject.FindGameObjectsWithTag("Pushable");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var distToMovePoint = Vector3.Distance(transform.position, movePoint.position);
        if(distToMovePoint != 0f) {
            animator.speed = 1;
            //rotation
            if(movementDirection != Vector3.zero) 
            {
                Quaternion toRotation = Quaternion.LookRotation(-movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            if(pushingBoxFlag) {
                animator.SetBool("Pushing", pushingBoxFlag);
                transform.position = Vector3.MoveTowards(transform.position, movePoint.position, boxMovingSpeed * Time.deltaTime);
            } else {
                transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
                animator.SetFloat("Speed", 1);
                runExtraTime = 0.2f;
            }

            
        } else {
            if(pushingBoxFlag) {
                pushingBoxFlag = false;
                animator.speed = 0;
            } else {
                runExtraTime -= Time.deltaTime;
                animator.SetFloat("Speed", runExtraTime);
            }
            
            if(movementDirection != Vector3.zero) {
                prevDirection = movementDirection;
            }
            
            float virticalDir = Input.GetAxisRaw("Vertical");
            float horizontalDir = 0f;
            if (virticalDir == 0) // 대각선 방지
            {
                horizontalDir = Input.GetAxisRaw("Horizontal");
            }
            movementDirection = new Vector3(horizontalDir, 0f, virticalDir);

            if(movementDirection != Vector3.zero) {
                if(movementDirection != prevDirection) {
                    animator.SetBool("Pushing", pushingBoxFlag);
                }
            } 
            

            // movement
            if (Mathf.Abs(horizontalDir) == 1f)
            {
                if (!Physics.Raycast(movePoint.position, movementDirection, 1.2f, whatStopsMovement))
                {
                    movePoint.position += movementDirection;
                }
                else
                {
                    CheckPush(movementDirection);
                }
            }
            else if (Mathf.Abs(virticalDir) == 1f)
            {
                if (!Physics.Raycast(movePoint.position, movementDirection, 1.2f, whatStopsMovement))
                {
                    movePoint.position += movementDirection;
                }
                else
                {
                    CheckPush(movementDirection);
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
                pushingBoxFlag = moveBox.Push(direction);

                if(pushingBoxFlag) {
                    // move with box
                    boxMovingSpeed = moveBox.moveSpeed;

                    movePoint.position += movementDirection;
                }
            }
        }
    }
}
