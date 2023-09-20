using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerUI : MonoBehaviour
{
    public static bool gameStart = false;
    public static PlayerMove playerNow;

    private static GameManagerUI instance;

    [SerializeField]
    private int checkTime;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private List<GameObject> whenGameOver;
    [SerializeField]
    private GameObject optionUI;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Animator hitAnimator;
    [SerializeField]
    private ParticleSystem shootParticle;
    [SerializeField]
    private TextMeshProUGUI stateText;
    [SerializeField]
    private Animator crossHairNormal;
    [SerializeField]
    private Animator crossHairZoom;
    [SerializeField]
    private GameObject modeNormal;
    [SerializeField]
    private GameObject modeZoom;
    [SerializeField]
    private Camera overlayCam;
    [SerializeField]
    private List<Transform> spawnPos;
    [SerializeField]
    private GameObject playerPref;

    private int timeNow;
    private IEnumerator enumerator;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ConnectionManager.soloMode)
        {
            Instantiate(playerPref);
        }
        else
        {
            PhotonNetwork.Instantiate(playerPref.name, Vector3.zero, Quaternion.identity);
        }
        timeNow = 0;
        instance.gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && enumerator == null)
        {
            gameStart = false;
            optionUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public static void GameStart(Transform camPos, PlayerMove playerMove, PlayerFire playerFire, PUN_PlayerHandler punPlayer)
    {
        instance.gameOverUI.SetActive(false);
        instance.enumerator = instance.GameStarter();
        Cursor.lockState = CursorLockMode.Locked;

        playerNow = playerMove;
        instance.cam.transform.SetParent(camPos);
        instance.cam.transform.localPosition = Vector3.zero;
        playerMove.Set(instance.hpSlider, instance.hitAnimator);
        playerFire.Set(instance.shootParticle, instance.stateText, instance.crossHairNormal, instance.crossHairZoom, instance.modeNormal, instance.modeZoom, instance.overlayCam);
        punPlayer.transform.position = instance.spawnPos[Random.Range(0, instance.spawnPos.Count)].position;

        instance.StartCoroutine(instance.enumerator);
    }

    private IEnumerator GameStarter()
    {
        text.color = Color.green;
        while (timeNow < checkTime)
        {
            text.text = (checkTime - timeNow) + "";
            yield return new WaitForSeconds(1f);
            timeNow++;
        }
        text.text = "Start!";
        yield return new WaitForSeconds(1f);
        foreach (GameObject obj in instance.whenGameOver)
        {
            obj.SetActive(true);
        }
        gameStart = true;
        text.gameObject.SetActive(false);
        StopCoroutine(enumerator);
        enumerator = null;
        yield return null;
    }

    public static void GameEnd()
    {
        instance.gameOverUI.SetActive(true);
        instance.text.gameObject.SetActive(true);
        instance.text.color = Color.red;
        instance.text.text = "Game Over";
        gameStart = false;
        Cursor.lockState = CursorLockMode.None;
        foreach(GameObject obj in instance.whenGameOver)
        {
            obj.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Continue()
    {
        gameStart = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
