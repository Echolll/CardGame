using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlace : MonoBehaviour
{
    public bool _placeAvalible = true;

    private void ChangeAvalible(bool avalible)
    {
        _placeAvalible = avalible;
    }
}
