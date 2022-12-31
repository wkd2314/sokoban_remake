using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconManager : MonoBehaviour
{
    private GameObject[] beaconObj;

    public LayerMask whatStopsMovement;

    private int beaconNum;

    private int arrivedNum;

    private GameObject playerObj;

    private Animator playerAnimator;

    private PlayerTileMovement playerTileMovement;

    public enum GameState
    {
        Playing = 0,
        Paused = 1,
        Cleared = 2
    } 
    private GameState gameState;

    RaycastHit[] hits;
    // Start is called before the first frame update
    void Start()
    {
        beaconObj = GameObject.FindGameObjectsWithTag("Beacon");
        beaconNum = beaconObj.Length;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTileMovement = playerObj.GetComponent<PlayerTileMovement>();
        playerAnimator = playerObj.GetComponent<Animator>();
        gameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Playing) {
            arrivedNum = 0;
            foreach (var obj in beaconObj)
            {
                Debug.DrawRay(obj.transform.position + 1.5f * Vector3.up, Vector3.down, Color.red, Time.deltaTime);
                if (Physics.Raycast(obj.transform.position + 1.5f * Vector3.up, Vector3.down, 1.0f, whatStopsMovement))
                {
                    arrivedNum += 1;
                }
            }
            
            if (arrivedNum == beaconNum)
            {
                gameState = GameState.Cleared;
                //clear
                playerAnimator.SetBool("Clear", true);

                playerAnimator.speed = 1;

                playerTileMovement.enabled = !playerTileMovement.enabled;
            }
        }
        else if (gameState == GameState.Cleared) 
        {
            // rotate to view front
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f), Vector3.up);

            playerObj.transform.rotation = Quaternion.RotateTowards(playerObj.transform.rotation, toRotation, 720f * Time.deltaTime);

        }
    }
}
