using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    Material material;

    private void Start()
    {
        material = Resources.Load<Material>("Materials/SimpleMaterials/PlaceMaterial");
        if (material == null)
        {
            Debug.Log("Нет материала");
        }
    }

    public void ChangePlaceMaterial(List<GameObject> cardPlaces)
    {
        for(int i = 0; i < cardPlaces.Count; i++)
        {
            CardPlace card = cardPlaces[i].GetComponent<CardPlace>();
            MeshRenderer matPlace = cardPlaces[i].GetComponent<MeshRenderer>();

            if (card._placeAvalible == true) 
            {
               matPlace.enabled = true;
               matPlace.material = material;
            }
        }
    }

    public void DeleteMaterial(List<GameObject> cardPlaces)
    {
        for (int i = 0;i < cardPlaces.Count;i++) 
        {
            MeshRenderer matPlace = cardPlaces[i].GetComponent<MeshRenderer>();

            if (matPlace != null) 
            {
                matPlace.enabled = false;           
            }
        }  
    }

}
