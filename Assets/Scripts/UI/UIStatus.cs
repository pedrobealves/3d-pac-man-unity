using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private CharacterStatus characterStatus;
    [SerializeField] private TMP_Text bulletCurrent;
    [SerializeField] private TMP_Text maxBullet;
    [SerializeField] private Image lifeFill;

    private float offsetMin = 0.096f;
    private float offset = 0.688f;
    // Start is called before the first frame update
    void Start()
    {
        maxBullet.text = characterStatus.maxBullets.ToString();

        GameEvents.instance.OnUpdateBullets += UpdateBullets;

        GameEvents.instance.OnUpdateLife += UpdateLife;

        UpdateLife();
        UpdateBullets();
    }

    private void UpdateLife() => lifeFill.fillAmount = (float)characterStatus.life / characterStatus.maxLife * offset + offsetMin;

    private void UpdateBullets() => bulletCurrent.text = characterStatus.bullets.ToString();

}
