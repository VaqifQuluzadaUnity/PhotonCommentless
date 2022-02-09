using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerLocomotionController : MonoBehaviourPunCallbacks
{
    public float moveSpeed;

    Rigidbody playerRb;

    //Camera and look properties
    public Camera playerCamera;

    private float lookHorizontal;

    private float lookVertical;

    public float mouseSensitivity;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!photonView.IsMine&& PhotonNetwork.IsConnected)
        {
            return;
        }
        LookAround();
    }
    private void FixedUpdate()
    {
        if (!photonView.IsMine&& PhotonNetwork.IsConnected)
        {
            return;
        }
        Move();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        playerRb.velocity = (moveX * playerRb.transform.right + moveY * playerRb.transform.forward)*moveSpeed;
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;

        lookVertical -= mouseY;

        lookVertical = Mathf.Clamp(lookVertical, -120f, 90f);

        transform.Rotate(transform.up * mouseX);

        playerCamera.transform.localRotation = Quaternion.Euler(lookVertical, 0, 0);

    }
}
