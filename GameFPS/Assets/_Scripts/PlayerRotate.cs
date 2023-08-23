using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float speed = 200f;

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

        float mouseX = Input.GetAxis("Mouse X");

        Vector3 dir = new Vector3(0, Mathf.Clamp(mouseX, -90, 90), 0);

        transform.eulerAngles += (dir * speed * Time.deltaTime);
    }
}
