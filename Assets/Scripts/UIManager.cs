using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour{
    public void ScaleChange(float value){
        GameVariablesManager.CardScale = value;
    }
}
