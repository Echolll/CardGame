using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectCardPackConfiguration", menuName = "CardConfigs/GameObject Card Pack Configuration")]
public class GameObjectCardPackConfiguration : ScriptableObject
{
    public List<GameObject> _cards;
}
