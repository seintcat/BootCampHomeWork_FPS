using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingNextScene : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber;
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private TMP_Text loadingText;

    private IEnumerator loadingNextScene;

    // Start is called before the first frame update
    void Start()
    {
        loadingNextScene = AsyncNextScnene();
        StartCoroutine(loadingNextScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AsyncNextScnene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNumber);
        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingText.text = (asyncOperation.progress * 100).ToString() + "%";

            if (asyncOperation.progress >= 0.90f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
