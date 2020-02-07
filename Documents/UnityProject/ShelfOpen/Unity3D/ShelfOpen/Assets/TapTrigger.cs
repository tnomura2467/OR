using UnityEngine;
using System.Collections;
using HoloToolkit.Unity.InputModule;


public class TapTrigger: MonoBehaviour, IInputClickHandler
{
    public bool tap = false;

    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        tap = true;
    }
}