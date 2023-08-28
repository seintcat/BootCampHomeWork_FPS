using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField id;
    [SerializeField]
    private TMP_InputField pass;
    [SerializeField]
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SignUp()
    {
        if (!PlayerPrefs.HasKey(id.text))
        {
            if (id.text == "" || pass.text == "")
            {
                text.text = "ÀÀ ±×·¸°Õ ¾ÈµÊ ¤µ¤¡~";
                return;
            }
            PlayerPrefs.SetString(id.text, pass.text);
            text.text = "Sign up complete!";
        }
        else
        {
            text.text = "ID allready exist!";
        }
    }

    public void LogIn()
    {
        if (!PlayerPrefs.HasKey(id.text))
        {
            text.text = "ID cannot found!";
        }
        else if(PlayerPrefs.GetString(id.text, "") == pass.text)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            text.text = "Login failed!";
        }
    }
}
