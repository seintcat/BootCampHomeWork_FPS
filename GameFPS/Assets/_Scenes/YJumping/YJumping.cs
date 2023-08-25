using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class YJumping : MonoBehaviour
{
    public float speed = 200f;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.rotation.eulerAngles;


        transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, new Vector3(0, 1, 0));


        float angle = dir.y - transform.rotation.eulerAngles.y;
        if((angle < -30 || angle > 30) && (dir.y < 350 && dir.y > 10))
        {
            Debug.Log(dir.y + ", " + transform.rotation.eulerAngles.y + ", " + Time.deltaTime);
            text.text = dir.y + ", " + transform.rotation.eulerAngles.y + ", " + Time.deltaTime;
        }
        //Debug.Log(Time.deltaTime);
    }
}


/*
 100.25, 59.63031, 0.2030987
44.98977, 4.452141, 0.2026881
270.2246, 227.2042, 0.215102
 */