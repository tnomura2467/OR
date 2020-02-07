using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ObjectManajer : MonoBehaviour, IInputClickHandler
{
    public GameObject obj;

    private void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        var pos = Camera.main.transform.position;
        var forword = Camera.main.transform.forward;

        Instantiate(obj, pos + forword, new Quaternion());
    }
}