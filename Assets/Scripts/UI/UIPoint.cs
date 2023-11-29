using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text currentPoints;
    [SerializeField] private CharacterStatus characterStatus;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.OnGetPill += UpdatePoints;
        UpdatePoints();
    }

    // Update is called once per frame
    void UpdatePoints() => currentPoints.text = characterStatus.points.ToString();
}
