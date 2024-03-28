using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardPlayerService : MonoBehaviour
{
    [Inject]Player _currectPlayer;
    [Inject]Player _enemyPlayer;

    void Start()
    {
        if (_currectPlayer == null)
        {
            Debug.Log("у меня нет инъекции этого игрока");
        }
    }

    public void Init(Player _currect , Player _enemy)
    {
        _currectPlayer = _currect;
        _enemyPlayer = _enemy;
    }

    public Player CurrectPlayer()
    {
        return _currectPlayer;
    }

    public Player EnemyPlayer()
    {
        return _enemyPlayer;
    }

    public class Factory : PlaceholderFactory<Player, Player, GameObject, CardPlayerService> 
    {
        public override CardPlayerService Create(Player currentPlayer, Player enemyPlayer, GameObject prefab)
        {
            CardPlayerService newCard = Instantiate(prefab).GetComponent<CardPlayerService>();
            newCard.Init(currentPlayer, enemyPlayer);
            return newCard;
        }

    }
}
