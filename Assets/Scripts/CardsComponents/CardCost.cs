
using UnityEngine;

public class CardCost : MonoBehaviour
{
   CardPlayerService _player;
   Player _currectPlayer;

   CardPropities _card;

    private void Start()
    {
        _player = GetComponent<CardPlayerService>();
        _currectPlayer = _player.CurrectPlayer();
        _card = GetComponent<CardPropities>();

        if (_currectPlayer != null)
        {
            Debug.Log("Игрок проинцилизирован");
        }
    }

   public bool SpendPoints()
   {
        if (_currectPlayer.MagicPoint > 0)
        {
            _currectPlayer.MagicPoint -= _card._cardCost;
            _currectPlayer.RemoveCardInHand(this.gameObject);
            return true;
        }
        else 
        {
            Debug.Log($"Не хватает очков маны у игрока {_currectPlayer}");
            return false;
        }
   }
}
