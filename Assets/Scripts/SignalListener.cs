using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalSender signal;
    public UnityEvent signalEvent;
    public void onSignalRaised()
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.registerListener(this);
    }
    private void OnDisable()
    {
        signal.deregisterListener(this);
    }
}