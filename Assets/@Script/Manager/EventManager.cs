using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnType();
    public static event OnType onType;

    public static void DoOnType()
    {
        if(onType!=null)
        {
            onType.Invoke();
        }
    }
    public delegate void OnAddTarget(EnemyController _enemy);
    public static event OnAddTarget onAddTarget;

    public static void DoOnAddTarget(EnemyController _enemy)
    {
        if (onAddTarget != null)
        {
            onAddTarget.Invoke(_enemy);
        }
    }

    public delegate void OnRestartEndless();
    public static event OnRestartEndless onRestartEndless;

    public static void DoOnRestartEndless()
    {
        if (onRestartEndless != null)
        {
            onRestartEndless.Invoke();
        }
    }

    public delegate void OnFalse();
    public static event OnFalse onFalse;

    public static void DoOnFalse()
    {
        if (onFalse != null)
        {
            onFalse.Invoke();
        }
    }
}
