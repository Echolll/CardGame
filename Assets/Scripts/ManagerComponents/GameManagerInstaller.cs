using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    [SerializeField] private SelectStarterCards _objectReference;
    [SerializeField] private UIMagicPanel _uiPanel;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<SelectStarterCards>().FromInstance(_objectReference).AsCached();
        Container.Bind<UIMagicPanel>().FromInstance(_uiPanel).AsSingle();
    }
}