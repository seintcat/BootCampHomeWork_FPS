using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventInvoker : MonoBehaviour
{
    [SerializeField]
    private UnityEvent invokeEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeEvent()
    {
        invokeEvent.Invoke();
    }
}
