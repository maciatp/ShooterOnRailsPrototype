﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpdatePosition_Script : MonoBehaviour
{

    public Vector3 offset = Vector3.zero;

   public void OverWriteEnemyPosition()
    {
       transform.GetChild(0).transform.localPosition +=  offset;
    }
}
