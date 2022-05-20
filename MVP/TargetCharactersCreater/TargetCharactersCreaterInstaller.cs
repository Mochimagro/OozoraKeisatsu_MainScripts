using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class TargetCharactersCreaterInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<TargetCharactersCreater.TargetCharactersCreaterModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.TargetCharactersCreaterPresenter>().AsCached().NonLazy();
		}
	}
}