using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameEvents instance;

    private void Awake() => instance = this;

    public event Action OnGetPill;

    public void CheckMaxPills() => OnGetPill?.Invoke();

    public event Action OnUpdateBullets;

    public void UpdateBullets() => OnUpdateBullets?.Invoke();

    public event Action OnUpdateLife;

    public void UpdateLife() => OnUpdateLife?.Invoke();

    public event Action OnGameOver;

    public void GameOver() => OnGameOver?.Invoke();

}
