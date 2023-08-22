using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private EnemyFSM enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, enemy.hpBar, Time.deltaTime);
        transform.forward = Camera.main.transform.forward;
    }
}
