using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class StartedEventTrigger : MonoBehaviour, IPointerClickHandler
{
    CardEventTrigger _cardEventTrigger;
    Player _currectPlayer;

    UIPanelAnimationController _panelAnimationController;
    SelectStarterCards _selectedStarterCards;

    private void OnEnable()
    {        
        _cardEventTrigger = GetComponent<CardEventTrigger>();
        _currectPlayer = _cardEventTrigger.GetPlayerComponent();
        _cardEventTrigger.enabled = false;
    }

    private void Update()
    {
        if (_currectPlayer.CheckCardsInHand())
        {           
            _currectPlayer.OnPutCardToHand();
            CloseUIElement();
        }
    }

    public void SelectStarterCards (SelectStarterCards selectedCard) => _selectedStarterCards = selectedCard;

    public void UIPanelAnimationController (UIPanelAnimationController _uiAnim) => _panelAnimationController = _uiAnim; 

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectedCard();
    }

    public void SelectedCard()
    {
        _currectPlayer.AddCardToHand(this.gameObject);
        _currectPlayer.RemoveCard(this.gameObject);
        
    }

    private void CloseUIElement()
    {
        _cardEventTrigger.enabled = true;
        _panelAnimationController.ChooseCardOnEnd();
        _currectPlayer.PutCardsToStock();
        Destroy(this);
    }
}
