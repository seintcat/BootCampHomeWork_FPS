using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private Transform firePos;
    [SerializeField]
    private float power;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Rigidbody bombInstance = Instantiate(bomb).GetComponent<Rigidbody>();
            bombInstance.transform.position = firePos.position;
            bombInstance.AddForce(Camera.main.transform.forward * power, ForceMode.Impulse);
        }
    }
}
