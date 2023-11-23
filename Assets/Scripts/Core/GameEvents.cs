using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameEvents instance;

    private void Awake() => instance = this;

}
