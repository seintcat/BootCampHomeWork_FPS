using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private Transform firePos;
    [SerializeField]
    private float power;
    [SerializeField]
    private GameObject hitEffect;
    [SerializeField]
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        //particleSystem = Instantiate(hitEffect).GetComponent<ParticleSystem>();
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                particleSystem.transform.position = hit.point;
                particleSystem.transform.forward = hit.normal;
                particleSystem.Play();
            }
        }
    }
}
