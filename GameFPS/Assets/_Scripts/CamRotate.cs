using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField, Range(0, 80)]
    private float angleClamp = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerUI.gameStart)
        {
            return;
        }
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 dir = transform.localRotation.eulerAngles;
        dir.x -= mouseY;
        if (dir.x > 180)
        {
            dir.x = Mathf.Clamp(dir.x, 270 + angleClamp, 370);
        }
        else
        {
            dir.x = Mathf.Clamp(dir.x, -10, 90 - angleClamp);
        }
        transform.localRotation = Quaternion.Euler(new Vector3(dir.x, 0, 0));
    }
}
