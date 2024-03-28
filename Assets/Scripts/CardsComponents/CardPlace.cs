using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlace : MonoBehaviour
{
    public bool PlaceAvalible()
    {
        if(transform.childCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
