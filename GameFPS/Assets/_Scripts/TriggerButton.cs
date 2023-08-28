using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject bridge;
    //[SerializeField]
    //private NavMeshSurface surface;
    [SerializeField]
    private GameObject link;

    // Start is called before the first frame update
    void Start()
    {
        bridge.gameObject.SetActive(false);
        link.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bridge.SetActive(true);
            link.SetActive(true);
            gameObject.SetActive(false);
            //surface.BuildNavMesh();
        }
    }
}
