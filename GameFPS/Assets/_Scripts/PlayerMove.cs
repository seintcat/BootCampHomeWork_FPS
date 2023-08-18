using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float speed;

    private bool isJumping;
    private float yVelocity;
    private float gravity = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (characterController.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
            }
            yVelocity = 0f;
        }

        //Debug.Log(yVelocity);

        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);

        yVelocity -= gravity * Time.deltaTime;
        dir.y = yVelocity;

        characterController.Move(dir * speed * Time.deltaTime);
    }
}
