using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    CardFightEventTrigger _eventTrigger;

    private void OnEnable()
    {
        _eventTrigger = GetComponent<CardFightEventTrigger>();
        CardCanAttack();
    }

    private void CardCanAttack()
    {
        _eventTrigger.CardCanAttack();
    }
}
