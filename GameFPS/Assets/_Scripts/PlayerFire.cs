using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

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
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private PlayerMove player;
    [SerializeField]
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //particleSystem = Instantiate(hitEffect).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hpBar < 0.001)
        {
            return;
        }
        if (!GameManagerUI.gameStart)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Rigidbody bombInstance = ObjectPoolingManager.Pooling(bomb).GetComponent<Rigidbody>();
            bombInstance.velocity = Vector3.zero;
            bombInstance.transform.position = firePos.position;
            bombInstance.AddForce(Camera.main.transform.forward * power, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.Play("demo_combat_shoot");
            Ray ray = new Ray(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                particleSystem.transform.position = hit.point;
                particleSystem.transform.forward = hit.normal;
                particleSystem.Play();

                EnemyFSM enemy = hit.collider.gameObject.GetComponent<EnemyFSM>();
                if (enemy != null)
                {
                    enemy.Damage(damage);
                }
            }
        }
    }
}
