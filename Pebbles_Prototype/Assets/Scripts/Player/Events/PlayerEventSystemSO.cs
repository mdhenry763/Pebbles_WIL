using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Player/Event System", fileName = "New player hint system")]
public class PlayerEventSystemSO : ScriptableObject
{
    public event Action<bool> OnActivateUI;
    public event Action OnActivateWrenchie;
    public event Action OnShowManual;
    
    //Add more

    public void FireUIEvent(bool activate)
    {
        OnActivateUI?.Invoke(activate);
    }
    
    public void FireWrenchieEvent()
    {
        OnActivateWrenchie?.Invoke();
    }

    public void FireShowManualEvent()
    {
        OnShowManual?.Invoke();
    }
}


