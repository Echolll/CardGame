using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlaceInHand : MonoBehaviour
{  
    public bool ThisPlaceAvalible()
    {
       if(gameObject.transform.childCount == 0)
       {
          return true;
       }
       else { return false; }
    }  
}
