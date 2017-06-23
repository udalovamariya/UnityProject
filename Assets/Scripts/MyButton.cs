using UnityEngine.Events;
using UnityEngine;
public class MyButton : MonoBehaviour
{
    public UnityEvent SignalOnClick = new UnityEvent();
    public void _onClick()
    {
        SignalOnClick.Invoke();
    }
}