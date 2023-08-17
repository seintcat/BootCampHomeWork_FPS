using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(Mathf.Clamp(-mouseY, -90, 90), 0, 0);

        transform.eulerAngles += (dir * speed * Time.deltaTime);
    }
}
