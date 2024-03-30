using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] Player _currectPlayer;
    [SerializeField] TMP_Text _health;

    void Update()
    {
        _health.text = _currectPlayer.Health.ToString();
    }
}
