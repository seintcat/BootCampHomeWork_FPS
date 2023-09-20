using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    //[SerializeField]
    //private GameObject hitEffect;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private PlayerMove player;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private List<GameObject> gunFire;

    private IEnumerator gunFireNow;
    private int gunFireIndex;
    private bool isGrenade;
    private ParticleSystem _particleSystem;
    private TextMeshProUGUI stateText;
    private Animator crossHair;
    private Animator crossHairZoom;
    private GameObject modeNormal;
    private GameObject modeZoom;
    private Camera overlay;

    // Start is called before the first frame update
    void Start()
    {
        //particleSystem = Instantiate(hitEffect).GetComponent<ParticleSystem>();
        isGrenade = true;
        stateText.text = "Grenade";
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
            if(isGrenade)
            {
                Rigidbody bombInstance = ObjectPoolingManager.Pooling(bomb).GetComponent<Rigidbody>();
                bombInstance.velocity = Vector3.zero;
                bombInstance.transform.position = firePos.position;
                bombInstance.AddForce(Camera.main.transform.forward * power, ForceMode.Impulse);
            }
            else
            {
                Camera.main.fieldOfView = 15;
                overlay.fieldOfView = 15;
                crossHair.gameObject.SetActive(false);
                crossHairZoom.gameObject.SetActive(true);

            }
        }
        if(!isGrenade && Input.GetMouseButtonUp(1))
        {
            Camera.main.fieldOfView = 60;
            overlay.fieldOfView = 60;
            crossHair.gameObject.SetActive(true);
            crossHairZoom.gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.Play("demo_combat_shoot");
            Ray ray = new Ray(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                _particleSystem.transform.position = hit.point;
                _particleSystem.transform.forward = hit.normal;
                _particleSystem.Play();

                EnemyFSM enemy = hit.collider.gameObject.GetComponent<EnemyFSM>();
                if (enemy != null)
                {
                    enemy.Damage(damage);
                    if (crossHair.gameObject.activeSelf)
                    {
                        crossHair.Play("Hit");
                    }
                    else
                    {
                        crossHairZoom.Play("Hit");
                    }
                }
            }

            if(gunFireNow != null)
            {
                StopCoroutine(gunFireNow);
                gunFireNow = null;
                gunFire[gunFireIndex].SetActive(false);
            }
            gunFireNow = GunFire();
            StartCoroutine(gunFireNow);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isGrenade = true;
            Camera.main.fieldOfView = 60;
            overlay.fieldOfView = 60;
            stateText.text = "Grenade";
            crossHair.gameObject.SetActive(true);
            crossHairZoom.gameObject.SetActive(false);
            modeNormal.gameObject.SetActive(true);
            modeZoom.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isGrenade = false;
            stateText.text = "Sniper";
            modeZoom.gameObject.SetActive(true);
            modeNormal.gameObject.SetActive(false);
        }
    }

    private IEnumerator GunFire()
    {
        for(int i = 0; i < 3; i++)
        {
            gunFireIndex = Random.Range(0, gunFire.Count);
            gunFire[gunFireIndex].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            gunFire[gunFireIndex].SetActive(false);
        }
        StopCoroutine(gunFireNow);
        gunFireNow = null;
    }

    public void Set(ParticleSystem shootParticle, TextMeshProUGUI _stateText, Animator crossHairNormal, Animator _crossHairZoom, GameObject _modeNormal, GameObject _modeZoom, Camera overlayCam)
    {
        enabled = true;
        _particleSystem = shootParticle;
        stateText = _stateText;
        crossHair = crossHairNormal;
        crossHairZoom = _crossHairZoom;
        modeNormal = _modeNormal;
        modeZoom = _modeZoom;
        overlay = overlayCam;
    }
}
