using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUN_PlayerHandler : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;
    [SerializeField]
    private PlayerRotate playerRotate;
    [SerializeField]
    private PlayerMove playerMove;
    [SerializeField]
    private PlayerFire playerFire;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private CamRotate camRotate;
    [SerializeField]
    private Transform camPos;
    [SerializeField]
    private List<GameObject> modelOutside;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        if(photonView.IsMine || ConnectionManager.soloMode)
        {
            playerRotate.enabled = true;
            playerFire.enabled = true;
            characterController.enabled = true;
            camRotate.enabled = true;
            GameManagerUI.GameStart(camPos, playerMove, playerFire, this);
            foreach(GameObject obj in modelOutside)
            {
                obj.layer = 7;
            }
        }
    }
}
