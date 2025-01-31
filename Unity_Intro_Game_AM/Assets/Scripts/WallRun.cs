using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform orientation;
    [SerializeField] private float minimumSpeed = 1;

    [Header("Detection")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;

    private bool wallLeft = false;
    private bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private Rigidbody RB;

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
   private void Update()
   {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft && RB.velocity.magnitude > minimumSpeed)
            {
                StartWallRun();
                Debug.Log("Wall Run on Left");
            }
            else if (wallRight && RB.velocity.magnitude > minimumSpeed)
            {
                StartWallRun();
                Debug.Log("Wall Run on Right");
            }
            else
            {
                StopWallRun();
            }
        }
   }


    void StartWallRun()
    {
        RB.useGravity = false;

        RB.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
                RB.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
                RB.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        RB.useGravity = true;

    }
}
