using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMagicPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMana;  
    
    private int _manaPoint;
    private int _maxManaPoint;
  
    private Player _currectPlayer;

    public Player Player 
    { 
        get { return _currectPlayer; } 
        set { _currectPlayer = value;}  
    }

    private void Update()
    {
        UpdateDataInfo();
        UpdatePanelData();
    }

    private void UpdateDataInfo()
    {
        _maxManaPoint = _currectPlayer.MaxMagicPoint;
        _manaPoint = _currectPlayer.MagicPoint;
    }

    private void UpdatePanelData()
    {
        _textMana.text = $"{_manaPoint}/{_maxManaPoint}";
    }
}
