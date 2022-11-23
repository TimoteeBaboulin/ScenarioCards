using System;
using UnityEngine;

public class GameVariablesManager : MonoBehaviour{
    public static event Action OnScaleChange;
    
    public static float CardScale{
        get => _cardScale;
        set{
            if (value <= 0) return;
            _cardScale = value;
            OnScaleChange?.Invoke();
        }
    }
    private static float _cardScale = 1;
}
