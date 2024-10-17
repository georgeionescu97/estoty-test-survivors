using Events;
using MVC;
using Units;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainMapInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerAttack _playerAttack;

        public override void InstallBindings()
        {
            Container.Bind<IUnitsContainer>().To<UnitsContainer>().AsSingle();
            Container.Bind<IEventsContainer>().To<EventsContainer>().AsSingle();
            Container.Bind<IPlayerController>().To<PlayerController>().FromInstance(_playerController);
            Container.Bind<IPlayerAttack>().To<PlayerAttack>().FromInstance(_playerAttack);
            Container.Bind<IEnemyKillsCount>().To<EnemyKillsCount>().AsSingle();
            Container.Bind<ILevels>().To<LevelsModel>().AsSingle();
            Container.Bind<IController>().To<Controller>().AsSingle().NonLazy();
        }
    }
}

