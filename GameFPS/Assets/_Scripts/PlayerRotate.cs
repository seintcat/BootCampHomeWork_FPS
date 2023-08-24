using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        //Vector3 dir = new Vector3(0, Mathf.Clamp(mouseX, -90, 90), 0);

        //transform.eulerAngles += (dir * speed * Time.deltaTime);

        Vector3 temp = transform.eulerAngles;

        transform.rotation *= Quaternion.AngleAxis(mouseX, new Vector3(0, 1, 0));

        //if(temp.y < (transform.eulerAngles.y - 30) || temp.y > (transform.eulerAngles.y + 30))
        //{
        //    if(!(transform.eulerAngles.y < 20 || transform.eulerAngles.y > 340))
        //    {
        //        Debug.Log(temp.y + ", " + transform.eulerAngles.y);
        //        transform.eulerAngles = temp;
        //    }
        //}
    }
}



//transform.eulerAngles += (new Vector3(0, mouseX, 0) * speed * Time.deltaTime);



//transform.Rotate(new Vector3(0, mouseX, 0) * speed * Time.deltaTime);



//Quaternion quaternion = transform.rotation;
//quaternion.Set(quaternion.x, quaternion.y + (mouseX * speed * Time.deltaTime / 360), quaternion.z, quaternion.w);
//transform.rotation = quaternion;