using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    [SerializeField]
    private GameObject fx;
    [SerializeField]
    private float explosionRadius = 5;
    [SerializeField]
    private int dmage = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<EnemyFSM>().Damage(dmage);
        }

        GameObject obj = Instantiate(fx);   
        obj.transform.position = transform.position;
        Destroy(obj, 3f);
        gameObject.SetActive(false);
    }
}
