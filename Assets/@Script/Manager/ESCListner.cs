using UnityEngine;
using UnityEngine.Events;

public class ESCListner:MonoBehaviour
{
    public UnityEvent doOnEsc;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            doOnEsc.Invoke();
        }
    }
}