using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class MainPlayerCharacterInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<MainPlayerCharacter.MainPlayerCharacterModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.MainPlayerCharacterPresenter>().AsCached().NonLazy();
		}
	}
}