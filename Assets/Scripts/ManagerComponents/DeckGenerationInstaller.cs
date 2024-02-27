using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class DeckGenerationInstaller : MonoInstaller
{
    [SerializeField] private Player _playerOne;
    [SerializeField] private Player _playerTwo;

    private Player _currectPlayer;

    public override void InstallBindings()
    {
        _currectPlayer = _playerOne;
        Container.Bind<Player>().FromInstance(_currectPlayer);      
        Container.Bind<DeckGenerationInstaller>().FromComponentInHierarchy().AsSingle();     
    }
    
    public void PlayerReBind()
    {
        _currectPlayer = _playerTwo;
        Container.Rebind<Player>().FromInstance(_currectPlayer);
        Debug.Log("Привязка изменилась");
    }

    public Player GetCurrentPlayer()
    {
        return _currectPlayer;
    }
}