using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static void ChangeScale(Transform transform, float scale){
        transform.localScale = Vector3.one * scale;
    }
}
