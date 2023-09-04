using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerUI : MonoBehaviour
{
    public static bool gameStart = false;
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

    private int timeNow;
    private IEnumerator enumerator;

    // Start is called before the first frame update
    void Start()
    {
        timeNow = 0;
        enumerator = GameStarter();
        StartCoroutine(enumerator);
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
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
