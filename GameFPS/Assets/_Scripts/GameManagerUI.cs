using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public static bool gameStart = false;
    private static GameManagerUI instance;

    [SerializeField]
    private int checkTime;
    [SerializeField]
    private TextMeshProUGUI text;

    private int timeNow;
    private IEnumerator enumerator;

    // Start is called before the first frame update
    void Start()
    {
        timeNow = 0;
        enumerator = GameStarter();
        StartCoroutine(enumerator);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        gameStart = true;
        gameObject.SetActive(false);
        StopCoroutine(enumerator);
        enumerator = null;
        yield return null;
    }

    public static void GameEnd()
    {
        instance.gameObject.SetActive(true);
        instance.text.color = Color.red;
        instance.text.text = "Game Over";
        gameStart = false;
    }
}
